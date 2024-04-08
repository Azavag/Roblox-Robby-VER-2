using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetSkinButtonsController : SkinButtonController
{
    [SerializeField]
    private SkinType skinType;
    private SoundController soundController;
    [SerializeField]
    private SkinCard[] skinCards;
    [SerializeField]
    private SkinsObjectsStorage skinsObjectsStorage;

    private int selectedSkinId;
    private bool[] skinsBuyState;


    [Header("Model Buttons")]
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private Button selectButton;
    [SerializeField]
    private Button adsButton;
    [SerializeField]
    private GameObject selectedText;
    SkinCard clickedSkinCard;

    private void OnEnable()
    {
        PetSkinCard.PetCardClicked += OnSkinButtonClicked;
        selectButton.onClick.AddListener(OnClickSelectButton);
    }
    private void OnDisable()
    {
        PetSkinCard.PetCardClicked -= OnSkinButtonClicked;
        selectButton.onClick.RemoveListener(OnClickSelectButton);
    }
    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
    }
    void Initialization()
    {
        skinsBuyState = Bank.Instance.playerInfo.petSkinsBuyStates;
        selectedSkinId = Bank.Instance.playerInfo.selectedPetId;
    }
    void Start()
    {
        Initialization();
        for (int i = 0; i < skinCards.Length; i++)
        {
            if (skinsBuyState[i])
                skinCards[i].Unclock();
        }
        ShowSkinObject(selectedSkinId);

        clickedSkinCard = skinCards[selectedSkinId];
        clickedSkinCard.Select();
        clickedSkinCard.Highlight();
    }

    public void OnSkinButtonClicked(SkinCard skinEntity)
    {
        soundController.Play("CardClick");
        clickedSkinCard = skinEntity;
        foreach (var entity in skinCards)
        {
            entity.UnHighlight();
        }
        clickedSkinCard.Highlight();
        ShowCurrentModelView(clickedSkinCard);
        ShowSkinObject(clickedSkinCard.GetSkinIdNumber());
    }

    public void ShowCurrentModelView(SkinCard skinCard)
    {
        buyButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(false);
        selectedText.gameObject.SetActive(false);
        adsButton.gameObject.SetActive(false);

        if (skinCard.isAdsReward && !skinCard.isBought)
            adsButton.gameObject.SetActive(true);
        if (!skinCard.isBought)
        {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text =
                skinCard.GetSkinPrice().ToString();
        }
        else
        {
            if (!skinCard.isSelected)
                selectButton.gameObject.SetActive(true);
            else selectedText.gameObject.SetActive(true);
        }
    }

    void OnClickSelectButton()
    {
        if (BuySkinButton.GetSelectedSkinCardType() != skinType)
            return;
        foreach (var entity in skinCards)
        {
            entity.Unselect();
        }
        soundController.Play("SelectSkin");
        clickedSkinCard.Select();
        ShowCurrentModelView(clickedSkinCard);
        selectedSkinId = clickedSkinCard.GetSkinIdNumber();
        ShowSkinObject(selectedSkinId);
        Bank.Instance.playerInfo.selectedPetId = selectedSkinId;
        YandexSDK.Save();

    }
    public void SaveStates(int id)
    {
        skinsBuyState[id] = true;
        Bank.Instance.playerInfo.petSkinsBuyStates = skinsBuyState;
        YandexSDK.Save();
    }
    void ShowSkinObject(int id)
    {
        skinsObjectsStorage.ShowSkinObject(id);
    }
    public void ResetSkin()
    {
        foreach (var card in skinCards)
        {
            card.UnHighlight();
        }
        skinCards[selectedSkinId].Highlight();
        clickedSkinCard = skinCards[selectedSkinId];
        ShowCurrentModelView(skinCards[selectedSkinId]);
        ShowSkinObject(selectedSkinId);
    }
}
