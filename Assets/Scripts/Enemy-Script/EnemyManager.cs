using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private RectTransform _enemyRectTransform;
    [SerializeField] private SpriteRenderer _enemySprite;
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private List<EnemyHealth> _enemyHealthList = new();
    [SerializeField] private List<Sprite> _enemyStateSpriteList;
    [SerializeField] private List<Sprite> _timerStateSpriteList;
    public List<EnemyHealth> EnemyHealthList { get { return _enemyHealthList; } set { _enemyHealthList = value; } }
    private EnemyState _currentEnemyState;
    private EnemyState _previousEnemyState;
    private bool _isGameStarted = false;
    private bool _isScaling = false;
    private bool _isAbrupted = false;
    private Dictionary<EnemyState, Vector3> _enemyStateScale;
    private Dictionary<EnemyState, Sprite> _enemyStateSprite;
    private Dictionary<EnemyState, Sprite> _timerStateSprite;


    private int counter = 0;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _enemyStateScale = new()
        {
            { EnemyState.PhaseOne, new(0.5f,0.5f,1f) },
            { EnemyState.PhaseTwo, new(0.85f,0.85f,1f) },
            { EnemyState.PhaseThree, new(1.15f,1.15f,1f) },
            { EnemyState.AttackMode, new(1.5f,1.5f,1f) },
        };
        _enemyStateSprite = new()
        {
            { EnemyState.PhaseOne, _enemyStateSpriteList[0] },
            { EnemyState.PhaseTwo, _enemyStateSpriteList[0] },
            { EnemyState.PhaseThree, _enemyStateSpriteList[1] },
            { EnemyState.AttackMode, _enemyStateSpriteList[1]},
            { EnemyState.Dead, _enemyStateSpriteList[3]}
        };
        _timerStateSprite = new()
        {
            { EnemyState.PhaseOne, _timerStateSpriteList[2] },
            { EnemyState.PhaseTwo, _timerStateSpriteList[1] },
            { EnemyState.PhaseThree, _timerStateSpriteList[0] },
            { EnemyState.AttackMode, _timerStateSpriteList[0] },
        };

        _currentEnemyState = EnemyState.PhaseOne;
        _previousEnemyState = EnemyState.Dead;
    }

    public void InitEnemyData()
    {
        _isGameStarted = true;
        
        InitializeHealth();
    }
    void InitializeHealth()
    {
        DestroyChildren(_healthBar.transform);
        Debug.Log($"ENEMY HEALTH: {_enemyData.EnemyHealth}");
        for(int i = 0; i < _enemyData.EnemyHealth; i++)
        {
            var health = Instantiate(_enemyData.HealthPrefab,_healthBar.GetComponent<RectTransform>());
            health.HealthSprite.sprite = CardMechanicManager.Instance.CardStyle[_enemyData.Sequence];
            health.CardType = _enemyData.Sequence;
            _enemyHealthList.Add(health);
        }
    }

    private void Update()
    {
        CheckEnemyState();
    }

    public void CheckEnemyState()
    {
        if (_isGameStarted/* && !_isScaling*/)
        {
            if (TimeManager.Instance.CurrentTime <= TimeManager.Instance.MaxTime)
            {
                _currentEnemyState = EnemyState.PhaseOne;
                //_enemyPrefab.transform.localScale = _enemyStateScale[_currentEnemyState];
            }
            if (TimeManager.Instance.CurrentTime <= Math.Floor(TimeManager.Instance.MaxTime * 0.67))
            {
                _currentEnemyState = EnemyState.PhaseTwo;
            }
            if (TimeManager.Instance.CurrentTime <= Math.Floor(TimeManager.Instance.MaxTime * 0.33))
            {
                _currentEnemyState = EnemyState.PhaseThree;
            }
            if (TimeManager.Instance.CurrentTime < 1)
            {
                _isGameStarted = false;
                _currentEnemyState = EnemyState.AttackMode;
                StopAllCoroutines();
            }
            Debug.Log($"<color=yellow>CoroutineCounter: </color> {counter}");
            Debug.Log($"Previous Enemy State: {_previousEnemyState}, Current State: {_currentEnemyState}");
            Debug.LogError($"CurrentScale: {_enemyRectTransform.localScale}"); 
            if (_currentEnemyState != _previousEnemyState && _currentEnemyState != EnemyState.Dead && _currentEnemyState != EnemyState.AttackMode)
            {
                bool isRegressing = false;
                if ((int)_currentEnemyState < (int)_previousEnemyState && _previousEnemyState != EnemyState.Dead)
                {
                    Debug.LogError("REVERTED");
                    isRegressing = true;
                }
                EnemyStateChanged();
                StopCoroutine(nameof(UpdateEnemyScale));
                StartCoroutine(UpdateEnemyScale(_currentEnemyState, isRegressing));
            }
        }
    }

    private void EnemyStateChanged()
    {
        Debug.LogError("ENEMY STATE CHANGED");
        _previousEnemyState = _currentEnemyState;
        Debug.LogWarning($"Previous Enemy State: {_previousEnemyState}, Current State: {_currentEnemyState}");
    }

    private IEnumerator UpdateEnemyScale(EnemyState enemyState, bool isRegressing)
    {
        Debug.LogError($"<color=red>INITIAL UPDATE ENEMY SCALE: {_enemyRectTransform.localScale}</color>");
        yield return new WaitForSeconds(0.1f);
        CalculateTransformTime(out int time, out float timePercentage, isRegressing);

        TimeManager.Instance.UpdateTimerState(_timerStateSprite[enemyState]);
        _enemySprite.sprite = _enemyStateSprite[enemyState];

        float elapsedTime = 0f;
        var nextEnum = isRegressing ? enemyState : (EnemyState)((int)enemyState + 1);
        Vector3 initialScale = _enemyRectTransform.localScale;
        Vector3 targetScale = _enemyStateScale[nextEnum];

        if(isRegressing)
        {
            targetScale = initialScale + (targetScale - initialScale) * timePercentage;
        }
        Debug.LogError($"Initial scale: {initialScale}, Target Scale: {targetScale}, TIME: {time}, percentage: {timePercentage}");
        if (enemyState != EnemyState.AttackMode)
        {
            while (elapsedTime < time && _isGameStarted)
            {
                float speed = 1f;
                if (!isRegressing)
                {
                    CalculateTransformTime(out int timeElapsed, out speed, isRegressing);
                }
                float t = elapsedTime / time;
                float scaledT = Mathf.Pow(t, speed);
                Debug.Log($"ScaledT: {scaledT}");
                _enemyRectTransform.localScale = Vector3.Lerp(initialScale, targetScale, scaledT);
                elapsedTime += Time.deltaTime;
                Debug.LogError($"FINAL UPDATE ENEMY SCALE: {_enemyRectTransform.localScale}");
                yield return null;
            }
            //_enemyRectTransform.localScale = targetScale;
        }
        Debug.Log($"End Scale: {_enemyRectTransform.localScale}");
    }

    private void CalculateTransformTime(out int time, out float timePercentage, bool isRegressing)
    {
        var oneThirdTimeWithIncrease = (TimeManager.Instance.MaxTime + TimeManager.Instance.TimeIncrease) * 0.33;
        var oneThirdtime = TimeManager.Instance.MaxTime * 0.33;
        time = (int)Math.Ceiling(oneThirdTimeWithIncrease + 1);
        timePercentage = 2.11f - (time / (float)oneThirdtime);
        if (isRegressing)
        {
            time = (int)Math.Ceiling(oneThirdTimeWithIncrease - oneThirdtime);
            time = time == 0 ? 1 : time;
            timePercentage = time / (float)oneThirdTimeWithIncrease;
        }
        Debug.Log($"Time {time}, Time Percentage: {timePercentage}, Time Increase: {TimeManager.Instance.TimeIncrease}");
        TimeManager.Instance.IncreaseTime(0);
    }

    public void AttackCheck(CardType card)
    {
        foreach (var health in EnemyHealthList.ToList())
        {
            if (card == health.CardType)
            {
                health.TriggerHealth();
                EnemyHealthList.Remove(health);
                break;
            }
        }
        CheckEnemyHealth();
    }

    public void CheckEnemyHealth()
    {
        if(EnemyHealthList.Count == 0)
        {
            _isGameStarted = false;
            _currentEnemyState = EnemyState.Dead;
            _enemySprite.sprite = _enemyStateSprite[EnemyState.Dead];
            StopAllCoroutines();
            Debug.Log("DEAD");
            CardMechanicManager.Instance.IsTokenCancelled = true;
        }
    }

    public void GameStartedToggler()
    {
        _currentEnemyState = EnemyState.AttackMode;
        _isGameStarted = !_isGameStarted;
    }

    void DestroyChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
}


public enum EnemyState
{
    PhaseOne = 0,
    PhaseTwo = 1,
    PhaseThree = 2,
    AttackMode = 3,
    Dead = 4
}