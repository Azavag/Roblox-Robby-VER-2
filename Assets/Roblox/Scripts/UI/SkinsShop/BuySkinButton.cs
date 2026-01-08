using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BuySkinButton : MonoBehaviour
{
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private Button adsButton;
    [SerializeField]
    private ParticleSystem confirmBuySkinParticles;


    [Header("Refs")]
    private SoundController soundController;
    private static SkinCard selectedSkinCard;
    private AdvManager advManager;
    private HatSkinButtonsController hatCardsController;
    private PetSkinsButtonController petCardsController;
    private ShirtColorSkinButtonsController shirtCardsController;
    private PantsColorSkinButtonsController pantsCardsController;
    private AccessoriesSkinButtonsController accessoriesCardsController;
    private HairSkinsButtonController hairSkinButtonsController;
    private BagSkinButtonsController bagSkinButtonController;
    private HairColorsSkinButtonsController hairColorsSkinButtonsController;

    Tween shakeTween;

    bool isAdsRewarded;
    public static event Action<int> SkinPurchaseMade;

    private void OnEnable()
    {
        SkinCard.cardClicked += SetSelectedSkinCardType;

        adsButton.onClick.AddListener(OnClickAdsButton);
        buyButton.onClick.AddListener(OnClickBuyButton);
    }
    private void OnDisable()
    {
        SkinCard.cardClicked -= SetSelectedSkinCardType;

        adsButton.onClick.RemoveListener(OnClickAdsButton);
        buyButton.onClick.RemoveListener(OnClickBuyButton);
    }
    private void OnDestroy()
    {
        shakeTween.Kill();
    }

    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
        advManager = FindObjectOfType<AdvManager>();

        hatCardsController = GetComponentInChildren<HatSkinButtonsController>();
        petCardsController = GetComponentInChildren<PetSkinsButtonController>();
        shirtCardsController = GetComponentInChildren<ShirtColorSkinButtonsController>();
        pantsCardsController = GetComponentInChildren<PantsColorSkinButtonsController>();
        accessoriesCardsController = GetComponentInChildren<AccessoriesSkinButtonsController>();
        hairSkinButtonsController = GetComponentInChildren<HairSkinsButtonController>();
        bagSkinButtonController = GetComponentInChildren<BagSkinButtonsController>();
        hairColorsSkinButtonsController = GetComponentInChildren<HairColorsSkinButtonsController>();
    }
    private void Start()
    {
        shakeTween = ButtonAnimation.ShakeAnimation(buyButton.transform);
        if(confirmBuySkinParticles == null)
        {
            Debug.LogError("No particles effect");
        }
    }
    void SetSelectedSkinCardType(SkinCard skinCard)
    {
        selectedSkinCard = skinCard;
    }

    public static SkinType GetSelectedSkinCardType()
    {
        return selectedSkinCard.GetSkinType();
    }

    void OnClickBuyButton()
    {       
        if (Bank.Instance.playerInfo.moneyCount >= selectedSkinCard.GetSkinPrice())
        {         
            ConfirmBuy();
            SkinPurchaseMade?.Invoke(-selectedSkinCard.GetSkinPrice());
            return;
        }
        DenyBuy();
    }
    void OnClickAdsButton()
    {
#if UNITY_EDITOR
        ConfirmBuy();
#endif
        advManager.ShowRewardedAdv();       
    }
    void DenyBuy()
    {
        soundController.Play("DeclineBuy");
        shakeTween.Pause();
        shakeTween.Rewind();
        shakeTween.Play();
    }
    void ConfirmBuy()
    {
        soundController.Play("ConfirmBuy");
        confirmBuySkinParticles.Play();
        selectedSkinCard.Unlock();

        switch (selectedSkinCard.GetSkinType())
        {
            case SkinType.Hat:
                hatCardsController.ShowCurrentCardModelView(selectedSkinCard);
                hatCardsController.SaveBoughtSkinState(selectedSkinCard.GetSkinIdNumber());
                break;
            case SkinType.Pet:
                petCardsController.ShowCurrentCardModelView(selectedSkinCard);
                petCardsController.SaveBoughtSkinState(selectedSkinCard.GetSkinIdNumber());
                break;         
            case SkinType.Shirt:
                shirtCardsController.ShowCurrentCardModelView(selectedSkinCard);
                shirtCardsController.SaveBoughtSkinState(selectedSkinCard.GetSkinIdNumber());
                break;
            case SkinType.Pants:
                pantsCardsController.ShowCurrentCardModelView(selectedSkinCard);
                pantsCardsController.SaveBoughtSkinState(selectedSkinCard.GetSkinIdNumber());
                break;          
            case SkinType.HairStyles:
                hairSkinButtonsController.ShowCurrentCardModelView(selectedSkinCard);
                hairSkinButtonsController.SaveBoughtSkinState(selectedSkinCard.GetSkinIdNumber());
                break;
            case SkinType.HairColors:
                hairColorsSkinButtonsController.ShowCurrentCardModelView(selectedSkinCard);
                hairColorsSkinButtonsController.SaveBoughtSkinState(selectedSkinCard.GetSkinIdNumber());
                break;
            case SkinType.Accessories:
                accessoriesCardsController.ShowCurrentCardModelView(selectedSkinCard);
                accessoriesCardsController.SaveBoughtSkinState(selectedSkinCard.GetSkinIdNumber());
                break;
            case SkinType.Bags:
                bagSkinButtonController.ShowCurrentCardModelView(selectedSkinCard);
                bagSkinButtonController.SaveBoughtSkinState(selectedSkinCard.GetSkinIdNumber());
                break;

        }
             
    }
    //Â jslib
    public void SetRewardingState()
    {
        isAdsRewarded = true;
    }
    //Â jslib
    public void UnlockRewardSkin()
    {
        if(isAdsRewarded)
            ConfirmBuy();
        isAdsRewarded = false;
    }
}
