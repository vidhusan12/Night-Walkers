using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 This Script represents a guns controller in a game. it handles
shooting functionality inculding differnt shooting modes and bullet behavior;
 */

public class Guns : MonoBehaviour
{
 

    // Firing
    public bool isFiring;
    public bool readyToFire;
    bool allowReset = true;
    public float fireDelay = 2f;

    // Burst
    public int projectilesPerBurst = 3;
    public int currentBurst;

    // Spread
    public float spreadIntensity;

    // Projectile 
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public float projectileSpeed;
    public float projectileLifetime = 3f; // seconds

    //References
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

        // If ready to fire and input is firing, shoot the weapon
        if (readyToFire && isFiring)
        {
            currentBurst = projectilesPerBurst;
            FireWeapon();
        }
    }

    // Fire the weapon
    private void FireWeapon()
    {
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

}
