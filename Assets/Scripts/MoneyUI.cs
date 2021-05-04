using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] GameObject nextLevelButton;
    [SerializeField] AudioSource likesAudioSource;
    [SerializeField] AudioSource moneyAudioSource;
    [SerializeField] Transform from;
    [SerializeField] Transform towards;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI moneyAmount;
    [SerializeField] int amountOfMoney;
    [SerializeField] GameObject coinPrefab;
    Coroutine likesCo;

    int index;
    private void Awake()
    {
        moneyText.text = PlayerPrefs.GetInt(GameStrings.MoneyString).ToString() + "k";

        if(nextLevelButton)
            nextLevelButton.SetActive(false);
    }
    public void CountLikes()
    {
        likesCo = StartCoroutine(CountLikesCO());
    }
    public void CalculateCoins()
    {
        CountLikes();
    }
    IEnumerator CountLikesCO()
    {
        int currentMoney = PlayerPrefs.GetInt(GameStrings.MoneyString);

        for (int i = currentMoney; i <= currentMoney + amountOfMoney; i++)
        {
            moneyAmount.text = (i - PlayerPrefs.GetInt(GameStrings.MoneyString)).ToString() + "k";
            if (likesAudioSource && i % (amountOfMoney/10) == 0)
                likesAudioSource.Play();
            yield return new WaitForSeconds(0.001f);
        }

        StartCoroutine(TweenMoneyTowardsUI());

        if(likesCo != null)
            StopCoroutine(likesCo);
    }
    IEnumerator TweenMoneyTowardsUI()
    {
        index = PlayerPrefs.GetInt(GameStrings.MoneyString);
        int currentMoney = index;

        for (int i = currentMoney; i <= currentMoney + amountOfMoney; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform.parent);
            coin.transform.position = from.position;

            coin.transform.DOMove(towards.position, Random.Range(0.8f, 1.5f)).SetEase(Ease.OutFlash).OnComplete(() =>
            {
                Destroy(coin);

                if (moneyAudioSource && i % 2 == 0)
                    moneyAudioSource.Play();
            });

            moneyText.text = i.ToString() + "k";

            index = i;

            yield return new WaitForSeconds(0.001f);
        }

        index++;
        yield return new WaitUntil(() => { return index >= amountOfMoney; });

        if (nextLevelButton)
            nextLevelButton.SetActive(true);
    }
    public int GetPlayerSavedMoney()
    {
        return PlayerPrefs.GetInt(GameStrings.MoneyString);
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt(GameStrings.MoneyString, index);
        PlayerPrefs.Save();
    }
}

