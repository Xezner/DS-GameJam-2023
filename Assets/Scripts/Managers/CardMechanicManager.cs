using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardMechanicManager : MonoBehaviour
{
    public static CardMechanicManager Instance;

    [Header("Card Object Data")]
    [SerializeField] private Transform[] _cardSpawnPoints;
    [SerializeField] private CardData _cardData;
    [SerializeField] List<CardData> _cardDataList;

    [Header("Objects To Turn off")]
    [SerializeField] private GameObject _attackSequencer;

    [Header("Level Data")]
    [SerializeField] int _countdownTimer;
    [SerializeField] private int _gameSpeed;
    [SerializeField] private EnemyData _enemyData;

    //Temp sprite data change when there are levels
    [Header("Card Sprites")]
    [SerializeField] private Sprite _attackIcon;
    [SerializeField] private Sprite _randomIcon;
    [SerializeField] private Sprite _medicardIcon;

    [Header("Card Overlay")]
    [SerializeField] private Sprite _approveOverlay;
    [SerializeField] private Sprite _rejectOverlay;

    [Header("Camera Animator")]
    [SerializeField] private CameraAnimationController _cameraAnimationController;

    public Dictionary<InputType, Sprite> CardOverlay { get { return _cardOverlay; } set { _cardOverlay = value; } }
    public Dictionary<CardType, Sprite> CardStyle { get { return _cardStyle; } set { _cardStyle = value; } }
    private Dictionary<InputType, Sprite> _cardOverlay;
    private Dictionary<CardType, Sprite> _cardStyle;
    private int _objectCount;
    private int MAX_OBJECT_COUNT;
    private DifficultyDataHolder _levelData;

    public bool IsTokenCancelled = false;
    private void Awake()
    {
        if(Instance == null)
        { 
            Instance = this; 
        }
    }

    private void Start()
    {
        GameStart(GameManager.Instance.CurrentDifficulty);
    }

    public void Init(DifficultyDataHolder difficultyData)
    {
        InitCardSprites();
        ResetCardPool();
        SetLevelData(difficultyData);
    }

    public void GameStart(DifficultyDataHolder difficultyData)
    {
        Init(difficultyData);
        TimeManager.Instance.CountdownTimer(_countdownTimer).Forget();
        GenerateCardPool().Forget();
    }


    private void InitCardSprites()
    {
        _cardStyle = new()
        {
            { CardType.Attack, _attackIcon },
            { CardType.Random, _randomIcon },
            { CardType.Medicard, _medicardIcon }
        };

        _cardOverlay = new()
        {
            {InputType.Approve, _approveOverlay },
            {InputType.Reject, _rejectOverlay },
            {InputType.None, null }
        };
    }

    private void ResetCardPool()
    {
        _cardDataList = new();
        MAX_OBJECT_COUNT = _cardSpawnPoints.Length;
    }

    public async UniTask GenerateCardPool()
    {
        while (TimeManager.Instance.CurrentTime > 0 && !IsTokenCancelled)
        {
            if (IsTokenCancelled)
            {
                return;
            }
            //Debug.Log(TimeManager.Instance.CurrentTime);
            RandomizeCardType(out CardType cardType, out Sprite cardStyle);
            PopCardList();
            await PushCardList(cardType, cardStyle);

            if (_objectCount != MAX_OBJECT_COUNT)
            {
                _objectCount++;
            }
            await UniTask.Delay(_gameSpeed);
            _gameSpeed -= ( (TimeManager.Instance.CurrentTime / 10) * (_gameSpeed / 100));
        }

        GameOver();
    }

    public void GameOver()
    {
        if (!IsTokenCancelled)
        {
            HideCardAndEncoder();
            _cameraAnimationController.AnimateCameraDeath();
            Debug.LogError("GAME OVER BITCH!");
        }
        else
        {
            HideCardAndEncoder();
            _cameraAnimationController.AnimateCameraWin();
            Debug.LogError("MISSION CLEARED");
        }
        EnemyManager.Instance.GameStartedToggler();
    }

    
    public void OpenGameOverScreen()
    {
        var resultDisplay = !IsTokenCancelled ? GameManager.Instance.GameOverUI : 
                           _levelData.DifficultyLevel == Difficulty.Legend ? GameManager.Instance.LegendClear : GameManager.Instance.MissionSuccess;
        resultDisplay.SetActive(true);
    }



    public void HideCardAndEncoder()
    {
        TimeManager.Instance.gameObject.SetActive(false);
        _cardSpawnPoints[0].transform.parent.gameObject.SetActive(false);
        _attackSequencer.SetActive(false);
    }

    public void RandomizeCardType(out CardType cardType, out Sprite cardStyle)
    {
        var chanceRate = UnityEngine.Random.Range(0, 101);
        bool isAttack = chanceRate >= _levelData.AttackRate;
        bool isRandom = chanceRate >= _levelData.RandomRate && !isAttack;

        cardType = isAttack ? CardType.Attack : isRandom ? CardType.Random : CardType.Medicard;
        cardStyle = _cardStyle[cardType];
    }

    private void PopCardList()
    {
        if (_objectCount >= MAX_OBJECT_COUNT)
        {
            var toDestroy = _cardDataList[MAX_OBJECT_COUNT - 1];
            _cardDataList.Remove(_cardDataList[MAX_OBJECT_COUNT - 1]);
            toDestroy.Animator.ResetTrigger("Spawn");
            toDestroy.Animator.ResetTrigger("Despawn");
            Destroy(toDestroy.gameObject);
            _objectCount--;
        }
    }


    private async UniTask PushCardList(CardType cardType, Sprite colorStyle)
    {
        for (int counter = 0; counter < _cardDataList.Count; counter++)
        {
            var cardData = _cardDataList[counter];
            var animator = cardData.Animator;
            animator.ResetTrigger("Spawn");
            animator.SetTrigger("Despawn");
            while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && animator.IsInTransition(0))
            {
                await UniTask.Yield();
            }
            await UniTask.Delay(50);
            animator.ResetTrigger("Despawn");
            cardData.transform.position = _cardSpawnPoints[counter + 1].position;
            cardData.ShowSprites();
            animator.SetTrigger("Spawn");
        }
        var currentPaperObject = Instantiate(_cardData, _cardSpawnPoints[0].position, Quaternion.identity, transform);
        currentPaperObject.CardType = cardType;
        currentPaperObject.Icon.sprite = colorStyle;
        currentPaperObject.Overlay.sprite = null;
        _cardDataList.Insert(0, currentPaperObject);

        _cardDataList.First().HideSprites();
        if (_objectCount >= MAX_OBJECT_COUNT - 1)
        {
            _cardDataList?.Last().HideSprites();
            AttackSequenceManager.Instance.InitiateAttackSequence(_cardDataList[MAX_OBJECT_COUNT-1]);
        }
    }

    private void SetLevelData(DifficultyDataHolder difficultyData)
    {
        _levelData = difficultyData;
        _countdownTimer = _levelData.GameTime;
        _gameSpeed = _levelData.GameSpeed;
        EnemyManager.Instance.InitEnemyData();
    }

}