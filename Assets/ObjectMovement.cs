using DG.Tweening;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    private Transform coinModel;
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform endPoint;

    [Header("MoveAnim")]
    private Tween moveTween;
    [SerializeField]
    private float moveDuration;
    [SerializeField]
    private Ease moveEase;

    [Header("StopOption")]
    [SerializeField]
    private bool hasStopOption;
    [SerializeField]
    private float stopDuration;
    private float stopTimer;
    private bool isStopped = false;

    private void OnDisable()
    {
        moveTween.Pause();
        moveTween.Kill();    
    }
    private void OnEnable()
    {
        coinModel.position = startPoint.position;

        moveTween = coinModel.DOMove(endPoint.position, moveDuration)
            .SetEase(moveEase)
            .SetLoops(-1, LoopType.Yoyo);


        if (hasStopOption)
        {
            moveTween.OnStepComplete(() =>
            {
                moveTween.Pause();
                isStopped = true;
                stopTimer = stopDuration;
            });


        }
        moveTween.Play();
    }
   

    private void Update()
    {
        if(isStopped)
            stopTimer -= Time.deltaTime;
        if(stopTimer <= 0)
        {
            isStopped = false;
            moveTween.Play();
        }
    }


}
