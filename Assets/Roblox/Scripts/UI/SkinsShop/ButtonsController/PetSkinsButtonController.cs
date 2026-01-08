

public class PetSkinsButtonController : SkinButtonController
{

    protected override void Start()
    {
        GetSavedData();
        base.Start();
    }

    protected override void GetSavedData()
    {
        skinsBuyState = Bank.Instance.playerInfo.petSkinsBuyStates;
        selectedSkinId = Bank.Instance.playerInfo.selectedPetId;
    }

    protected override void SelectSkin()
    {     
        base.SelectSkin();
        Bank.Instance.playerInfo.selectedPetId = selectedSkinId;
        YandexSDK.Save();
    }

    public override void SaveBoughtSkinState(int id)
    {
        skinsBuyState[id] = true;
        Bank.Instance.playerInfo.petSkinsBuyStates = skinsBuyState;
        YandexSDK.Save();
    }
}
