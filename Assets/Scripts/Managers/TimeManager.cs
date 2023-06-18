using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Image _timeUISprite;

    private int _currentTime;
    public int CurrentTime { get { return _currentTime; } set{ _currentTime = value; } }

    public int MaxTime;

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

        MaxTime = maxTimer;
       _currentTime = maxTimer;
        while (_currentTime >= 0 && !CardMechanicManager.Instance.IsTokenCancelled)
        {
            if (CardMechanicManager.Instance.IsTokenCancelled)
            {
                return;
            }
            _timerText.text = $"{_currentTime}";
            await UniTask.Delay(ONE_SECOND);
            _currentTime--;
        }
    }

    public void UpdateTimerState(Sprite _sprite)
    {
        _timeUISprite.sprite = _sprite;
    }

    public void IncreaseTime(int timeIncrease)
    {
        Debug.Log($"CurrentTime: {_currentTime}");
        _currentTime += timeIncrease;
        Debug.Log($"TimeAfter: {_currentTime}");
    }

    public void ResetEverything()
    {
        _timerText.text = string.Empty;
        _currentTime = 0;
    }
}
