using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPostureRotate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Transform postureTransform;

    private float deltaMovement;
    private bool onBeginDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        deltaMovement = -eventData.delta.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onBeginDrag = false;
    }


    private void Update()
    {
        if (!onBeginDrag)
            return;
        postureTransform.Rotate(new Vector3(0,deltaMovement,0));
        deltaMovement = 0;
    }
}
