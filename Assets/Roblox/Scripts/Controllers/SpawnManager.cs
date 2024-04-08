using System;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField]
    private int levelNumber;
    [SerializeField]
    private CheckPoint[] checkPoints;
    private Transform currenCheckPointTransform;
    private int currentCheckPointNumber;

    [Header("Refs")]
    [SerializeField] 
    private PlayerController playerController;
    [SerializeField]
    private FadeScreen fadeScreen;
    private SoundController soundController;
    private AdvManager advManager;

    private FinalLevelController finalLevelController;

    private void OnEnable()
    {
        DeadZone.PlayerDead += OnPlayerDead;
        CheckPoint.SpawnPointSet += OnSpawnPointSet;
    }

    private void OnDisable()
    {
        DeadZone.PlayerDead -= OnPlayerDead;
        CheckPoint.SpawnPointSet -= OnSpawnPointSet;
    }
    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
        advManager = FindObjectOfType<AdvManager>();
        finalLevelController = FindObjectOfType<FinalLevelController>();

    }
    private void Start()
    {
        currentCheckPointNumber = Bank.Instance.playerInfo.currentLevelsCheckpointsNumbers[levelNumber];
        playerController.BlockPlayersInput(true);
        OnReachNewCheckpoint(currentCheckPointNumber);
        for (int i = 0; i < currentCheckPointNumber; i++)
        {
            checkPoints[i].DeactivateTrigger();
        }

        StartCoroutine(FirstSpawn());
    }
   
    IEnumerator FirstSpawn()
    {      
        fadeScreen.EnterLevelFadeOut();
        TransferPlayer();
        yield return new WaitForSeconds(fadeScreen.GetOutFadeDuration() * 0.7f);
        if(!PlayerController.IsBusy)
            playerController.BlockPlayersInput(false);
    }
    IEnumerator RespawnPlayer()
    {
        playerController.BlockPlayersInput(true);
        PlayerController.IsBusy = true;
        fadeScreen.StartInFadeScreenTween();
        yield return new WaitForSeconds(fadeScreen.GetInFadeDuration());
        TransferPlayer();
        yield return new WaitForSeconds(fadeScreen.GetOutFadeDuration() * 0.7f);
        playerController.BlockPlayersInput(false);
        PlayerController.IsBusy = false;
        advManager.StartCountToAdv();
    }
    private void OnSpawnPointSet(CheckPoint point)
    {
        int index = Array.IndexOf(checkPoints, point);
        OnReachNewCheckpoint(index);
        Bank.Instance.playerInfo.currentLevelsCheckpointsNumbers[levelNumber] = index;
    }
    void TransferPlayer()
    {
        playerController.transform.position = currenCheckPointTransform.position;
    }
    void OnPlayerDead()
    {
        soundController.Play("Death");
        StartCoroutine(RespawnPlayer());
    }
  
    void OnReachNewCheckpoint(int index)
    {
        currentCheckPointNumber = index;
        if (currentCheckPointNumber == checkPoints.Length - 1)
            finalLevelController.ShowPredfinalPanel();
        currenCheckPointTransform = checkPoints[currentCheckPointNumber].GetSpawnCoordinates();
    }
    public int GetLevelNumber()
    {
        return levelNumber;
    }

}
