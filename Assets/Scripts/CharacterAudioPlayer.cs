using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource thinkingAudioSource;
    [SerializeField] GameObject requirementsWindow;

    public void PlayThinkingSound()
    {
        if (thinkingAudioSource)
            thinkingAudioSource.Play();
    }
    public void ShowRequirementsWindow()
    {
        requirementsWindow.SetActive(true);
    }
}
