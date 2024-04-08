using TMPro;
using UnityEngine;

public class CannonShotController : MonoBehaviour
{
    [SerializeField] GameObject cannonballObject;
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform targetPoint;
    [SerializeField] GameObject coinObject;
    [SerializeField] TextMeshProUGUI hintText;
    bool characterIsReady;
    float shootDelay = 0.5f;
    bool isShotProccess, isAlreadyRewarded;
    GameObject cannonballClone;
    float distance;
    Vector3 shootVector;
    float shootPower = 250f;
    
    SoundController soundController;
    
    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
        coinObject.SetActive(false);
        shootVector = -(shootPoint.position - targetPoint.position).normalized;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isShotProccess && characterIsReady && Input.GetMouseButtonDown(0))
        {        
            hintText.gameObject.SetActive(false);
            isShotProccess = true;
            Invoke("MakeShot", shootDelay);
        }      
        CheckDistance();
    }

    void MakeShot()
    {
        soundController.Play("CannonShot");
        cannonballClone = Instantiate(cannonballObject, 
            shootPoint.position, 
            Quaternion.identity, 
            transform);

        cannonballClone.GetComponent<Rigidbody>().AddForce
            (shootVector * shootPower, 
            ForceMode.Impulse);

        if (!isAlreadyRewarded)
        {
            soundController.Play("Success");
            coinObject.SetActive(true);
            isAlreadyRewarded = true;
        }
        
        isShotProccess = false;
    }
    void CheckDistance()
    {
        if (cannonballClone == null)
            return;
        distance = (cannonballClone.transform.position - transform.position).magnitude;
        if(distance > 100f)
            Destroy(cannonballClone);
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
