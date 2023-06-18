using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardMechanicManager : MonoBehaviour
{
    public static CardMechanicManager Instance;

    [Header("Card Object Data")]
    [SerializeField] private Transform[] _cardSpawnPoints;
    [SerializeField] private CardData _cardData;
    [SerializeField] List<CardData> _cardDataList;


    [Header("Level Data")]
    [SerializeField] int _countdownTimer;
    [SerializeField] private int _gameSpeed;
    

    Dictionary<CardType, Color> _cardStyle;
    private int _objectCount;
    private int MAX_OBJECT_COUNT;
    private LevelData _levelData;
    private void Awake()
    {
        if(Instance == null)
        { 
            Instance = this; 
        }
    }

    private void Start()
    {
        SetDifficulty();
        ResetCardPool();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            _countdownTimer = 30;
            TimeManager.Instance.CountdownTimer(_countdownTimer).Forget();
            GenerateCardPool().Forget();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            _gameSpeed = 1000;
            SceneManager.LoadScene(0);
        }
    }

    private void ResetCardPool()
    {
        _cardDataList = new();
        _cardStyle = new()
        {
            { CardType.Attack, Color.blue },
            { CardType.Random, Color.red },
            { CardType.Medicard, Color.green }
        };
        MAX_OBJECT_COUNT = _cardSpawnPoints.Length;
    }

    public async UniTask GenerateCardPool()
    {
        while (TimeManager.Instance.CurrentTime > 0)
        {
            //Debug.Log(TimeManager.Instance.CurrentTime);
            RandomizeCardType(out CardType cardType, out Color colorStyle);
            PopCardList();
            await PushCardList(cardType, colorStyle);

            if (_objectCount != MAX_OBJECT_COUNT)
            {
                _objectCount++;
            }
            await UniTask.Delay(_gameSpeed);
            _gameSpeed -= ( (TimeManager.Instance.CurrentTime / 10) * (_gameSpeed / 100));
        }

        Debug.LogError("GAME OVER BITCH!");
    }

    public void RandomizeCardType(out CardType cardType, out Color colorStyle)
    {
        var chanceRate = UnityEngine.Random.Range(0, 101);
        bool isAttack = chanceRate >= _levelData.AttackRate;
        bool isRandom = chanceRate >= _levelData.RandomRate && !isAttack;

        cardType = isAttack ? CardType.Attack : isRandom ? CardType.Random : CardType.Medicard;
        colorStyle = _cardStyle[cardType];
    }

    private void PopCardList()
    {
        if (_objectCount >= MAX_OBJECT_COUNT)
        {
            Destroy(_cardDataList[0].gameObject);
            _cardDataList.Remove(_cardDataList[0]);
            _objectCount--;
        }
    }


    private async UniTask PushCardList(CardType cardType, Color colorStyle)
    {
        for (int counter = 0; counter < _cardDataList.ToList().Count; counter++)
        {
            if (counter == 4)
            {
                continue;
            }
            var animator = _cardDataList[counter].Animator;
            animator.ResetTrigger("Spawn");
            animator.SetTrigger("Despawn");
            while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && animator.IsInTransition(0))
            {
                await UniTask.Yield();
            }
            await UniTask.Delay(50);
            animator.ResetTrigger("Despawn");
            _cardDataList[counter].transform.position = _cardSpawnPoints[_objectCount - counter].position;
            animator.SetTrigger("Spawn");
        }
        var currentPaperObject = Instantiate(_cardData, _cardSpawnPoints[0].position, Quaternion.identity, transform);
        currentPaperObject.CardType = cardType;
        currentPaperObject.SpriteRenderer.color = colorStyle;
        _cardDataList.Add(currentPaperObject);

        if(_objectCount >= MAX_OBJECT_COUNT - 1)
        {
            AttackSequenceManager.Instance.InitiateAttackSequence(_cardDataList[0]);
        }
    }

    private void SetDifficulty(/*LevelData*/)
    {
        //Leveldata.Attackrate
        //LevelData.RandomRate
        //LevelData.MedicardRate

        _levelData = new();
    }

}


[Serializable]
public class LevelData
{
    public int AttackRate = 50;
    public int RandomRate = 30;
    public int MedicardRate = 0;

}