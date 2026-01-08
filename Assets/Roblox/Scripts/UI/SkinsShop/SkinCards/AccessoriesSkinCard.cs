using System;
using UnityEngine.EventSystems;

public class AccessoriesSkinCard : SkinCard, IPointerClickHandler
{
    public static event Action<SkinCard> ÀccessoriesCardClicked;

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    ÀccessoriesCardClicked?.Invoke(this);
    //}
}
