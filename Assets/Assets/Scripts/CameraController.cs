using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera myVC;
    private CinemachineBasicMultiChannelPerlin noise;

    private void Start() {
        noise = myVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        //StartCoroutine(ShakeCamera(3,5f));
    }

    public void CallScreenShake(){
        StartCoroutine(ShakeCamera(5,0.5f));
    }

    IEnumerator ShakeCamera(float intensity, float time){
        noise.m_AmplitudeGain = intensity;
        float totalTime = time;
        float initIntensity = intensity;
        while(totalTime > 0){
            totalTime -= Time.deltaTime;
            noise.m_AmplitudeGain = Mathf.Lerp(initIntensity,0f, 1-(totalTime/time));
            yield return null;
        }
    }
}
