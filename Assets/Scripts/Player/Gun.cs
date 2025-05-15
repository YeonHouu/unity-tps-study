using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField][Range(0, 100)] private float attackRange;
    [SerializeField] private int shootDamage;
    [SerializeField] private float shootDelay;
    [SerializeField] private AudioClip shootSFX;

    private CinemachineImpulseSource impulse;
    
    private Camera cam;

    private bool canShoot { get => currentCount <= 0; }     // .currentCount가 0이면 true 반환
    private float currentCount;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        HandleCanShoot();
    }

    private void Init()
    {
        cam = Camera.main;
        impulse = GetComponent<CinemachineImpulseSource>();
    }

    public bool Shoot()
    {
        if (!canShoot) return false;

        PlayShootSound();
        PlayCameraEffect();
        PlayShootEffect();
        currentCount = shootDelay;

        // Ray 발사 -> Ray 충돌한 대상(반환받은)에게 데미지 부여 기능 추가
        GameObject target = RayShoot();
        //대상이 없어도 총을 쏠 수 있음: 총을 쏘긴 했으니 true 반환
        if (target == null) return true;

        Debug.Log($"총에 맞음 : {target.name}");

        return true;
    }

    private void HandleCanShoot()
    {
        if (canShoot) return;

        currentCount -= Time.deltaTime;
    }

    private GameObject RayShoot()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, attackRange, targetLayer))
        {
            return hit.transform.gameObject;
            //몬스터를 어떻게 구현하는가에 따라 다름
            // IDamagable
        }
        return null;
    }

    private void PlayShootSound()
    {
        // 효과음 객체 반환
        SFXController sfx = GameManager.Instance.Audio.GetSFX();
        sfx.Play(shootSFX);
    }

    private void PlayCameraEffect()
    {
        impulse.GenerateImpulse();
    }

    private void PlayShootEffect()
    {
        // 총구 화염 효과
    }
}
