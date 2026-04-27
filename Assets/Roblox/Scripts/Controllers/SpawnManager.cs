using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

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
    [SerializeField]
    private IntroManagment introManagment;

    private SoundController soundController;
    private AdvManager advManager;

    public event Action AllSpawnsReached;

    private void OnEnable()
    {
        DeadZone.PlayerDead += OnPlayerDead;
        PlayerController.PlayerDead += OnPlayerDead;
        CheckPoint.SpawnPointSet += OnSpawnPointSet;
        introManagment.IntroFinished += OnIntroFinished;
    }

    private void OnDisable()
    {
        DeadZone.PlayerDead -= OnPlayerDead;
        PlayerController.PlayerDead -= OnPlayerDead;
        CheckPoint.SpawnPointSet -= OnSpawnPointSet;
        introManagment.IntroFinished -= OnIntroFinished;
    }
    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
        advManager = FindObjectOfType<AdvManager>();
        introManagment = FindObjectOfType<IntroManagment>();
        PlayerController.IsBusy = true;
    }
    private void Start()
    {
        currentCheckPointNumber = Bank.Instance.playerInfo.currentLevelsCheckpointsNumbers[levelNumber];
        Debug.Log("currentCheckPointNumber = " + currentCheckPointNumber);
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
        yield return new WaitForSeconds(0.1f);
        if (introManagment != null)
        {
            introManagment.StartIntro();
        }
        TransferPlayer();
        CursorLocking.LockCursor(true);
        yield return new WaitForSeconds(fadeScreen.GetOutFadeDuration() * 0.7f);
    }
    private void OnIntroFinished()
    {
        PlayerController.IsBusy = false;
        playerController.BlockPlayersInput(false);
    }
    private IEnumerator RespawnPlayer()
    {
        playerController.BlockPlayersInput(true);
        PlayerController.IsBusy = true;
        fadeScreen.StartInFadeScreenTween();
        yield return new WaitForSeconds(fadeScreen.GetInFadeDuration());
        TransferPlayer();
        yield return new WaitForSeconds(fadeScreen.GetOutFadeDuration() * 0.7f);
        playerController.BlockPlayersInput(false);
        CursorLocking.LockCursor(true);
        PlayerController.IsBusy = false;
        advManager.StartCountToAdv();
    }
    private void OnSpawnPointSet(CheckPoint point)
    {
        int index = Array.IndexOf(checkPoints, point);
        if (index < 0)
        {
            Debug.LogWarning($"Checkpoint {point.name} was not found in SpawnManager list on level {levelNumber}.");
            return;
        }

        OnReachNewCheckpoint(index);
        Bank.Instance.playerInfo.currentLevelsCheckpointsNumbers[levelNumber] = index;

        if(index >= checkPoints.Length - 1)
        {
            AllSpawnsReached?.Invoke();
        }
    }

    private 

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
        currenCheckPointTransform = checkPoints[currentCheckPointNumber].GetSpawnCoordinates();
        Debug.Log($"CheckpointObject = {checkPoints[currentCheckPointNumber]}");
    }
    public int GetLevelNumber()
    {
        return levelNumber;
    }

}
