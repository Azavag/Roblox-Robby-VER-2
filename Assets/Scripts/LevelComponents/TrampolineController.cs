using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    [SerializeField]
    private float bouncePower = 100;
    private int jumpCounter, jumpTarget = 3;
    private bool isRecieved;
    [SerializeField]
    private GameObject coinObject;
    private SoundController soundController;
    void Start()
    {
        coinObject.SetActive(false);
        soundController = FindObjectOfType<SoundController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundController.Play("Spring");
            other.gameObject.GetComponent<Rigidbody>().AddForce(bouncePower * Vector3.up, ForceMode.Impulse);
            jumpCounter++;
            if (jumpCounter == jumpTarget && !isRecieved)
            {
                soundController.Play("Success");
                coinObject.SetActive(true);
                isRecieved = true;
            }
        }

    }
}
