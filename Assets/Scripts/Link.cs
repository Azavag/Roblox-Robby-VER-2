using System.Runtime.InteropServices;
using UnityEngine;

public class Link : MonoBehaviour
{  
    //Получение домена
    [DllImport("__Internal")]
    private static extern string GetDomainExtern();

    public static Link Instance;
    public string currentDomain;
    string link = "";
    private void Awake()
    {      
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    //По кнопке
    public void GotoDeveloperPage()
    {
#if !UNITY_EDITOR
            currentDomain = GetDomainExtern();
#endif
        link = $"https://yandex.{currentDomain}/games/developer?name=DemiGames";
        Application.OpenURL(link);
    }
}
