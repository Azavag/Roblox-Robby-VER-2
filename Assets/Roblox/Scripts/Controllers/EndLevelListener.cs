using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelListener : MonoBehaviour
{
    [SerializeField]
    private SpawnManager spawnManager;
    [SerializeField]
    private FinalCanvas finishLevelCanvas;
    [SerializeField]
    private PlayerController playerController;

    private void OnEnable()
    {
        spawnManager.AllSpawnsReached += SpawnManager_AllSpawnsReached;
    }
    private void OnDisable()
    {
        spawnManager.AllSpawnsReached -= SpawnManager_AllSpawnsReached;
    }

    private void SpawnManager_AllSpawnsReached()
    {
        PlayerController.IsBusy = true;
        CursorLocking.LockCursor(false);
        playerController.BlockPlayersInput(true);
        StartCoroutine(ShowFinishCanvasRoutine());
    }

    private IEnumerator ShowFinishCanvasRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        finishLevelCanvas.gameObject.SetActive(true);
    }

}
