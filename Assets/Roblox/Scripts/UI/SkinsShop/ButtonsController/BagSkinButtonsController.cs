using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagSkinButtonsController : SkinButtonController
{  
    protected override void Start()
    {
        GetSavedData();
        base.Start();   
    }
    protected override void GetSavedData()
    {
        skinsBuyState = Bank.Instance.playerInfo.bagsSkinsBuyStates;
        selectedSkinId = Bank.Instance.playerInfo.selectedBagsId;
    }

    protected override void SelectSkin()
    {
        if (BuySkinButton.GetSelectedSkinCardType() != controllerSkinType)
            return;
        base.SelectSkin();
        Bank.Instance.playerInfo.selectedBagsId = selectedSkinId;       //Сохранение
        YandexSDK.Save();
    }

    public override void SaveBoughtSkinState(int id)
    {
        skinsBuyState[id] = true;
        Bank.Instance.playerInfo.bagsSkinsBuyStates[id] = true;
        YandexSDK.Save();
    }
}
