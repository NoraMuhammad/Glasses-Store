using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] MoneyUI money;
    public void UpdateMoney()
    {
        money.CalculateCoins();
    }
    public void PlayStarSound()
    {
        if (audioSource)
            audioSource.Play();
    }    
}
