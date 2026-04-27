using UnityEngine;

public class MainShopPage : SkinShopPage
{
    [SerializeField]
    private GameObject closeButton;

    public override void ClosePage()
    {
        if (closeButton != null)
            SetCloseButtonVisibility(false);
    }

    public override void OpenPage()
    {
        if (closeButton != null)
            SetCloseButtonVisibility(true);
    }

    private void SetCloseButtonVisibility(bool isVisible)
    {
        closeButton.SetActive(isVisible);      
    }

}