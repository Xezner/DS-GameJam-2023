using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public void WaveOverlayStartCallback()
    {
        CardMechanicManager.Instance.WaveStartCallback();
    }

    public void AfterWaveOverlayCallback()
    {
        CardMechanicManager.Instance.AfterWaveOverlayCallback();
    }
}
