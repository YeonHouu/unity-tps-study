using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource bgmSource;


    private void Awake() => Init();

    private void Init()
    {
        bgmSource = GetComponent<AudioSource>();
    }

    public void BgmPlay()
    {
        bgmSource.Play();
    }
}
