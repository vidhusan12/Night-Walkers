using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawm;
    public float bulletVelocity;
    public float bulletPrefabLifeTime;

    public void Update()
    {
        //Left mouse click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        //Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawm.position, Quaternion.identity);

        //Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawm.forward.normalized * bulletVelocity, ForceMode.Impulse);

        //Destory the bullet after some time in the air
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
        
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

}
