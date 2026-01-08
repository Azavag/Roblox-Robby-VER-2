using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SkinType
{
    Hat,
    Pet,
    Shirt,
    Pants,
    HairStyles,
    Accessories,
    Bags,
    HairColors,
}
public class SkinCard : MonoBehaviour, IPointerClickHandler
{
    [Header("Stats")]
    public SkinScriptableObject skinScriptable;
    protected SkinType skinType;
    [SerializeField]
    protected int idNumber;
    public bool isSelected { private set; get; }
    public bool isBought { private set; get; }
    private int price;
    public bool isAdsReward;

    [Header("Card Components")]
    [SerializeField]
    private GameObject lockImage;
    [SerializeField]
    private GameObject selectedImage;
    [SerializeField]
    private GameObject priceObject;
    [SerializeField]
    private GameObject adsObject;
    [SerializeField]
    protected Image skinImage;

    [Header("SkinsButtonBackgrounds")]
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Sprite standartBackgroundSprite;
    [SerializeField]
    private Sprite selectedBackgroundSprite;

    public static event Action<SkinCard> cardClicked;

    private void Awake()
    {
        priceObject.SetActive(true);
        lockImage.SetActive(true);
        selectedImage.SetActive(false);

        idNumber = skinScriptable.idNumber;
        skinType = skinScriptable.skinType;
        price = skinScriptable.price;
        skinImage.sprite = skinScriptable.sprite;
        priceObject.GetComponentInChildren<TextMeshProUGUI>().text = price.ToString();
        isAdsReward = skinScriptable.isAdsReward;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        cardClicked?.Invoke(this);
    }

    private void OnValidate()
    {
        idNumber = skinScriptable.idNumber;
        skinType = skinScriptable.skinType;
        price = skinScriptable.price;
        skinImage.sprite = skinScriptable.sprite;
        priceObject.GetComponentInChildren<TextMeshProUGUI>().text = price.ToString();
        isAdsReward = skinScriptable.isAdsReward;
    }
    private void OnEnable()
    {
        if(!isAdsReward)
        {
            adsObject.SetActive(false);
        }
        else priceObject.SetActive(false);
        if (isBought)
        {
            lockImage.SetActive(false);
            priceObject.SetActive(false);
            adsObject.SetActive(false);
        }
        
        if (isSelected)
        {
            selectedImage.SetActive(true);
        }
    }

    public void Unlock()
    {
        isBought = true;
        lockImage.SetActive(false);
        priceObject.SetActive(false);
        adsObject.SetActive(false);
    }
    public void Select()
    {
        if (!isBought)
            return;
        isSelected = true;
        selectedImage.gameObject.SetActive(true);
    }

    public void Unselect()
    {
        isSelected = false;
        selectedImage.gameObject.SetActive(false);
    }

    public void ChangeSkinImageColor(Color color)
    {
        skinImage.color = color;
    }

    public void Highlight() => backgroundImage.sprite = selectedBackgroundSprite;
    public void UnHighlight() => backgroundImage.sprite = standartBackgroundSprite;

    public SkinType GetSkinType()
    {
        return skinType;
    }
    public int GetSkinPrice()
    {
        return price;
    }
    public int GetSkinIdNumber()
    {
        return skinScriptable.idNumber;
    }
}
