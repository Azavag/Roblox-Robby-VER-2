using UnityEngine;
using UnityEngine.UI;


public class SkinShop : MonoBehaviour
{
    [Header("Refs")]
    private HatSkinButtonsController hatSkinButtonsController;
    private PetSkinsButtonController petSkinButtonsController;
    private ShirtColorSkinButtonsController shirtSkinButtonsController;
    private PantsColorSkinButtonsController pantsSkinButtonsController;
    private AccessoriesSkinButtonsController àccessoriesSkinButtonController;
    private HairSkinsButtonController hairSkinButtonsController;
    private BagSkinButtonsController bagSkinButtonController;
    private HairColorsSkinButtonsController hairColorsSkinButtonsController;

    private SoundController soundController;

    [Header("Shop UI")]
    [SerializeField]
    private Button closeSkinShopButton;
    [SerializeField]
    private GameObject buyWindow;

    int lastOpenedPage = 0;

    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();

        hatSkinButtonsController = GetComponentInChildren<HatSkinButtonsController>();
        petSkinButtonsController = GetComponentInChildren<PetSkinsButtonController>();
        shirtSkinButtonsController = GetComponentInChildren<ShirtColorSkinButtonsController>();
        pantsSkinButtonsController = GetComponentInChildren<PantsColorSkinButtonsController>();
        àccessoriesSkinButtonController = GetComponentInChildren<AccessoriesSkinButtonsController>();
        hairSkinButtonsController = GetComponentInChildren<HairSkinsButtonController>();
        bagSkinButtonController = GetComponentInChildren<BagSkinButtonsController>();
        hairColorsSkinButtonsController = GetComponentInChildren<HairColorsSkinButtonsController>();
        
    }
    private void OnEnable()
    {
        ShopMenuNavigation.TabSelected += OnTabSelected;
        closeSkinShopButton.onClick.AddListener(CloseSkinShop);
    }
    private void OnDisable()
    {
        ShopMenuNavigation.TabSelected -= OnTabSelected;
        closeSkinShopButton.onClick.RemoveListener(CloseSkinShop);
    }

    public void OpenSkinShop()
    {
        soundController.MakeClickSound();
    }

    public void CloseSkinShop()
    {
        ResetPages();      
    }

    public void ResetPages()
    {
        switch (lastOpenedPage)
        { 
            case 0:
                ResetHatSkin();
                ResetHairSkin();
                break;
            case 1:
                ResetHairSkin();
                ResetHatSkin();
                break;
            case 2:
                ResetÀccessoriesSkin();
                break;
            case 3:
                ResetPetSkin();
                break;
            case 4:
                ResetShirtSkin();
                break;
            case 5:
                ResetBagSkin();
                break;
            case 6:
                ResetPantsSkin();
                break;
        }

    }
    //Íà êàêóþ âêëàäêó ïåðåêëþ÷èëèñü
    void OnTabSelected(int index)
    {
        soundController.MakeClickSound();      
        
        switch (index)
        {
            //Íà ñòðàíèöó âûáîðà îïöèé âîëîñ
            case -2:
                ToggleBuyWindow(false);
                lastOpenedPage = 1;
                return;
            case -1:
                ToggleBuyWindow(false);
                ResetPages();
                return;
            case 0:
                break;      
            default:               
                break;
        }
        ToggleBuyWindow(true);
        lastOpenedPage = index;
    }

    void ResetPetSkin()
    {
        petSkinButtonsController.ResetSkin();
    }
    void ResetHatSkin()
    {
        hatSkinButtonsController.ResetSkin();       
    }

    void ResetShirtSkin()
    {
        shirtSkinButtonsController.ResetSkin();
    }
    
    void ResetPantsSkin()
    {
        pantsSkinButtonsController.ResetSkin();
    }

    void ResetÀccessoriesSkin()
    {
        àccessoriesSkinButtonController.ResetSkin();
    }

    void ResetHairSkin()
    {
        hairSkinButtonsController.ResetSkin();
        hairColorsSkinButtonsController.ResetSkin();
    }

    void ResetBagSkin()
    {
        bagSkinButtonController.ResetSkin();
    }

    void ToggleBuyWindow(bool toggle)
    {
        buyWindow.gameObject.SetActive(toggle);
    }
}
