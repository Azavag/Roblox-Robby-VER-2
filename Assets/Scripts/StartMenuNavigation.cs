using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuNavigation : MonoBehaviour
{
    [Header("UI Buttons")]
    [Header("Open Buttons")]
    [SerializeField]
    private Button levelChooseButton;
    [SerializeField]
    private Button settingButton;
    [SerializeField]
    private Button shopButton;
    [SerializeField]
    private Button linkButton;

    [Header("Close Buttons")]
    [SerializeField]
    private Button closeLevelChooseButton;
    [SerializeField]
    private Button closeSettingsButton;
    [SerializeField]
    private Button closeShopButton;

    [SerializeField]
    private Button level_1Button;
    [SerializeField]
    private Button level_2Button;

    [Header("Canvases")]
    [SerializeField]
    private Canvas startCanvas;
    [SerializeField]
    private Canvas levelChooseCanvas;
    [SerializeField]
    private Canvas settingsCanvas;
    [SerializeField]
    private Canvas shopCanvas;
    [SerializeField]
    private GameObject shopPlayerModel;

    private Link link;
    private SoundController soundController;

    private void OnEnable()
    {
        levelChooseButton.onClick.AddListener(()=>ToggleLevelNavigationMenu(true));
        settingButton.onClick.AddListener(()=>ToggleSettingsMenu(true));
        shopButton.onClick.AddListener(() => ToggleSkinShop(true));
        linkButton.onClick.AddListener(OpenDeveloperLink);

        closeLevelChooseButton.onClick.AddListener(() => ToggleLevelNavigationMenu(false));
        closeSettingsButton.onClick.AddListener(() => ToggleSettingsMenu(false));
        closeShopButton.onClick.AddListener(() => ToggleSkinShop(false));

        level_1Button.onClick.AddListener(() => OpenLevel(1));
        level_2Button.onClick.AddListener(() => OpenLevel(2));
    }
    private void OnDisable()
    {
        levelChooseButton.onClick.RemoveAllListeners();
        settingButton.onClick.RemoveAllListeners();
        shopButton.onClick.RemoveAllListeners();
        linkButton.onClick.RemoveListener(OpenDeveloperLink);

        closeLevelChooseButton.onClick.RemoveAllListeners();
        closeSettingsButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.RemoveAllListeners();

        level_1Button.onClick.RemoveListener(() => OpenLevel(1));
        level_2Button.onClick.RemoveListener(() => OpenLevel(2));
    }
    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
        link = FindObjectOfType<Link>();
    }
    
    void Start()
    {
        InitAllCanvas();
    }

    void InitAllCanvas()
    {
        ToggleCanvas(startCanvas, true);
        ToggleCanvas(levelChooseCanvas, false);
        ToggleCanvas(settingsCanvas, false);
        ToggleCanvas(shopCanvas, false);
        ToggleObject(shopPlayerModel, false);
    }

    public void ToggleLevelNavigationMenu(bool toggle)
    {
        ToggleCanvas(levelChooseCanvas, toggle);
        ToggleCanvas(startCanvas, !toggle);
        soundController.MakeClickSound();
    }
    public void ToggleSettingsMenu(bool toggle)
    {
        ToggleCanvas(settingsCanvas, toggle);
        ToggleCanvas(startCanvas, !toggle);
        soundController.MakeClickSound();
    }
    public void ToggleSkinShop(bool toggle)
    {
        ToggleCanvas(shopCanvas, toggle);
        ToggleCanvas(startCanvas, !toggle); 
        ToggleObject(shopPlayerModel, toggle);
        soundController.MakeClickSound();
    }

    void OpenDeveloperLink()
    {
        soundController.MakeClickSound();
        link.GotoDeveloperPage();
    }


    private void ToggleCanvas(Canvas canvas, bool state)
    {
        canvas.gameObject.SetActive(state);
    }
    private void ToggleObject(GameObject gameObject, bool state)
    {
        gameObject.SetActive(state);
    }

    void OpenLevel(int number)
    {
        SceneManager.LoadScene(number+1);
    }
}
