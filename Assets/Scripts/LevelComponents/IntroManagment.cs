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
    private PlayableDirector introDirector;

    [Header("Cameras fields")]
    [SerializeField]
    private CinemachineVirtualCamera[] dollyCameras;
    [SerializeField]
    private CinemachineSmoothPath[] trackPathes;

    public Action IntroFinished;
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
        ToggleDollyCameras(false);
        ToggleSmoothPathTracks(true);
        dollyCameras[0].gameObject.SetActive(true);
    }
    public void StartIntro()
    {
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
        IntroFinished?.Invoke();
        ToggleDollyCameras(false);
        ToggleSmoothPathTracks(false);
    }
}
