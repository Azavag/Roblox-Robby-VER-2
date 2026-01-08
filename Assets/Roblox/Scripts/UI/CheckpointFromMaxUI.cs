using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckpointFromMaxUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI checkpointsText;
    [SerializeField]
    private int levelIndex;
    [SerializeField]
    private int levelMaxLevels;

    private void Start()
    {
        checkpointsText.text = 
            $"{Bank.Instance.playerInfo.currentLevelsCheckpointsNumbers[levelIndex]} / {levelMaxLevels}";
    }

}
