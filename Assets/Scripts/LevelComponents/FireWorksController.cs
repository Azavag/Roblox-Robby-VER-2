using System.Collections;
using TMPro;
using UnityEngine;

public class FireWorksController : MonoBehaviour
{
    [SerializeField]
    private GameObject coinObject;
    [SerializeField]
    private ParticleSystem[] fireworks;
    [SerializeField]
    private TextMeshProUGUI hintText;
    private float timeBetweenParticles = 1f;
    private bool isAlreadyRewarded, characterIsReady;
    private SoundController soundController;
    private bool inAction;
    void Start()
    {
        coinObject.SetActive(false);
        soundController = FindObjectOfType<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inAction)
            return;
        if (characterIsReady && Input.GetMouseButtonDown(0))
        {
            hintText.gameObject.SetActive(false);
            StartCoroutine(FireworksAnimation());

        }
    }

    IEnumerator FireworksAnimation()
    {
        soundController.Play("Fireworks");
        inAction = true;
        foreach (var fire in fireworks) 
        {
            fire.Play();
            yield return new WaitForSeconds(timeBetweenParticles);
        }
        if (!isAlreadyRewarded)
        {
            soundController.Play("Success");
            coinObject.SetActive(true);
            isAlreadyRewarded = true;
            inAction = false;
        }
        yield return null;
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
