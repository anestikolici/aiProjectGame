using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    private int bulletsShot = 0;
    
    private ProceduralRecoil recoilComponent;

    void Start()
    {
        recoilComponent = GameObject.Find("SciFiGunLightRad").GetComponent<ProceduralRecoil>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            bulletsShot++;
            recoilComponent.StartRecoil(0.2f, 10f, 10f);

            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }

    public int GetBulletsShot()
    {
        return bulletsShot;
    }
}
