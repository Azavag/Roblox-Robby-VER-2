using UnityEngine;

public class SkinShopACtivation : MonoBehaviour
{
    [SerializeField]
    private GameObject skinShopObject;

    private void Awake()
    {
        skinShopObject.SetActive(false);
    }
    void Update()
    {
        if (!Bank.progressLoaded)
            return;
        if (Bank.progressLoaded)
        {
            skinShopObject.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
