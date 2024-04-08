using Unity.VisualScripting;
using UnityEngine;

public class SkinRealisation : MonoBehaviour
{

    [SerializeField]
    private SkinsObjectsStorage hatsObjectsStorage;
    [SerializeField]
    private SkinsObjectsStorage hairObjectsStorage;
    [SerializeField]
    private SkinsObjectsStorage accessoiresObjectsStorage;
    [SerializeField]
    private SkinsObjectsStorage bagsObjectsStorage;
    [SerializeField]
    private SkinsObjectsStorage petObjectsStorage;

    [SerializeField]
    private SkinsColorsStorage shirtColorsStorage;
    [SerializeField]
    private SkinsColorsStorage pantsColorsStorage;
    [SerializeField]
    private SkinsColorsStorage hairColorsStorage;

  
    private void Start()
    {
        WearAllSkins();
    }

    void WearAllSkins()
    {
        hatsObjectsStorage.ShowSkinObject(Bank.Instance.playerInfo.selectedHatId);
        hairObjectsStorage.ShowSkinObject(Bank.Instance.playerInfo.selectedHairId);
        bagsObjectsStorage.ShowSkinObject(Bank.Instance.playerInfo.selectedBagsId);
        accessoiresObjectsStorage.ShowSkinObject(Bank.Instance.playerInfo.selectedAccessoiresId);
        petObjectsStorage.ShowSkinObject(Bank.Instance.playerInfo.selectedPetId);

        shirtColorsStorage.ShowSkinObject(Bank.Instance.playerInfo.selectedShirtId);
        pantsColorsStorage.ShowSkinObject(Bank.Instance.playerInfo.selectedPantsId);
        hairColorsStorage.ShowSkinObject(Bank.Instance.playerInfo.selectedHairColorId);

    }
}
