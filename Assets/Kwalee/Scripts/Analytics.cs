using System.Collections;
using UnityEngine;
using Facebook.Unity;

public class Analytics : MonoBehaviour
{
    private void Awake()
    {
        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Application.targetFrameRate = 60;
        }

        FB.Init(FBInitCallback);
    }

    private void FBInitCallback()
    {
        if(FB.IsInitialized)
        {
            FB.ActivateApp();
        }
    }

    public void OnApplicationPause(bool pause)
    {
        if(!pause)
        {
            if(FB.IsInitialized)
            {
                FB.ActivateApp();
            }
        }
    }
}
