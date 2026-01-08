using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HatSkinButtonsController : SkinButtonController
{
    [Header("Options")]
    [SerializeField]
    private HairSkinsButtonController hairSkinButtonController;

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
    protected override void GetSavedData()
    {
        skinsBuyState = Bank.Instance.playerInfo.hatSkinsBuyStates;
        selectedSkinId = Bank.Instance.playerInfo.selectedHatId;
    }

    public override void SaveBoughtSkinState(int id)
    {
        skinsBuyState[id] = true;
        Bank.Instance.playerInfo.hatSkinsBuyStates[id] = true;
        YandexSDK.Save();
    }

    protected override void SelectSkin()
    {
        if (BuySkinButton.GetSelectedSkinCardType() != controllerSkinType)
            return;

        base.SelectSkin();
        hairSkinButtonController.DropSkin();
        Bank.Instance.playerInfo.selectedHatId = selectedSkinId;       //Сохранение
        YandexSDK.Save();
    }
    public override void DropSkin()
    {
        base.DropSkin();
        Bank.Instance.playerInfo.selectedHatId = selectedSkinId;
    }

    protected void HideSkin(SkinCard card)
    {
        if(card.GetSkinType() == SkinType.HairStyles)
        {
            ShowSkinObject(0);
        }
    }
}
