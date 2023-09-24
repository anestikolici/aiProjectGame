using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class PlayerShooting : MonoBehaviour
{
    // Weapon anchor
    [Tooltip("Weapon anchor")]
    [SerializeField]
    private GameObject weapon;

    // Weapon anchor GameObject for headbobbing
    [Tooltip("Weapon anchor GameObject for headbobbing")]
    [SerializeField]
    private GameObject headbobbingAnchor;

    // Weapon ammo
    [Tooltip("Weapon ammo")]
    [SerializeField]
    private int ammo;

    // Laser prefab
    [Tooltip("Laser prefab")]
    [SerializeField]
    private GameObject laser;

    // Fire Rate
    [Tooltip("Fire rate")]
    [SerializeField]
    private float fireRate;

    // Weapon barrel
    [Tooltip("Weapon barrel")]
    [SerializeField]
    private GameObject weaponBarrel;

    // Weapon recoil
    [Tooltip("Weapon recoil")]
    [SerializeField]
    private float recoil;

    // Weapon kickback
    [Tooltip("Weapon kickback")]
    [SerializeField]
    private float kickback;

    // Laser audio
    [Tooltip("Laser audio")]
    [SerializeField]
    private AudioSource laserAudio;

    // Reload audio
    [Tooltip("Reload audio")]
    [SerializeField]
    private AudioSource reloadAudio;

    // Ammo count text
    [Tooltip("Ammo count text")]
    [SerializeField]
    private TextMeshProUGUI ammoText;

    // Player movement script
    [Tooltip("Player Movement Script")]
    [SerializeField]
    private PlayerMovement playerMovement;

    // Bollean containing whether the Player is shooting or not
    private bool isShooting = false;

    // Current Player ammo
    private int currentAmmo;

    // Time passed after the last bullet was fired by the player
    private float lastFired;

    // Original weapon position
    private Vector3 originalWeaponPosition;

    // Original weapon rotation
    private Quaternion originalWeaponRotation;

    /// <summary>
    /// Called before the first frame update
    /// </summary>
    void Start()
    {
        currentAmmo = ammo;

        originalWeaponPosition = weapon.transform.localPosition;
        originalWeaponRotation = weapon.transform.localRotation;

        ammoText.SetText("Ammo: " + ammo);
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // Shooting
        Shooting();
    }

    /// <summary>
    /// Player shooting
    /// </summary>
    private void Shooting()
    {
        if (isShooting & currentAmmo > 0)
        {
            if (Time.time > lastFired + fireRate)
            {
                currentAmmo--;
                ammoText.SetText("Ammo: " + currentAmmo);
                lastFired = Time.time;
                float currentRecoil = recoil * 1.5f;

                laserAudio.Play();
                GameObject currentLaser = Instantiate(laser, weaponBarrel.transform.position, Quaternion.identity);
                currentLaser.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 2500);

                Camera.main.transform.Rotate(-currentRecoil * Time.deltaTime, 0f, 0f);
                weapon.transform.position -= weapon.transform.forward * kickback;
            }
        }
        weapon.transform.SetLocalPositionAndRotation(Vector3.Lerp(weapon.transform.localPosition, originalWeaponPosition, Time.deltaTime * 10 / 2f),
                                                     Quaternion.Lerp(weapon.transform.localRotation, originalWeaponRotation, Time.deltaTime * 4f));
    }

    /// <summary>
    /// Sets the isShooting value to True if the Player is pressing/holding down the Fire button
    /// </summary>
    public void FirePressed()
    {
        isShooting = true;
    }

    /// <summary>
    /// Sets the isShooting value to True if the Player has stopped pressing/holding down the Fire button
    /// </summary>
    public void FireReleased()
    {
        isShooting = false;
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
