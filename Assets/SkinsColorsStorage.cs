using UnityEngine;

public class SkinsColorsStorage : SkinsStorage
{
    [SerializeField]
    private Material material;
    [SerializeField]
    private Color[] colors;

    public override void ShowSkinObject(int index)
    {
        currentSkinIndex = index;
        material.color = colors[index];
    }
    
    public Color[] GetColorsArray()
    {
        return colors;
    }
}
