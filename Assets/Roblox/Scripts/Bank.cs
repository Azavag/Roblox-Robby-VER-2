using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    //Настройки
    public float musicVolume = 0.5f;                //++
    public float effectsVolume = 0.5f;              //++
    public float sensivityValue = 1f;               //++
        
    //Выбранные скины
    public int selectedHatId = 0;                   //++
    public int selectedPetId = 0;                   //++
    public int selectedAccessoiresId = 0;
    public int selectedShirtId = 0;
    public int selectedPantsId = 0;
    public int selectedBagsId = 0;
    public int selectedHairId = 0;
    public int selectedHairColorId = 0;

    public int spawnPointNumber = 0;
    public int moneyCount = 0;                           //++

    public int[] currentLevelsCheckpointsNumbers = new int[2];
    public int[] currentLevelsCoinsNumbers = new int[2];
    public string[] currentLevelsCollectedCoinsMasks = new string[2];

    //Купленные скины
    public bool[] hatSkinsBuyStates = new bool[90];                //++
    public bool[] hairSkinsBuyStates = new bool[61];
    public bool[] hairColorsBuyStates = new bool[42];
    public bool[] accessoriesSkinsBuyStates = new bool[19];
    public bool[] petSkinsBuyStates = new bool[43];                //++
    public bool[] shirtsSkinsBuyStates = new bool[42];
    public bool[] bagsSkinsBuyStates = new bool[25];
    public bool[] pantsSkinsBuyStates = new bool[42];
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

            for (int index = 0; index < Instance.playerInfo.currentLevelsCoinsNumbers.Length; index++)
            {
                Instance.playerInfo.currentLevelsCoinsNumbers[index] = -1;
            }

            if (Instance.playerInfo.currentLevelsCollectedCoinsMasks == null ||
                Instance.playerInfo.currentLevelsCollectedCoinsMasks.Length != Instance.playerInfo.currentLevelsCheckpointsNumbers.Length)
            {
                Instance.playerInfo.currentLevelsCollectedCoinsMasks =
                    new string[Instance.playerInfo.currentLevelsCheckpointsNumbers.Length];
            }

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
            if (Instance.playerInfo.currentLevelsCollectedCoinsMasks == null ||
                Instance.playerInfo.currentLevelsCollectedCoinsMasks.Length != Instance.playerInfo.currentLevelsCheckpointsNumbers.Length)
            {
                Instance.playerInfo.currentLevelsCollectedCoinsMasks =
                    new string[Instance.playerInfo.currentLevelsCheckpointsNumbers.Length];
            }

            Instance.playerInfo.hatSkinsBuyStates[0] = true;
            Instance.playerInfo.petSkinsBuyStates[0] = true;
            Instance.playerInfo.accessoriesSkinsBuyStates[0] = true;
            Instance.playerInfo.shirtsSkinsBuyStates[0] = true;
            Instance.playerInfo.pantsSkinsBuyStates[0] = true;
            Instance.playerInfo.bagsSkinsBuyStates[0] = true;
            Instance.playerInfo.hairSkinsBuyStates[0] = true;
            Instance.playerInfo.hairColorsBuyStates[0] = true;

            

            YandexSDK.Save();
            progressLoaded = true;
        }
    }
}
