using System.Collections;
using UnityEngine;
using Cinemachine;
public class CineMachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakerTime;
    private CinemachineBasicMultiChannelPerlin cMP;
    public static CineMachineShake instance;
    private void Awake()
    {
        instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cMP = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void ShakeCamera(float intensity, float time)
    {
       

        cMP.m_AmplitudeGain = intensity;
        shakerTime = time;
    }
    private void Update()
    {
        if(shakerTime > 0)
        {
            shakerTime -= Time.deltaTime;
            if(shakerTime <= 0)
            {
                cMP.m_AmplitudeGain = 0f;
            }
        }
    }
}