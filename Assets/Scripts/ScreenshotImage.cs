using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotImage : MonoBehaviour
{
    [SerializeField] NextUnlockUI nextUnlock;
    public void StartUpdatingProgress()
    {
        nextUnlock.UpdateProgress();
    }
}
