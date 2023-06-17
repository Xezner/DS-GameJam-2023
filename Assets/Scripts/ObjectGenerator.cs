using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _paperPrefab;
    [SerializeField] private Transform[] _paperPoints;

    //private GameObject _paperPrefab;
    //private Transform[] _paperPoints;

    public GameObject PaperPrefab { get { return _paperPrefab; } set { value = _paperPrefab; } }
    public Transform[] PaperPoints { get { return _paperPoints; } set { value = _paperPoints; } }

    [SerializeField] List<GameObject> _paperObjects;

    Dictionary<PaperStyle, Color> _paperStyle;
    private int _objectCount;
    private const int MAX_OBJECT_COUNT = 7;


    public static ObjectGenerator Instance;

    private void Awake()
    {
        if(Instance == null)
        { 
            Instance = this; 
        }
    }

    private void Start()
    {
        _paperObjects = new();
        _paperStyle = new()
        {
            { PaperStyle.StyleOne, Color.red },
            { PaperStyle.StyleTwo, Color.green },
            { PaperStyle.StyleThree, Color.blue }
        };
    }


    public void GenerateObjectPool()
    {
        //if(_paperObjects.Count == 5)
        //{
        //    _objectCount = 0;
        //    Destroy(_paperObjects[5]);
        //    _paperObjects.Remove(_paperObjects[5]);
        //    PushObjectsList();

        //    GenerateObjectPool();
        //}
        //if(_paperObjects.Count == 5)
        //{
        //    foreach(GameObject gameObject in _paperObjects)
        //    {
        //        Destroy(gameObject);
        //    }
        //    _paperObjects.Clear();

        //    _objectCount = 0;
        //}
        var num = Random.Range(0, _paperStyle.Count);
        var colorStyle = _paperStyle[(PaperStyle)num];
        if (_objectCount == MAX_OBJECT_COUNT)
        {
            Destroy(_paperObjects[0]);
            _paperObjects.Remove(_paperObjects[0]);
            _objectCount--;
        }
        Debug.Log($"PlayerObjects Count:{_paperObjects.Count}");
        Debug.Log($"Objects Count:{_objectCount}");
        for (int counter = 0; counter < _paperObjects.Count; counter++)
        {
            _paperObjects[counter].transform.position = _paperPoints[_objectCount - counter].position;
            Debug.Log(_paperObjects[counter].name + ", Counter: " + (_objectCount - counter));
        }
        var currentPaperObject = Instantiate(_paperPrefab, _paperPoints[0].position, Quaternion.identity, transform);
        currentPaperObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colorStyle;
        currentPaperObject.name = $"Object# {_objectCount}";
        _paperObjects.Add(currentPaperObject);

        if (_objectCount != MAX_OBJECT_COUNT)
        {
            _objectCount++;
        }
    }


    private void PushObjectsList()
    {
        //_paper
    }
}
