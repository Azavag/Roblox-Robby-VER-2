using UnityEngine;

public class SkinsObjectsStorage : SkinsStorage
{
    [SerializeField]
    private GameObject[] skinObjects;
    
    public override void ShowSkinObject(int index)
    {
        skinObjects[prevSkinIndex].SetActive(false);
        skinObjects[index].SetActive(true);
        prevSkinIndex = index;
    }
}
