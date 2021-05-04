using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSpray : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] ColorUI initialColor;
    [SerializeField] SelectableUI selectedColor;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] AudioSource shakingAudio;
    public void PlaySprayShakingSound()
    {
        if (shakingAudio)
            shakingAudio.Play();
    }
    private void Start()
    {
        initialColor.EnableColor();
        selectedColor.SelectUI(true);
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.position = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            anim.StopPlayback();
            anim.SetTrigger("shake");
        }
    }

    public void playSpraySound()
    {
        audioSource.clip = audioClip;
        audioSource.PlayOneShot(audioClip);
    }
}
