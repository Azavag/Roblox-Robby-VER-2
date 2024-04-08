using TMPro;
using UnityEngine;

public class MainCanvasUpdate : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI coinsText;

    private void OnEnable()
    {     
        Wallet.CoinsChanged += OnCoinsValueChanged;
    }
    private void OnDisable()
    {     
        Wallet.CoinsChanged -= OnCoinsValueChanged;
    }

    void OnCoinsValueChanged()
    {
        coinsText.text = Bank.Instance.playerInfo.moneyCount.ToString();
    }
}
