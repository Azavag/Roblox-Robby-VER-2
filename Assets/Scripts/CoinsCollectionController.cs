using System;
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
    private bool[] collectedCoinsState;
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
        collectedCoinsState = new bool[coinControllers.Length];
        LoadCollectedCoinsState();
        PrepareCoinsOnLevel();
    }

    void PrepareCoinsOnLevel()
    {
        for (int i = 0; i < collectedCoinsState.Length; i++)
        {
            if (collectedCoinsState[i])
            {
                coinControllers[i].DeactivateCoin();
            }
        }
    }

    private void OnCoinCollected(CoinObject coin)
    {
        int collectedCoinIndex = Array.IndexOf(coinControllers, coin);
        if (collectedCoinIndex < 0)
        {
            Debug.LogWarning($"Coin {coin.name} was not found in CoinsCollectionController list.");
            return;
        }

        if (collectedCoinsState[collectedCoinIndex])
            return;

        collectedCoinsState[collectedCoinIndex] = true;

        PlayParticles(coin);
        soundController.Play("CoinCollect");
        coinControllers[collectedCoinIndex].DeactivateCoin();
        wallet.OnCoinsValueChanged(coinControllers[collectedCoinIndex].GetMoneyValue());
        SaveCollectedCoinsState();
    }

    private void PlayParticles(CoinObject coin)
    {
        ParticleSystem tempParticles =
          Instantiate(coinsParticles, coin.transform.position, coinsParticles.transform.rotation, coin.transform);
        tempParticles.Play();
    }

    private void LoadCollectedCoinsState()
    {
        if (Bank.Instance.playerInfo.currentLevelsCollectedCoinsMasks == null ||
            levelIndex >= Bank.Instance.playerInfo.currentLevelsCollectedCoinsMasks.Length)
        {
            return;
        }

        string coinsMask = Bank.Instance.playerInfo.currentLevelsCollectedCoinsMasks[levelIndex];
        if (string.IsNullOrEmpty(coinsMask))
        {
            InitializeCoinsStateFromCheckpointProgress();
            SaveCollectedCoinsState();
            return;
        }

        int count = Math.Min(coinsMask.Length, collectedCoinsState.Length);
        for (int i = 0; i < count; i++)
        {
            collectedCoinsState[i] = coinsMask[i] == '1';
        }
    }

    private void InitializeCoinsStateFromCheckpointProgress()
    {
        if (Bank.Instance.playerInfo.currentLevelsCheckpointsNumbers == null ||
            levelIndex >= Bank.Instance.playerInfo.currentLevelsCheckpointsNumbers.Length)
        {
            return;
        }

        int reachedCheckpointIndex = Bank.Instance.playerInfo.currentLevelsCheckpointsNumbers[levelIndex];
        int coinsToDisableCount = Mathf.Clamp(reachedCheckpointIndex, 0, collectedCoinsState.Length);
        for (int i = 0; i < coinsToDisableCount; i++)
        {
            collectedCoinsState[i] = true;
        }
    }


    public void SaveCollectedCoinsState()
    {
        if (Bank.Instance.playerInfo.currentLevelsCollectedCoinsMasks == null ||
            levelIndex >= Bank.Instance.playerInfo.currentLevelsCollectedCoinsMasks.Length)
        {
            return;
        }

        char[] maskChars = new char[collectedCoinsState.Length];
        for (int i = 0; i < collectedCoinsState.Length; i++)
        {
            maskChars[i] = collectedCoinsState[i] ? '1' : '0';
        }

        Bank.Instance.playerInfo.currentLevelsCollectedCoinsMasks[levelIndex] = new string(maskChars);
        YandexSDK.Save();
    }
}
