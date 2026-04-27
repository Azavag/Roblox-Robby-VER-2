using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkinButtonController : MonoBehaviour
{
    [Header("Skin setup")]
    [SerializeField]
    protected SkinType controllerSkinType;
    [SerializeField]
    protected SkinCard[]skinCards;
    [SerializeField]
    private SkinsStorage skinsStorage;

    [Header("Model Buttons")]
    [SerializeField]
    protected Button buyButton;
    [SerializeField]
    protected Button selectButton;
    [SerializeField]
    protected Button adsButton;
    [SerializeField]
    protected GameObject selectedText;

    protected int selectedSkinId;
    protected bool[] skinsBuyState;
    protected SkinCard clickedSkinCard;
    protected SoundController soundController;

    protected virtual void OnEnable()
    {
        SkinCard.cardClicked += OnSkinCardClicked;
        selectButton.onClick.AddListener(SelectSkin);
    }
    protected virtual void OnDisable()
    {
        SkinCard.cardClicked -= OnSkinCardClicked;
        selectButton.onClick.RemoveListener(SelectSkin);
    }

    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
    }
    protected virtual void Start()
    {
        if (skinsBuyState.Length != skinCards.Length)
            Debug.LogError($"{name} Length problem");

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

    protected virtual void GetSavedData()
    {

    }

    private void OnSkinCardClicked(SkinCard clickedSkinCard)
    {
        if (clickedSkinCard.GetSkinType() != controllerSkinType)
            return;

        soundController.Play("CardClick");
        this.clickedSkinCard = clickedSkinCard;
        foreach (var card in skinCards)
        {
            card.UnHighlight();
        }
        this.clickedSkinCard.Highlight();
        ShowCurrentCardModelView(this.clickedSkinCard);
        //ShowSkinObject(this.clickedSkinCard.GetSkinIdNumber());
        int lastClickedCardOIndex = Array.IndexOf(skinCards, clickedSkinCard);
        ShowSkinObject(lastClickedCardOIndex);
    }

    public void ShowCurrentCardModelView(SkinCard skinCard)
    {
        buyButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(false);
        selectedText.gameObject.SetActive(false);
        adsButton.gameObject.SetActive(false);

        if (skinCard.isAdsReward && !skinCard.isBought)
        {
            adsButton.gameObject.SetActive(true);
            return;
        }    

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
    protected virtual void SelectSkin()
    {
        if (BuySkinButton.GetSelectedSkinCardType() != controllerSkinType)
            return;

        foreach (var card in skinCards)     //Можно оптимизировать
        {
            card.Unselect();
        }
        soundController.Play("SelectSkin");
        clickedSkinCard.Select();
        ShowCurrentCardModelView(clickedSkinCard);
        //selectedSkinId = clickedSkinCard.GetSkinIdNumber();
        selectedSkinId = Array.IndexOf(skinCards, clickedSkinCard);
        ShowSkinObject(selectedSkinId);
    }

    protected void ShowSkinObject(int id)
    {
        skinsStorage.ShowSkinObject(id);
    }

    public virtual void SaveBoughtSkinState(int id)
    {

    }

    public virtual void DropSkin()
    {
        clickedSkinCard.Unselect();
        selectedSkinId = 0;
        clickedSkinCard = skinCards[selectedSkinId];
        clickedSkinCard.Select();
        ResetSkin();
    }
    public void ResetSkin()
    {
        foreach (var card in skinCards)
        {
            card.UnHighlight();
        }
        skinCards[selectedSkinId].Highlight();
        clickedSkinCard = skinCards[selectedSkinId];
        ShowSkinObject(selectedSkinId);
        ShowCurrentCardModelView(skinCards[selectedSkinId]);
    }


}

