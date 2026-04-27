using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class IntroManagment : MonoBehaviour
{
    [SerializeField]
    private bool isSkipIntro;
    [SerializeField]
    private PlayableDirector introDirector;

    [Header("Cameras fields")]
    [SerializeField]
    private CinemachineVirtualCamera[] dollyCameras;
    [SerializeField]
    private CinemachineSmoothPath[] trackPathes;

    public Action IntroFinished;  

    public bool IsSkipIntro 
    { 
        get => isSkipIntro;
        set => isSkipIntro = value;
    }

    private void Awake()
    {
        introDirector.stopped += OnDirectorStopped;
    }
    private void OnDestroy()
    {
        introDirector.stopped -= OnDirectorStopped;
    }
    private void Start()
    {
        Initialiazation();
    }
    private void Initialiazation()
    {
        if (isSkipIntro)
            return;

        ToggleDollyCameras(false);
        ToggleSmoothPathTracks(true);
        dollyCameras[0].gameObject.SetActive(true);
    }
    public void StartIntro()
    {
        if (isSkipIntro)
        {
            StopDirector();
            return;
        }

        introDirector.Play();
    }

    private void ToggleDollyCameras(bool toogle)
    {
        foreach (var camera in dollyCameras)
        {
            if (camera != null)
            {
                camera.gameObject.SetActive(toogle);
            }
        }
    }

    private void ToggleSmoothPathTracks(bool toogle)
    {
        foreach (var track in trackPathes)
        {
            if (track != null)
            {
                track.gameObject.SetActive(toogle);
            }
        }
    }

    private void OnDirectorStopped(PlayableDirector director)
    {
        StopDirector();
    }
    private void StopDirector()
    {
        IntroFinished?.Invoke();
        ToggleDollyCameras(false);
        ToggleSmoothPathTracks(false);
    }
}
