using UnityEngine;
using UnityEngine.UI;

public class MusicUI : MonoBehaviour
{
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] Sprite mutedImages;
    [SerializeField] Sprite unmutedImages;
    Image audioImage;
    private void Awake()
    {
        audioImage = GetComponent<Image>();
    }
    public void ShuffleAudioMute()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if(audioSources[i].isPlaying)
            {
                audioSources[i].Pause();
                audioImage.sprite = mutedImages;
            }
            else
            {
                audioSources[i].Play(); 
                audioImage.sprite = unmutedImages;
            }
        }
    }
}
