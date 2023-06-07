using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is attached to a bullet object and handles its collion behaviour

public class Bullet : MonoBehaviour
{
    //This function is called when the bullet collides with another object
    private void OnCollisionEnter(Collision ObjectWeHit)
    {
        //Check if the object we hit has the "Target" tag
        if (ObjectWeHit.gameObject.CompareTag("Target"))
        {
            print("Hit " + ObjectWeHit.gameObject.name + " !");

            //Create a bullet impact effect at teh collision point
            CreateBulletImpactEffect(ObjectWeHit);
            //Destory the bull object
            Destroy(gameObject);
        }

        if (ObjectWeHit.gameObject.CompareTag("Wall"))
        {
            print("hit a wall");
            CreateBulletImpactEffect(ObjectWeHit);
            Destroy(gameObject);
        }
    }

    //This function creates a bullet impact effect at the collision point
    public void CreateBulletImpactEffect(Collision ObjectWeHit)
    {
        //Get the contact point of the collision
        ContactPoint contact = ObjectWeHit.contacts[0];

        //Instantiate the bullet impact effect at the contact point with the normal rotaion
        GameObject hole = Instantiate(GlobalReferences.Instance.impactEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));

        //Set the parent of the Impact effect to the object we hit
        hole.transform.SetParent(ObjectWeHit.gameObject.transform);

    }
}
