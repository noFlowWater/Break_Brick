using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ZogagSound : MonoBehaviour
{
    AudioSource audioSource;
    public float maxDistanceFromCenter = 500f;
    public float maxVolumeDifference = 0.5f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        if (SceneManager.GetActiveScene().name != "InGameScene")
        {
            Debug.Log("타이틀 : " + audioSource.panStereo);
            audioSource.Play();
            return;
        }
        float distanceFromCenter = Mathf.Abs(transform.position.x - Screen.width / 2f);
        float volumeMultiplier = 1f - Mathf.Clamp(distanceFromCenter / maxDistanceFromCenter, 0f, 1f);
        float leftVolume = 1f;
        float rightVolume = 1f - volumeMultiplier * maxVolumeDifference;

        if (transform.position.x < Screen.width / 2f)
        {
            // 파티클 프리팹이 화면 왼쪽에 위치한 경우, 오른쪽 채널에 작은 볼륨을 적용
            leftVolume = 1f - volumeMultiplier * maxVolumeDifference;
            rightVolume = 1f;
        }

        // 이어폰의 좌우 스테레오 출력 값 조정
        audioSource.panStereo = (leftVolume - rightVolume) / 2f;
        Debug.Log("인게임 : " + audioSource.panStereo);
        audioSource.Play(); 
    }
}
