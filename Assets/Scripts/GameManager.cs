using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(Instance == null)
        {
            Instance = this;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
