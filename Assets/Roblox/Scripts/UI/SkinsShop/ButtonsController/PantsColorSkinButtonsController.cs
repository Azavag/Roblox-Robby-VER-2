using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PantsColorSkinButtonsController : SkinButtonController
{
    [SerializeField]
    private SkinsColorsStorage skinsColorsStorage;
    private Color[] pantsColors;

    private void OnValidate()
    {
        Initialization();
    }

    void Initialization()
    {
        if (skinsColorsStorage != null)
        {
            pantsColors = skinsColorsStorage.GetColorsArray();
            for (int i = 0; i < skinCards.Length; i++)
            {
                skinCards[i].ChangeSkinImageColor(pantsColors[i]);
            }
        }
    }

    protected override void Start()
    {
        GetSavedData();
        base.Start();
        Initialization();
    }

    protected override void GetSavedData()
    {
        skinsBuyState = Bank.Instance.playerInfo.pantsSkinsBuyStates;
        selectedSkinId = Bank.Instance.playerInfo.selectedPantsId;
    }

    protected override void SelectSkin()
    {
        if (BuySkinButton.GetSelectedSkinCardType() != controllerSkinType)
            return;
        base.SelectSkin();
        Bank.Instance.playerInfo.selectedPantsId = selectedSkinId;       //Сохранение
        YandexSDK.Save();
    }

    public override void SaveBoughtSkinState(int id)
    {
        skinsBuyState[id] = true;
        Bank.Instance.playerInfo.pantsSkinsBuyStates[id] = true;
        YandexSDK.Save();
    }
}
