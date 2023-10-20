using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class Shooting : MonoBehaviour
{
    // Laser audio
    [Tooltip("Laser audio")]
    [SerializeField]
    private AudioSource laserAudio;

    // Weapon ammo
    [Tooltip("Weapon ammo")]
    [SerializeField]
    private int ammo;

    // Ammo count text
    [Tooltip("Ammo count text")]
    [SerializeField]
    private TextMeshProUGUI ammoText;

    // Reload audio
    [Tooltip("Reload audio")]
    [SerializeField]
    private AudioSource reloadAudio;

    // Current Player ammo
    private int currentAmmo;


    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    private int bulletsShot = 0;
    
    private ProceduralRecoil recoilComponent;
    private bool canShoot = true;

    void Start()
    {
        currentAmmo = ammo;
        ammoText.SetText("Ammo: " + ammo);

        recoilComponent = GameObject.Find("SciFiGunLightRad").GetComponent<ProceduralRecoil>();
    }

    
    void Update()
    {
        bool hasAmmo = currentAmmo > 0;
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot && hasAmmo)
        {
            UpdateCurrentAmmo();
            bulletsShot++;
            recoilComponent.StartRecoil(0.2f, 2f, 2f);
            laserAudio.Play();
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            OnReloadPressed();
        }
    }

    void UpdateCurrentAmmo()
    {
        currentAmmo--;
        ammoText.SetText("Ammo: " + currentAmmo);
    }

    public int GetBulletsShot()
    {
        return bulletsShot;
    }

    public void SetCanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }

    #region - Reload -

    public void OnReloadPressed()
    {

        StartCoroutine(Reload());
    }
    /// <summary>
    /// Reloading
    /// </summary>
    IEnumerator Reload()
    {
        if (!reloadAudio.isPlaying && currentAmmo < ammo)
        {
            currentAmmo = 0;
            reloadAudio.Play();
            while (reloadAudio.isPlaying)
            {
                ammoText.SetText("Reloading");
                yield return null;
            }
            currentAmmo = ammo;
            ammoText.SetText("Ammo: " + currentAmmo);
        }
    }
    #endregion
}
