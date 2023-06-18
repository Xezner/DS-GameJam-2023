using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [SerializeField] private TextMeshProUGUI _timerText;

    private int _currentTime;
    public int CurrentTime { get { return _currentTime; } set{ _currentTime = value; } }

    const int ONE_SECOND = 1000;

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
        DontDestroyOnLoad(this.gameObject);
        _timerText.text = string.Empty;
    }

    public async UniTask CountdownTimer(int maxTimer)
    {
        while(maxTimer >= 0)
        {
            _currentTime = maxTimer;
            _timerText.text = $"Time Left: {_currentTime}";
            await UniTask.Delay(ONE_SECOND);
            maxTimer--;
        }
    }

    public void ResetEverything()
    {
        _timerText.text = string.Empty;
        _currentTime = 0;
    }
}
