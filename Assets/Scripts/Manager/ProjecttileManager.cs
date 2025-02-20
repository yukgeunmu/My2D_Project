using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecttileManager : MonoBehaviour
{
    private static ProjecttileManager instance;
    public static ProjecttileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private ParticleSystem impactParticleSystem;

    private void Awake()
    {
        instance = this;

    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {

        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];

        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler, this);
    }

    public void CreateImpactParticlesAtPostion(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        if (impactParticleSystem == null)
        {
            Debug.LogError(" impactParticleSystem이 존재하지 않습니다! 재시작 후 씬이 로드되면서 삭제된 것 같습니다.");
            return;
        }

        impactParticleSystem.transform.position = position;
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));
        ParticleSystem.MainModule mainModule = impactParticleSystem.main;
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;
        impactParticleSystem.Play();


    }

}
