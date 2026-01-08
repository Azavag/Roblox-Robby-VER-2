using UnityEngine;

public abstract class SkinsStorage : MonoBehaviour
{
    protected int currentSkinIndex;
    protected int prevSkinIndex;

    public abstract void ShowSkinObject(int index);

}
