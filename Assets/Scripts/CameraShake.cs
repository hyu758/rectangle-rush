using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float shakeIntensity = 1f;
    [SerializeField] private float shakeTime = 0.2f;
    private float timer;

    private CinemachineBasicMultiChannelPerlin _cbmcp;
    public GameController _controller;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        stopShake();
    }
    public void shakeCamera()
    {
        _cbmcp.m_AmplitudeGain = shakeIntensity;
        timer = shakeTime;
    }

    void stopShake()
    {
        _cbmcp.m_AmplitudeGain = 0;
        timer = 0;
    }

    private void Update()
    {
        if (_controller.isDead)
        {
            shakeCamera();
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                stopShake();
            }
        }
    }
}
