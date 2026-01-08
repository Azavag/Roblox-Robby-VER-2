using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HairSkinsButtonController : SkinButtonController
{
    [SerializeField]
    private HatSkinButtonsController hatSkinButtonController;

    protected override void OnEnable()
    {
        base.OnEnable();
        SkinCard.cardClicked += HideSkin;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        SkinCard.cardClicked -= HideSkin;
    }

    protected override void Start()
    {
        GetSavedData();
        base.Start();
    }

    protected override void SelectSkin()
    {
        if (BuySkinButton.GetSelectedSkinCardType() != controllerSkinType)
            return;

        base.SelectSkin();
        hatSkinButtonController.DropSkin();
        Bank.Instance.playerInfo.selectedHairId = selectedSkinId;       //Сохранение
        YandexSDK.Save();       
    }

   
    protected override void GetSavedData()
    {
        skinsBuyState = Bank.Instance.playerInfo.hairSkinsBuyStates;
        selectedSkinId = Bank.Instance.playerInfo.selectedHairId;
    }

    public override void DropSkin()
    {
        base.DropSkin();
        Bank.Instance.playerInfo.selectedHairId = selectedSkinId;
    }

    protected void HideSkin(SkinCard card)
    {
        if (card.GetSkinType() == SkinType.Hat)
        {
            ShowSkinObject(0);
        }
    }

    public override void SaveBoughtSkinState(int id)
    {
        skinsBuyState[id] = true;
        Bank.Instance.playerInfo.hairSkinsBuyStates[id] = true;
        YandexSDK.Save();
    }

}
