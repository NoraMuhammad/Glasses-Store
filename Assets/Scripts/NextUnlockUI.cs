using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SO.Events;
using System.Collections;

public class NextUnlockUI : MonoBehaviour
{
    [SerializeField] Animator upgradeAnimator;
    [SerializeField] ScoreHandler scoreHandler;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Slider progressBar;
    [SerializeField] TextMeshProUGUI progressPercentage;
    [SerializeField] float requiredFill;
    [SerializeField] EventSO onStartStarsCount;
    Coroutine progressCo;

    private void Awake()
    {
        if (PlayerPrefs.GetFloat(GameStrings.NextUnlockString) > 0.9f)
            gameObject.SetActive(false);

        if (PlayerPrefs.GetFloat(GameStrings.NextUnlockString) == 1f)
            PlayerPrefs.SetFloat(GameStrings.NextUnlockString, 0f);

        progressBar.value = PlayerPrefs.GetFloat(GameStrings.NextUnlockString);

        //if(progressBar.value != 0)
        //    progressPercentage.text = Mathf.Ceil(progressBar.value * 100) + "%";
        progressPercentage.gameObject.SetActive(false);
    }
    public void UpdateProgress()
    {
        if(gameObject.activeInHierarchy)
            progressCo = StartCoroutine(FillProgress());
        else
        {
            onStartStarsCount.Raise();
            scoreHandler.CalculateFinalScore();
        }
    }
    IEnumerator FillProgress()
    {
        float index = progressBar.value;
        float initialProgress = index;

        for (float i = progressBar.value; i < initialProgress + requiredFill; i+= Time.deltaTime)
        {
            progressBar.value = i;
            progressPercentage.text = Mathf.Ceil(i * 100) + "%";

            if (audioSource)
                audioSource.Play();

            index = i;

            yield return new WaitForSeconds(0.01f);
        }

        index += Time.deltaTime;
        yield return new WaitUntil(() => { return decimal.Round((decimal)index, 2) >= (decimal)requiredFill; });

        progressPercentage.text = Mathf.Ceil(index * 100) + "%";

        if(progressBar.value > 0.9f)
        {
            upgradeAnimator.enabled = true;
            LevelUpgradeHandler.Instance.SetPlantPotUpgrade();
        }

        if (audioSource)
            audioSource.Play();

        onStartStarsCount.Raise();

        scoreHandler.CalculateFinalScore();
        if (progressCo != null)
            StopCoroutine(progressCo);
    }
    public void SaveUnlockProgress()
    {
        PlayerPrefs.SetFloat(GameStrings.NextUnlockString, progressBar.value);
        PlayerPrefs.Save();
    }
}
