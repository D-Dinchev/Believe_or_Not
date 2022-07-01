using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 45;
    }
}
