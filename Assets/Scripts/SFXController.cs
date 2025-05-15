using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : PooledObject
{
    private AudioSource audioSource;

    private float currentCount;

    private void Awake() => Init();

    private void Init()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 한 프레임이 갱신될 때까지의 시간 = DeltaTime(프레임 시간)
        // -= Time.deltaTime : 흐른 시간만큼 계속 뺌
        // += Time.deltaTime : 흐른 시간만큼 계속 더해짐
        currentCount -= Time.deltaTime;

        if(currentCount <= 0)
        {
            //audioSource.Stop();
            //audioSource.clip = null;
            ReturnPool();
        }
    }

    public void Play(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();

        // 오디오 파일 길이
        currentCount = clip.length;
    }

}
