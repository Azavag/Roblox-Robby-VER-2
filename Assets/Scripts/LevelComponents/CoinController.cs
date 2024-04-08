using System;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] 
    private int moneyForCollect;
    [SerializeField]
    private GameObject coinObject;

    public static event Action<CoinController> CoinCollected;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Collecting();
    }

    void Collecting()
    {
        CoinCollected?.Invoke(this);
        DeactivateCoin();
    }

    public int GetMoneyValue()
    {
        return moneyForCollect;
    }

    public void DeactivateCoin()
    {
        coinObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
    }
}
