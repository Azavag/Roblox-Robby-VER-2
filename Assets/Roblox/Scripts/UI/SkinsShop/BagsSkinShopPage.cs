using DG.Tweening;
using UnityEngine;

public class BagsSkinShopPage : SkinShopPage
{
    [SerializeField]
    private GameObject playerPostureObject;

    [Header("Rotate Posture settings")]
    [SerializeField]
    private float rotateDuration;
    [SerializeField]
    private float targetAngle;
    [SerializeField]
    private float startAngle;
    [SerializeField]
    private Ease rotateEaseType;

    private Tween _rotatePostureTween;
    private bool _isInitialized;

    private void Awake()
    {
        Initialize();
    }
    private void OnDestroy()
    {
        DisposeTween();
    }
    private void Initialize()
    {
        if (_isInitialized || playerPostureObject == null)
            return;

        _isInitialized = true;
    }


    public override void OpenPage()
    {
        RotatePosture();
    }

  
    private void RotatePosture()
    {
        if (!_isInitialized)
        {
            Initialize();
            if (!_isInitialized) return;
        }

        // Останавливаем предыдущую анимацию, если она еще активна
        if (_rotatePostureTween != null && _rotatePostureTween.IsActive())
        {
            _rotatePostureTween.Kill();
            _rotatePostureTween = null;
        }

        // Вращаем от текущей позиции на targetAngle градусов
        _rotatePostureTween = playerPostureObject.transform
            .DOLocalRotate(
                new Vector3(0, targetAngle, 0),
                rotateDuration
            )
            .SetEase(rotateEaseType)
            .OnKill(() => _rotatePostureTween = null) // Очищаем ссылку при уничтожении
            .Play();
    }
    private void DisposeTween()
    {
        if (_rotatePostureTween != null && _rotatePostureTween.IsActive())
        {
            _rotatePostureTween.Kill();
        }
        _rotatePostureTween = null;
        _isInitialized = false;
    }

    public override void ClosePage()
    {
        // Останавливаем предыдущую анимацию, если она еще активна
        if (_rotatePostureTween != null && _rotatePostureTween.IsActive())
        {
            _rotatePostureTween.Pause();
            _rotatePostureTween.Kill();
            _rotatePostureTween = null;
        }

        // Вращаем от текущей позиции на targetAngle градусов
        _rotatePostureTween = playerPostureObject.transform
            .DOLocalRotate(
                new Vector3(0, startAngle, 0),
                rotateDuration
            )
            .SetEase(rotateEaseType)
            .OnKill(() => _rotatePostureTween = null) // Очищаем ссылку при уничтожении
            .Play();
    }
}
