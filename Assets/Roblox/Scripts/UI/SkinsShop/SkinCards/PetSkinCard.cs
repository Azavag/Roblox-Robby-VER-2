using System;
using UnityEngine.EventSystems;

public class PetSkinCard : SkinCard, IPointerClickHandler
{
    public static event Action<SkinCard> PetCardClicked;

}
