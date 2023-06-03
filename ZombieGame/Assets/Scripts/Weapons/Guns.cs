using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/*
 This Script represents a guns controller in a game. it handles
shooting functionality inculding differnt shooting modes and bullet behavior;
 */

public class Guns : MonoBehaviour
{

    [Header("Firing")]
    // Firing
    public bool isFiring;
    public bool readyToFire;
    bool allowReset = true;
    public float fireDelay = 2f;

    // Burst
    [Header("Burst")]
    public int projectilesPerBurst = 3;
    public int currentBurst;

    // Spread
    [Header("Spread")]
    public float spreadIntensity;

    //Loading
    [Header("Loading")]
    public float reloadTime;
    public int magazineSize;
    public int bulletsLeft;
    public bool isReloading;


    // Projectile
    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public float projectileSpeed;
    public float projectileLifetime = 3f; // seconds

    //References
    [Header("References")]
    public GameObject muzzleEffect;
    private Animator animator;

    public enum FiringMode
    {
        Single,
        Burst,
        Auto
    }

    public FiringMode currentFiringMode;

    // Initialize the gun controller
    public void Awake()
    {
        readyToFire = true;
        currentBurst = projectilesPerBurst;
        animator = GetComponent<Animator>();
        bulletsLeft = magazineSize;
    }

    // Update is called once per frame
    public void Update()
    {
        // Check the firing mode and input to determine if firing
        if (currentFiringMode == FiringMode.Auto)
        {
            // Holding down the left mouse button
            isFiring = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentFiringMode == FiringMode.Single || currentFiringMode == FiringMode.Burst)
        {
            // Clicking the left mouse button once
            isFiring = Input.GetKeyDown(KeyCode.Mouse0);
        }

        // If bullet is less then the magazineSize and R is pressed then reload
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
        {
            Reload();
        }

        // If you want to automatically reload when magazine is empty
        if(readyToFire && isFiring == false && isReloading == false && bulletsLeft <= 0)
        {
            Reload();
        }

        // If ready to fire and input is firing, shoot the weapon
        if (readyToFire && isFiring && bulletsLeft > 0)
        {
            currentBurst = projectilesPerBurst;
            FireWeapon();
        }

        // Update the UI according to amount of bullets left
        if(AmmoManger.Instance.ammoDisplay != null)
        {
            AmmoManger.Instance.ammoDisplay.text = $"{bulletsLeft/projectilesPerBurst}/{magazineSize/projectilesPerBurst}";
        }

    }

    // Fire the weapon
    private void FireWeapon()
    {
        //Decreasing the amount of bullet left
        bulletsLeft--;

        //Activiting the muzzle
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        SoundManger.Instance.shootingSoundM1911.Play();

        readyToFire = false;

        // Calculate the firing direction with spread
        Vector3 firingDirection = CalculateDirectionAndSpread().normalized;

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);

        // Point the projectile to face the firing direction
        projectile.transform.forward = firingDirection;

        // Shoot the projectile
        projectile.GetComponent<Rigidbody>().AddForce(firingDirection * projectileSpeed, ForceMode.Impulse);

        // Destroy the projectile after some time in the air
        StartCoroutine(DestroyProjectileAfterTime(projectile, projectileLifetime));

        // Check if we are done firing
        if (allowReset)
        {
            Invoke("ResetFire", fireDelay);
            allowReset = false;
        }

        // Burst Mode
        if (currentFiringMode == FiringMode.Burst && currentBurst > 1)
        {
            currentBurst--;
            Invoke("FireWeapon", fireDelay);
        }
    }

    // Reset the firing state
    private void ResetFire()
    {
        readyToFire = true;
        allowReset = true;
    }

    // Destroy the projectile after a specified delay
    private IEnumerator DestroyProjectileAfterTime(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(projectile);
    }

    // Calculate the firing direction with spread
    public Vector3 CalculateDirectionAndSpread()
    {
        // Fire from the center of the screen to check where we are aiming
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        if(Physics.Raycast(ray, out hit))
        {
            //Hitting Something
            targetPoint = hit.point;
        }
        else
        {
            //Shooting at the air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - projectileSpawn.position;
        float spreadX = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float spreadY = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        // Apply spread to the firing direction
        Vector3 firingDirection = direction + new Vector3(spreadX, spreadY, 0);

        return firingDirection.normalized;
    }

    //Reloading method
    private void Reload()
    {
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }

}
