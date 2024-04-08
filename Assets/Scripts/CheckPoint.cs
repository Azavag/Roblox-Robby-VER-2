using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static event Action<CheckPoint> SpawnPointSet;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DeactivateTrigger();
        SpawnPointSet?.Invoke(this);
    }

    public void DeactivateTrigger()
    {
        GetComponent<Collider>().enabled = false;
    }
    public Transform GetSpawnCoordinates()
    {
        return transform;
    }
}
