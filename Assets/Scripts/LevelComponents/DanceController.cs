using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class DanceController : MonoBehaviour
{
    private bool isAnimationInProccess;
    private bool isAlreadyRewarded;
    private bool characterIsReady;
    [SerializeField] 
    private CinemachineVirtualCamera danceCamera;
    [SerializeField] 
    private PlayerController playerController;
    [SerializeField]
    private GameObject coinObject;

    private float timer;
    private float animationTime = 12f;
    
    SoundController soundController;

    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
        ResetTimer();
        coinObject.SetActive(false);
    }

    void ResetTimer()
    {
        timer = animationTime;
    }
   
    void Update()
    {
        if (isAnimationInProccess)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isAnimationInProccess = false;
                playerController.DanceAnimation(isAnimationInProccess);
                danceCamera.enabled = false;
                PlayerController.IsBusy = false;
                if (isAlreadyRewarded)
                    return;

                soundController.Play("Success");
                coinObject.SetActive(true);
                isAlreadyRewarded = true;

            }
        }
        if (isAnimationInProccess || !characterIsReady)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            danceCamera.enabled = true;
            RotateCharacterToCamera();
            isAnimationInProccess = true;
            PlayerController.IsBusy = true;
            ResetTimer();
            playerController.DanceAnimation(isAnimationInProccess);
        }

       
    }

    void RotateCharacterToCamera()
    {
        Vector3 lookDirection = danceCamera.transform.position - playerController.transform.position;
        lookDirection.y = 0.0f;
        playerController.transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            characterIsReady = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            characterIsReady = false;
    }
}
