using DG.Tweening;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    private Transform coinModel;

    [Header("RotateAnim")]
    private Tween rotateTween;
    [SerializeField]
    private Vector3 rotationVector;
    [SerializeField]
    private float rotateDuration;
    [SerializeField]
    private Ease rotateEase;


    private void OnDisable()
    {
        rotateTween.Pause();
        rotateTween.Kill();
    }
    private void OnEnable()
    {
        rotateTween = coinModel.DOLocalRotate(rotationVector, rotateDuration, RotateMode.FastBeyond360)
           .SetEase(rotateEase)
           .SetLoops(-1, LoopType.Incremental)
           .Play();
    }
}
