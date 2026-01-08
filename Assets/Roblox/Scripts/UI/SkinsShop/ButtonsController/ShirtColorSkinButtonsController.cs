using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShirtColorSkinButtonsController : SkinButtonController
{
    [SerializeField] private SkinsColorsStorage skinsColorsStorage;
    private Color[] shirtColors;

    private void OnValidate()
    {
        Initialization();
    }

    void Initialization()
    {
        if (skinsColorsStorage != null)
        {
            shirtColors = skinsColorsStorage.GetColorsArray();
            for (int i = 0; i < skinCards.Length; i++)
            {
                skinCards[i].ChangeSkinImageColor(shirtColors[i]);
            }
        }
    }

    protected override void GetSavedData()
    {
        skinsBuyState = Bank.Instance.playerInfo.shirtsSkinsBuyStates;
        selectedSkinId = Bank.Instance.playerInfo.selectedShirtId;
    }

    protected override void Start()
    {
        GetSavedData();
        base.Start();
        Initialization();
    }

    protected override void SelectSkin()
    {
        if (BuySkinButton.GetSelectedSkinCardType() != controllerSkinType)
            return;
        base.SelectSkin();     
        Bank.Instance.playerInfo.selectedShirtId = selectedSkinId;       //Сохранение
        YandexSDK.Save();
    }

    public override void SaveBoughtSkinState(int id)
    {
        skinsBuyState[id] = true;
        Bank.Instance.playerInfo.shirtsSkinsBuyStates[id] = true;
        YandexSDK.Save();
    }
}



