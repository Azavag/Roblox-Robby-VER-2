using UnityEngine;

[CreateAssetMenu(fileName = "NewColorSkinScriptableObject", menuName = "Custom/Create Color Skin Scriptable Object")]
public class ColorSkinScriptableObject : SkinScriptableObject
{
    [HideInInspector] public new Sprite sprite; // Скрываем базовое поле
    public Color skinColor;
}

