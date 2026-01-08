using System;
using Unity.VisualScripting;
using UnityEngine;

public class CoinsCollectionController : MonoBehaviour
{
    [SerializeField]
    private int levelIndex;

    [SerializeField]
    private CoinObject[] coinControllers;
    [SerializeField]
    private ParticleSystem coinsParticles;

    private SoundController soundController;
    private Wallet wallet;

    [SerializeField]
    private int lastCollectedCoinIndex;
    private void OnEnable()
    {
        CoinObject.CoinCollected += OnCoinCollected;   
    }

    private void OnDisable()
    {
        CoinObject.CoinCollected -= OnCoinCollected;
    }
    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
        wallet = FindObjectOfType<Wallet>();

    }
    void Start()
    {
        int currenCollectedCoinIndex = Bank.Instance.playerInfo.currentLevelsCoinsNumbers[levelIndex];
        lastCollectedCoinIndex = currenCollectedCoinIndex;

        if (lastCollectedCoinIndex < 0)
            return;
        PrepareCoinsOnLevel(lastCollectedCoinIndex);
    }

    void PrepareCoinsOnLevel(int collectedCoins)
    {
        for(int i = 0; i < collectedCoins; i++)
        {
            coinControllers[i].DeactivateCoin();
        }
    }

    private void OnCoinCollected(CoinObject coin)
    {
        PlayParticles(coin);
        soundController.Play("CoinCollect");
        int collectedCoinIndex = Array.IndexOf(coinControllers, coin);
        SaveCollectedCoinIndex(collectedCoinIndex);
        CalculateMoneyValue(collectedCoinIndex);
    }

    private void PlayParticles(CoinObject coin)
    {
        ParticleSystem tempParticles =
          Instantiate(coinsParticles, coin.transform.position, coinsParticles.transform.rotation, coin.transform);
        tempParticles.Play();
    }

    private void CalculateMoneyValue(int collectedCoinIndex)
    {
        int totalMoneyValue = 0;
        int startIndex = lastCollectedCoinIndex + 1;
        // ѕроходим по всем пропущенным и текущей монете
        for (int i = startIndex; i <= collectedCoinIndex; i++)
        {
            totalMoneyValue += coinControllers[i].GetMoneyValue();
            coinControllers[i].DeactivateCoin();
        }


        wallet.OnCoinsValueChanged(totalMoneyValue);
        lastCollectedCoinIndex = collectedCoinIndex;
    }


    public void SaveCollectedCoinIndex(int coinIndex)
    {
        Bank.Instance.playerInfo.currentLevelsCoinsNumbers[levelIndex] = coinIndex;     
        YandexSDK.Save();
    }
}
