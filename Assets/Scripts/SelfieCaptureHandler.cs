using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SelfieCaptureHandler : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] PostProcessVolume volume;
    [SerializeField] [Range(0, 5)] float focusDistanceRange;
    DepthOfField DOF;
    bool canFocus = true;
    int bonus;
    private void Awake()
    {
        if (volume.profile.TryGetSettings(out DOF))
        {
            DOF.focusDistance.value = 5.0f;
        }
    }
    private void Update()
    {
        if(canFocus)
            DOF.focusDistance.value = focusDistanceRange;
    }
    public float GetCurrentFocus()
    {
        return DOF.focusDistance.value;
    }
    public void PlayAudioClip()
    {
        source.Play();
    }
    public void DisableFocus()
    {
        canFocus = false;
    }
}
