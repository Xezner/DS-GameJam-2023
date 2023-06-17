using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerationManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform[] _cardSpawnPoints;
    [SerializeField] private CardData _cardData;
    [SerializeField] List<CardData> _cardDataList;
    
    Dictionary<CardType, Color> _cardStyle;
    private int _objectCount;
    private int MAX_OBJECT_COUNT;


    public static CardGenerationManager Instance;

    private void Awake()
    {
        if(Instance == null)
        { 
            Instance = this; 
        }
    }

    private void Start()
    {
        _cardDataList = new();
        _cardStyle = new()
        {
            { CardType.TypeOne, Color.red },
            { CardType.TypeTwo, Color.green },
            { CardType.TypeThree, Color.blue }
        };
        MAX_OBJECT_COUNT = _cardSpawnPoints.Length;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateObjectPool();
        }
    }

    public void GenerateObjectPool()
    {
        var randomNum = Random.Range(1, _cardStyle.Count + 1);
        var cardType = (CardType)randomNum;
        var colorStyle = _cardStyle[cardType];

        PopCardList();
        
        Debug.Log($"PlayerObjects Count:{_cardDataList.Count}");
        Debug.Log($"Objects Count:{_objectCount}");

        PushCardList(cardType, colorStyle);

        if (_objectCount != MAX_OBJECT_COUNT)
        {
            _objectCount++;
        }
    }

    private void PopCardList()
    {
        if (_objectCount == MAX_OBJECT_COUNT)
        {

            switch(_cardDataList[0].InputType)
            {
                case InputType.Approve:
                {
                    AttackSequenceManager.Instance.AddAttackSequence(_cardDataList[0].CardType, _cardDataList[0].SpriteRenderer.color);
                    break;
                }
                case InputType.None:
                {
                    AttackSequenceManager.Instance.ResetAttackSequence();
                    break;
                }
                default: break;
            }

            Destroy(_cardDataList[0]);
            _cardDataList.Remove(_cardDataList[0]);
            _objectCount--;
        }
    }


    private void PushCardList(CardType cardType, Color colorStyle)
    {
        for (int counter = 0; counter < _cardDataList.Count; counter++)
        {
            _cardDataList[counter].transform.position = _cardSpawnPoints[_objectCount - counter].position;
            Debug.Log(_cardDataList[counter].name + ", Counter: " + (_objectCount - counter));
        }
        var currentPaperObject = Instantiate(_cardData, _cardSpawnPoints[0].position, Quaternion.identity, transform);
        currentPaperObject.CardType = cardType;
        currentPaperObject.SpriteRenderer.color = colorStyle;
        _cardDataList.Add(currentPaperObject);
    }
}
