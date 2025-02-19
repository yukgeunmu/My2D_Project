using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecttileManager : MonoBehaviour
{
    private static ProjecttileManager instance;
    public static ProjecttileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;

    private void Awake()
    {
        instance = this;
    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {

        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];

        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler);
    }

}
