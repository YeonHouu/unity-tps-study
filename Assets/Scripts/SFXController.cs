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
        // �� �������� ���ŵ� �������� �ð� = DeltaTime(������ �ð�)
        // -= Time.deltaTime : �帥 �ð���ŭ ��� ��
        // += Time.deltaTime : �帥 �ð���ŭ ��� ������
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

        // ����� ���� ����
        currentCount = clip.length;
    }

}
