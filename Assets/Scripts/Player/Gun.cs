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

    private bool canShoot { get => currentCount <= 0; }     // .currentCount�� 0�̸� true ��ȯ
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

        // Ray �߻� -> Ray �浹�� ���(��ȯ����)���� ������ �ο� ��� �߰�
        GameObject target = RayShoot();
        //����� ��� ���� �� �� ����: ���� ��� ������ true ��ȯ
        if (target == null) return true;

        Debug.Log($"�ѿ� ���� : {target.name}");

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
            //���͸� ��� �����ϴ°��� ���� �ٸ�
            // IDamagable
        }
        return null;
    }

    private void PlayShootSound()
    {
        // ȿ���� ��ü ��ȯ
        SFXController sfx = GameManager.Instance.Audio.GetSFX();
        sfx.Play(shootSFX);
    }

    private void PlayCameraEffect()
    {
        impulse.GenerateImpulse();
    }

    private void PlayShootEffect()
    {
        // �ѱ� ȭ�� ȿ��
    }
}
