using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Laser audio
    [Tooltip("Laser audio")]
    [SerializeField]
    private AudioSource laserAudio;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    private int bulletsShot = 0;
    
    private ProceduralRecoil recoilComponent;
    private bool canShoot = true;

    void Start()
    {
        recoilComponent = GameObject.Find("SciFiGunLightRad").GetComponent<ProceduralRecoil>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            bulletsShot++;
            recoilComponent.StartRecoil(0.2f, 4f, 4f);
            laserAudio.Play();
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }

    public int GetBulletsShot()
    {
        return bulletsShot;
    }

    public void SetCanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }
}
