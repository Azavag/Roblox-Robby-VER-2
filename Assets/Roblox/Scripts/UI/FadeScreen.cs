using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField]
    private Image blackImage;
    [SerializeField]
    private Color targetColor = Color.black;
    private Color startColor;
    [SerializeField]
    public float inFadeAnimDuration;
    [SerializeField]
    public float outFadeAnimDuration;
    [SerializeField]
    private Ease inFadeAnimEase;
    [SerializeField]
    private Ease outFadeAnimEase;
    [SerializeField]
    private Ease startAnimEase;
    Tween inFadeScreenTween;
    Tween outFadeScreenTween;

    private InterfaceNavigation interfaceNavigation;
    [SerializeField]
    private PlayerController playerController;
    private void Awake()
    {
        interfaceNavigation = GetComponent<InterfaceNavigation>();
        SetupTweens();
    }
    void Start()
    {
        //EnterLevelFadeOut();       
    }
    private void OnDestroy()
    {
        inFadeScreenTween.Kill();
        outFadeScreenTween.Kill();
        blackImage.DOKill();
    }
   
    void SetupTweens()
    {
        startColor = blackImage.color;
        inFadeScreenTween = ChangeColorTween(targetColor, inFadeAnimDuration, inFadeAnimEase);
        outFadeScreenTween = ChangeColorTween(startColor, outFadeAnimDuration, outFadeAnimEase);
        inFadeScreenTween.OnComplete(() => { StartOutFadeScreenTween(); });
        outFadeScreenTween.OnComplete(() => { interfaceNavigation.ToggleFadeScreenCanvas(false); });

    }
    public void StartInFadeScreenTween()
    {       
        interfaceNavigation.ToggleFadeScreenCanvas(true);       
        inFadeScreenTween.Restart();
    }
    public void StartOutFadeScreenTween()
    {
        outFadeScreenTween.Restart();
    }

    public Tween ChangeColorTween(Color targetColor, float duration, Ease ease)
    {
        Tween colorTween = blackImage.DOColor(targetColor, duration);
        colorTween.SetEase(ease);
        return colorTween;
    }

    public void EnterLevelFadeOut()
    {
        interfaceNavigation.ToggleFadeScreenCanvas(true);      
        blackImage.color = targetColor;
        Tween colorTween = ChangeColorTween(startColor, outFadeAnimDuration, outFadeAnimEase);
        colorTween.OnComplete(delegate
        { 
            interfaceNavigation.ToggleFadeScreenCanvas(false);
        });
        colorTween.SetAutoKill(true);
        colorTween.Play();
    }
    public void ExitLevelFadeIn(TweenCallback callback)
    {
        playerController.BlockPlayersInput(true);
        interfaceNavigation.ToggleFadeScreenCanvas(true);
        Tween colorTween = ChangeColorTween(targetColor, inFadeAnimDuration, inFadeAnimEase);
        colorTween.OnComplete(callback);
        colorTween.SetAutoKill(true);
        colorTween.Play();
    }

    public float GetInFadeDuration()
    {
        return inFadeAnimDuration;
    }
    public float GetOutFadeDuration()
    {
        return outFadeAnimDuration;
    }

}
