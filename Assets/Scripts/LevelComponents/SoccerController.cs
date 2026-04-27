using UnityEngine;

public class SoccerController : MonoBehaviour
{
    [SerializeField] 
    private GameObject ballObject;
    [SerializeField]
    private GameObject postObject;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField] Transform playerStandPoint;
    private Vector3 vectorBetweenObjects;
    private float kickPower = 15f;
    private bool isPlayerReady;
    private bool isAnimation;
    private SoundController soundController;
    private Animator animator; 
    private bool _isKicked = false;

    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
        animator = playerController.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerReady && Input.GetMouseButtonDown(0)) 
        {
            MoveCharacter();
            isAnimation = true;
            playerController.BlockPlayersInput(true);
            playerController.PassAnimation();
            
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Pass") && isAnimation)
        {
            if (_isKicked)
                return;

            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("PassBool", animationLength / 3.75f);
            soundController.Play("BallKick");
            playerController.BlockPlayersInput(false);
            isAnimation = false;
            _isKicked = true;
        }

    }
    //Перемещение мяча
    void PassBool()
    {
        vectorBetweenObjects = (postObject.transform.position
          - ballObject.transform.position).normalized;

        ballObject.GetComponent<Rigidbody>().isKinematic = false;
        ballObject.GetComponent<Rigidbody>().AddForce(vectorBetweenObjects * kickPower,
              ForceMode.Impulse);
    }
    //Установка персонажа
    void MoveCharacter()
    {
        playerController.transform.position = playerStandPoint.position;
        playerController.transform.rotation = Quaternion.LookRotation(playerStandPoint.forward);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerReady = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerReady = false;
    }
}
