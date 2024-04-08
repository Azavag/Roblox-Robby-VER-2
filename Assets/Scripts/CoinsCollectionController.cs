using System;
using Unity.VisualScripting;
using UnityEngine;

public class CoinsCollectionController : MonoBehaviour
{
    [SerializeField]
    private int levelNumber;

    [SerializeField]
    private CoinController[] coinControllers;
    [SerializeField]
    private ParticleSystem coinsParticles;

    private SoundController soundController;
    private Wallet wallet;

    
    private void OnEnable()
    {
        CoinController.CoinCollected += OnCoinCollected;   
    }

    private void OnDisable()
    {
        CoinController.CoinCollected -= OnCoinCollected;
    }
    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
        wallet = FindObjectOfType<Wallet>();
    }
    void Start()
    {
        bool[] collectedCoinsStatesArray = new bool[0];
        switch (levelNumber)
        {
            case 0:
                collectedCoinsStatesArray = Bank.Instance.playerInfo.areCoinsCollect;
                break;
            case 1:
                collectedCoinsStatesArray = Bank.Instance.playerInfo.areCoinsCollect_2;
                break;

        }
        SetupCoins(collectedCoinsStatesArray);
    }

    void SetupCoins(bool[] coinStates)
    {
        for(int i = 0; i < coinStates.Length; i++)
        {
            if (coinStates[i] == true)
                coinControllers[i].DeactivateCoin();
        }
    }

    private void OnCoinCollected(CoinController coin)
    {
        ParticleSystem tempParticles = 
            Instantiate(coinsParticles, coin.transform.position, coinsParticles.transform.rotation, coin.transform);
        tempParticles.Play();
        soundController.Play("CoinCollect");
        SaveCollectedCoins(coin);
        wallet.OnCoinsValueChanged(coin.GetMoneyValue());
    }

    public void SaveCollectedCoins(CoinController coin)
    {
        int collectedCoinNumber = Array.IndexOf(coinControllers, coin);
        switch (levelNumber)
        {
            case 0:
                Bank.Instance.playerInfo.areCoinsCollect[collectedCoinNumber] = true;
                break;
            case 1:
                Bank.Instance.playerInfo.areCoinsCollect_2[collectedCoinNumber] = true;
                break;
        }

        YandexSDK.Save();
    }
}
