using System;
using UnityEngine;

public class CoinObject : MonoBehaviour
{
    [SerializeField] 
    private int moneyForCollect;
    [SerializeField]
    private GameObject coinObject;
    public bool isAlive = true;

    public static event Action<CoinObject> CoinCollected;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Collecting();
    }

    void Collecting()
    {
        CoinCollected?.Invoke(this);
    }

    public int GetMoneyValue()
    {
        return moneyForCollect;
    }

    public void DeactivateCoin()
    {
        coinObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        isAlive = false;
    }
}
