using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera singleVirtualCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StopFollowingPlayer()
    {
        if (singleVirtualCamera != null && singleVirtualCamera.enabled)
        {
            singleVirtualCamera.Follow = null;
            singleVirtualCamera.LookAt = null;
        }
    }
}