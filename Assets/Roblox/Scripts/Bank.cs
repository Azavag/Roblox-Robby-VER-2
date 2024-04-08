using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public float musicVolume = 0.5f;                //++
    public float effectsVolume = 0.5f;              //++
    public float sensivityValue = 1f;               //++
    
    
    //Скины
    public int selectedHatId = 0;                   //++
    public int selectedPetId = 0;                   //++
    public int selectedAccessoiresId = 0;
    public int selectedShirtId = 0;
    public int selectedPantsId = 0;
    public int selectedBagsId = 0;
    public int selectedHairId = 0;
    public int selectedHairColorId = 0;

    //public int levelsCheckpointStates = 1;
    public int spawnPointNumber = 0;
    public int moneyCount = 0;                           //++
    public bool[] areCoinsCollect = new bool[68];       //++
    public bool[] areCoinsCollect_2 = new bool[68];
    public bool[] colorsPantsBuyState = new bool[9];    //++
    public bool[] colorsShirtBuyState = new bool[9];    //++
    public bool[] specialsBuyState = new bool[7];       //++
    public string choosedPantsColor;        //++
    public string choosedShirtColor;        //++
    public string choosedSpecialColor;      //++

    public int[] currentLevelsCheckpointsNumbers = new int[2];

    public bool[] areSpawnpointsSet = new bool[51];
    //public bool[] are2LevelSpawnpointsSet = new bool[51];

    public bool[] hatSkinsBuyStates = new bool[90];                //++
    public bool[] hairSkinsBuyStates = new bool[62];
    public bool[] hairColorsBuyStates = new bool[42];
    public bool[] accessoriesSkinsBuyStates = new bool[19];
    public bool[] petSkinsBuyStates = new bool[43];                //++
    public bool[] shirtsSkinsBuyStates = new bool[42];
    public bool[] glovesSkinsBuyStates = new bool[42];
    public bool[] bagsSkinsBuyStates = new bool[25];
    public bool[] pantsSkinsBuyStates = new bool[42];
    public bool[] trailSkinsBuyStates = new bool[20];              //++
}

public class Bank : MonoBehaviour
{
    public static Bank Instance;
    public PlayerInfo playerInfo; 
    private YandexSDK yandexSDK;
    public static bool progressLoaded = false;
    private void Awake()
    {
        progressLoaded = false;
        yandexSDK = FindObjectOfType<YandexSDK>();
#if UNITY_EDITOR
        YandexSDK.dataIsLoaded = true;
    #endif
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            yandexSDK.Load();          
            return;
        }
        Destroy(gameObject);
    }
    private void Update()
    {
        if (progressLoaded)
            return;

        if (YandexSDK.dataIsLoaded)
        {
            Instance.playerInfo.hatSkinsBuyStates[0] = true;
            Instance.playerInfo.petSkinsBuyStates[0] = true;
            Instance.playerInfo.trailSkinsBuyStates[0] = true;
            Instance.playerInfo.accessoriesSkinsBuyStates[0] = true;
            Instance.playerInfo.shirtsSkinsBuyStates[0] = true;
            Instance.playerInfo.glovesSkinsBuyStates[0] = true;
            Instance.playerInfo.pantsSkinsBuyStates[0] = true;
            Instance.playerInfo.bagsSkinsBuyStates[0] = true;
            Instance.playerInfo.hairSkinsBuyStates[0] = true;
            Instance.playerInfo.hairColorsBuyStates[0] = true;

            if (Instance.playerInfo.currentLevelsCheckpointsNumbers[0] == 0)
            {
                Instance.playerInfo.currentLevelsCheckpointsNumbers[0] = Instance.playerInfo.spawnPointNumber;
                YandexSDK.Save();
            }           
            
            progressLoaded = true;
        }
    }
}
