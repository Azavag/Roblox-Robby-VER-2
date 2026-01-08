using TMPro;
using UnityEngine;

public class HairColorsSkinButtonsController : SkinButtonController
{
    [SerializeField]
    private SkinType skinType;
    [SerializeField]
    private SkinsColorsStorage skinsColorsStorage;


    protected override void Start()
    {
        GetSavedData();
        base.Start();
    }
    protected override void GetSavedData()
    {
        skinsBuyState = Bank.Instance.playerInfo.hairColorsBuyStates;
        selectedSkinId = Bank.Instance.playerInfo.selectedHairColorId;
    }

    protected override void SelectSkin()
    {
        if (BuySkinButton.GetSelectedSkinCardType() != controllerSkinType)
            return;
        base.SelectSkin();
        Bank.Instance.playerInfo.selectedHairColorId = selectedSkinId;       //Сохранение
        YandexSDK.Save();
    }
    
    public override void SaveBoughtSkinState(int id)
    {
        skinsBuyState[id] = true;
        Bank.Instance.playerInfo.hairColorsBuyStates[id] = true;
        YandexSDK.Save();
    }

    [ContextMenu("ChangeSpriteColors")]
    private void ChangeSkinCardsColors()
    {
        int cardIndex = 0;
        foreach(var card in skinCards)
        {
            card.ChangeSkinImageColor(skinsColorsStorage.GetColorsArray()[cardIndex]);
            cardIndex++;
        }
    }


}
