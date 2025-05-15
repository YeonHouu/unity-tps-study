using DesignPattern;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource bgmSource;

    [SerializeField] private List<AudioClip> bgmList = new();
    [SerializeField] private SFXController sfxPrefab;
    
    private ObjectPool sfxPool;


    private void Awake() => Init();

    private void Init()
    {
        bgmSource = GetComponent<AudioSource>();
        sfxPool = new ObjectPool(this.transform, sfxPrefab, 10);
    }

    public void BgmPlay(int index)
    {
        if (0 <= index && index < bgmList.Count)
        {
            bgmSource.Stop();
            bgmSource.clip = bgmList[index];
            bgmSource.Play();
        }
    }

    public SFXController GetSFX()
    {
        // 풀에서 꺼내와서 반환
         PooledObject po = sfxPool.PopPool();

        // PooledObject -> SFXController로 변환 후 반환
        return po as SFXController;
    }
}
