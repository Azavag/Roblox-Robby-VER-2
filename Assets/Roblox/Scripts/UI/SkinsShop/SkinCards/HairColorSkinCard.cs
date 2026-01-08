using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HairColorSkinCard : SkinCard, IPointerClickHandler
{
    public static event Action<SkinCard> HairColorCardClicked;
}
