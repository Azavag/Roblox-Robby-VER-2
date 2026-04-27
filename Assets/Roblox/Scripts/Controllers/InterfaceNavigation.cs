using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceNavigation : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField]
    private Canvas mainCanvas;
    [SerializeField]
    private Canvas fadeScreenCanvas;
    [SerializeField]
    private Canvas pauseCanvas;
    [SerializeField]
    private Canvas settingsCanvas;
    [SerializeField]
    private Canvas advAlertCanvas;
    [SerializeField]
    private Canvas joystickCanvas;
    [SerializeField]
    private Canvas finalCanvas;

    [Header("UI elements")]
    [SerializeField]
    private Button pauseMenuButton;
    [SerializeField]
    private Button closePauseMenuButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button closeSettingsButton;


    private PlayerController playerController;
    private SoundController soundController;
    private SpawnManager spawnManager;

    public static bool isPauseMenuOpen;
    private bool isSettingsOpen;

    private void OnEnable()
    {
        pauseMenuButton.onClick.AddListener(OpenPauseMenu);
        closePauseMenuButton.onClick.AddListener(ClosePauseMenu);
        mainMenuButton.onClick.AddListener(OpenMainMenu);
        settingsButton.onClick.AddListener(() => OpenSettingsMenu(true));
        closeSettingsButton.onClick.AddListener(() => OpenSettingsMenu(false));
    }
    private void OnDisable()
    {
        pauseMenuButton.onClick.RemoveListener(OpenPauseMenu);
        closePauseMenuButton.onClick.RemoveListener(ClosePauseMenu);
        mainMenuButton.onClick.RemoveListener(OpenMainMenu);
        settingsButton.onClick.RemoveAllListeners();
        closeSettingsButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isPauseMenuOpen)
            {
                OpenPauseMenu();
                return;
            }
            ClosePauseMenu();
        }
    }
    private void Awake()
    {
        isPauseMenuOpen = false;
        Initialize();
        playerController = FindObjectOfType<PlayerController>();
        soundController = FindObjectOfType<SoundController>();
        spawnManager = FindAnyObjectByType<SpawnManager>();
        DOTween.SetTweensCapacity(500, 50);
    }

    void Initialize()
    {
        ToggleCanvas(mainCanvas, true);
        ToggleCanvas(fadeScreenCanvas, false);
        ToggleCanvas(pauseCanvas, false);
        ToggleCanvas(settingsCanvas, false);
        ToggleCanvas(finalCanvas, false);
        ToggleJoystickCanvas(true);
        ToggleAdvAlertCanvas(false);
        
    }

    private void Start()
    {
        CursorLocking.LockCursor(true);
    }

    public void TogglePauseCanvas(bool state)
    {
        ToggleCanvas(pauseCanvas, state);
    }
  
    public void ToggleSettingsCanvas(bool state)
    {
        ToggleCanvas(settingsCanvas, state);
    }
  
    public void ToggleFinalCanvas(bool state)
    {
        ToggleCanvas(finalCanvas, state);
    }

    public void ToggleFadeScreenCanvas(bool state)
    {
        ToggleCanvas(fadeScreenCanvas, state);
    }   
  
    public void ToggleAdvAlertCanvas(bool state)
    {
        ToggleCanvas(advAlertCanvas, state);
    }

    public void ToggleJoystickCanvas(bool state)
    {
        if (IsMobileController.IsMobile)
            ToggleCanvas(joystickCanvas, state);

    }
    void OpenSettingsMenu(bool state)
    {
        ToggleCanvas(settingsCanvas, state);
        soundController.MakeClickSound();
        isSettingsOpen = state;
    }
    void OpenPauseMenu()
    {
        if (isPauseMenuOpen || AdvManager.isAdvOpen || PlayerController.IsBusy)
            return;
        isPauseMenuOpen = true;
        soundController.MakeClickSound();
        TogglePauseCanvas(true);
        CursorLocking.LockCursor(false);
        playerController.BlockPlayersInput(true);
    }

    void ClosePauseMenu()
    {
        if(isSettingsOpen || PlayerController.IsBusy)
            return;
        soundController.MakeClickSound();
        TogglePauseCanvas(false);
        SoundController.SaveVolumeSetting();
        CursorLocking.LockCursor(true);
        playerController.BlockPlayersInput(false);
        isPauseMenuOpen = false;
    }
    void ToggleCanvas(Canvas canvas, bool state)
    {
        canvas.gameObject.SetActive(state);
    }

    void OpenMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void FinishLevel()
    {
        Bank.Instance.playerInfo.currentLevelsCheckpointsNumbers[spawnManager.GetLevelNumber()] = 0;
        Bank.Instance.playerInfo.currentLevelsCoinsNumbers[spawnManager.GetLevelNumber()] = -1;
        YandexSDK.Save();
        PlayerController.IsBusy = false;
        OpenMainMenu();
    }

}
