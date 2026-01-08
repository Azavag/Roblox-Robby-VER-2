using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccessoriesSkinButtonsController : SkinButtonController
{
    private void Start()
    {
        skinsBuyState = Bank.Instance.playerInfo.accessoriesSkinsBuyStates;
        selectedSkinId = Bank.Instance.playerInfo.selectedAccessoiresId;
        for (int i = 0; i < skinCards.Length; i++)
        {
            if (skinsBuyState[i])
                skinCards[i].Unlock();
        }
        ShowSkinObject(selectedSkinId);

        clickedSkinCard = skinCards[selectedSkinId];
        clickedSkinCard.Select();
        clickedSkinCard.Highlight();
    }

    protected override void SelectSkin()
    {
        if (BuySkinButton.GetSelectedSkinCardType() != controllerSkinType)
            return;

        base.SelectSkin();
        Bank.Instance.playerInfo.selectedAccessoiresId = selectedSkinId;       //Сохранение
        YandexSDK.Save();
    }

    public override void SaveBoughtSkinState(int id)
    {
        skinsBuyState[id] = true;
        Bank.Instance.playerInfo.accessoriesSkinsBuyStates[id] = true;
        YandexSDK.Save();
    }
}
