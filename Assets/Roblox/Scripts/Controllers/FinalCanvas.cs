using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalCanvas : MonoBehaviour
{
    [SerializeField]
    private Button menuButton;

    [SerializeField]
    private InterfaceNavigation interfaceNavigation;

    private void OnEnable()
    {
        menuButton.onClick.AddListener(OnClickMenuButton);
    }
    private void OnDisable()
    {
        menuButton.onClick.RemoveListener(OnClickMenuButton);
    }

    private void OnClickMenuButton()
    {
        interfaceNavigation.FinishLevel();
    }
}
