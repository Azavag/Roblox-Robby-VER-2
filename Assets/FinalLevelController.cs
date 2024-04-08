using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinalLevelController : MonoBehaviour
{
    enum Weapons
    { 
        knifew, 
        hyt
    }
    Weapons weapons;
    [Header("Refs")]
    private InterfaceNavigation interfaceNavigation;
    [SerializeField]
    private PlayerController playerController;
    private SoundController soundController;
    private SpawnManager spawnManager;
    [Header("UI")]
    [SerializeField]
    private Button closePredfinalPanelButton;
    [SerializeField]
    private Button closeFinalPanelButton;
    [Header("Refs")]
    [SerializeField]
    private Transform[] triggerTransforms;


    [SerializeField]
    private int finalTasksCount = 5;
    private int finalTasksCounter;
    bool[] tempCointStates = new bool[0];
    private void OnEnable()
    {
        closePredfinalPanelButton.onClick.AddListener(ClosePredfinalPanel);
        closeFinalPanelButton.onClick.AddListener(CloseFinalPanel);
        Wallet.CoinsChanged += OnCoinsChanged;
    }


    private void OnDisable()
    {
        closePredfinalPanelButton.onClick.RemoveListener(ClosePredfinalPanel);
        closeFinalPanelButton.onClick.RemoveListener(CloseFinalPanel);
        Wallet.CoinsChanged -= OnCoinsChanged;
    }
    private void Awake()
    {
        interfaceNavigation = FindObjectOfType<InterfaceNavigation>();
        soundController = FindObjectOfType<SoundController>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }
    private void Start()
    {
        ToggleTriggers(false);
    }

    public void ShowPredfinalPanel()
    {      
        StartCoroutine(ShowPredFinalPanelRoutine());
    }
    private IEnumerator ShowPredFinalPanelRoutine()
    {
        playerController.BlockPlayersInput(true);
        PlayerController.IsBusy = true;
        CursorLocking.LockCursor(false);
        yield return new WaitForSeconds(1f);
        interfaceNavigation.TogglePredfinalCanvas(true);       
    }

    void ToggleTriggers(bool state)
    {
        foreach(var trigger in triggerTransforms)
        {
            trigger.GetComponent<Collider>().enabled = state;
            if(state)
                trigger.GetComponentInChildren<ParticleSystem>().Play();
            else trigger.GetComponentInChildren<ParticleSystem>().Stop();
        }
            
    }

    private void ClosePredfinalPanel()
    {
        CursorLocking.LockCursor(true);
        playerController.BlockPlayersInput(false);
        interfaceNavigation.TogglePredfinalCanvas(false);
        PlayerController.IsBusy = false;
        ToggleTriggers(true);
    }
    private IEnumerator ShowFinalPanelRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerController.IsBusy = true;
        CursorLocking.LockCursor(false);
        playerController.BlockPlayersInput(true);
        soundController.Play("Finish");
        yield return new WaitForSeconds(1f);
        interfaceNavigation.ToggleFinalCanvas(true);
    }
    private void CloseFinalPanel()
    {
        interfaceNavigation.TogglePredfinalCanvas(false);
        interfaceNavigation.FinishLevel();
    }
    private void OnCoinsChanged()
    {        
        finalTasksCounter = 0;
        switch (spawnManager.GetLevelNumber())
        {
            case 0:
                tempCointStates = Bank.Instance.playerInfo.areCoinsCollect;
                break;
            case 1:
                tempCointStates = Bank.Instance.playerInfo.areCoinsCollect_2;
                break;
        }

        for (int i = tempCointStates.Length - finalTasksCount; i < tempCointStates.Length; i++)
        {
            if (tempCointStates[i])
                finalTasksCounter++;         
            if (finalTasksCounter == finalTasksCount)
                StartCoroutine(ShowFinalPanelRoutine());
        }
    }
}
