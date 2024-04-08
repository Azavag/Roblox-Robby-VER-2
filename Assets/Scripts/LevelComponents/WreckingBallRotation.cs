using DG.Tweening;
using UnityEngine;

public class WreckingBallRotation : MonoBehaviour
{
    [SerializeField]
    private Transform ballTransform;
    [SerializeField]
    private Vector3 startRotation;
    [Header("Animation Settings")]
    private Tween rotateTween;
    [SerializeField]
    private Vector3 rotationVector;
    [SerializeField]
    private float rotateDuration;
    [SerializeField]
    private Ease rotateEase;

    private void OnDestroy()
    {
        rotateTween.Pause();
        rotateTween.Kill();
    }
    private void OnDisable()
    {
       
    }

    private void Start()
    {
        ballTransform.localEulerAngles = startRotation;

        rotateTween = ballTransform.DOLocalRotate(rotationVector, rotateDuration, RotateMode.Fast)
           .SetEase(rotateEase)
           .SetLoops(-1, LoopType.Yoyo)
           .Play();
    }

}
