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
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private List<EnemyHealth> _enemyHealthList = new();

    public List<EnemyHealth> EnemyHealthList { get { return _enemyHealthList; } set { _enemyHealthList = value; } }
    private EnemyState _enemyState;
    private bool _isGameStarted = false;
    private Dictionary<EnemyState, Vector3> _enemyStateScale;
    private Dictionary<EnemyState, Sprite> _enemyStateSprite;

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
    }

    public void InitEnemyData()
    {
        _isGameStarted = true;
        DestroyChildren();
        InitializeHealth();
    }


    void DestroyChildren()
    {
        foreach (Transform child in _healthBar.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void InitializeHealth()
    {
        for(int i = 0; i < _enemyData.EnemyHealth; i++)
        {
            var health = Instantiate(_enemyData.HealthPrefab,_healthBar.GetComponent<RectTransform>());
            health.HealthSprite.sprite = CardMechanicManager.Instance.CardStyle[_enemyData.Sequence];
            health.CardType = _enemyData.Sequence;
            _enemyHealthList.Add(health);
        }
    }

    private bool _isScaling = false;

    // Update is called once per frame
    void Update()
    {
        if (_isGameStarted && !_isScaling)
        {
            if (TimeManager.Instance.CurrentTime == TimeManager.Instance.MaxTime)
            {
                _enemyState = EnemyState.PhaseOne;
                _enemyPrefab.transform.localScale = _enemyStateScale[_enemyState];
            }
            else if (TimeManager.Instance.CurrentTime <= Math.Floor(TimeManager.Instance.MaxTime * 0.67))
            {
                _enemyState = EnemyState.PhaseTwo;
            }
            else if (TimeManager.Instance.CurrentTime <= Math.Floor(TimeManager.Instance.MaxTime * 0.33))
            {
                _enemyState = EnemyState.PhaseThree;
            }
            else if (TimeManager.Instance.CurrentTime <= 1)
            {
                _isGameStarted = false;
                _enemyState = EnemyState.AttackMode;
            }
            var time = (int)Math.Ceiling(TimeManager.Instance.MaxTime * 0.33) + 1;
            UpdateEnemyState(_enemyState, time).Forget();
        }
    }

    private async UniTask UpdateEnemyState(EnemyState enemyState, int time)
    {
        float elapsedTime = 0f;
        var nextEnum = (EnemyState)((int)enemyState + 1);
        Vector3 initialScale = _enemyPrefab.transform.localScale;
        Vector3 targetScale = _enemyStateScale[nextEnum];

        if (enemyState != EnemyState.AttackMode)
        {
            _isScaling = true;
            while (elapsedTime < time)
            {
                float t = elapsedTime / time;
                _enemyPrefab.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
                elapsedTime += Time.deltaTime;
                await UniTask.Yield();
            }
            _enemyPrefab.transform.localScale = _enemyStateScale[(EnemyState)(int)enemyState + 1];
        }
        _isScaling = false;
    }

    public void AttackCheck(CardType card)
    {
        foreach (var health in EnemyHealthList.ToList())
        {
            Debug.Log($"HEALTH ATTACK: {health.CardType}");
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
            Debug.Log("HELLO");
            CardMechanicManager.Instance.IsTokenCancelled = true;
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