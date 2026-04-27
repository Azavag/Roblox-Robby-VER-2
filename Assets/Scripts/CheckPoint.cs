using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private const string PlayerTag = "Player";
    public static event Action<CheckPoint> SpawnPointSet;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PlayerTag))
            return;

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
