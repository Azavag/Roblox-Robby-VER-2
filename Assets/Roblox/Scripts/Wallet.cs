using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int coins;
    public static event Action CoinsChanged; 
    private void OnEnable()
    {
        BuySkinButton.SkinPurchaseMade += OnCoinsValueChanged;
    }
    private void OnDisable()
    {
        BuySkinButton.SkinPurchaseMade -= OnCoinsValueChanged;
    }

    void Start()
    {
        coins = Bank.Instance.playerInfo.moneyCount;
        CoinsChanged?.Invoke();
    }
    public void OnCoinsValueChanged(int diff)
    {
        coins += diff;
        Bank.Instance.playerInfo.moneyCount = coins;
        YandexSDK.Save();
        CoinsChanged?.Invoke();
    }

}
