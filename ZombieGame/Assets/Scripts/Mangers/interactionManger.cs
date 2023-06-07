using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class interactionManger : MonoBehaviour
{
    //Static refereence to the GlobalReference instance
    public static interactionManger Instance { get; set; }

    public Guns hoveredWeapon = null;

    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            //If there is already an instance of GlobalReferenec and it is not this one, destory this object
            Destroy(gameObject);
        }
        else
        {
            //Set the Instance to this Object
            Instance = this;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            GameObject ObjHitByRayCast = hit.transform.gameObject;

            if (ObjHitByRayCast.GetComponent<Guns>() && ObjHitByRayCast.GetComponent<Guns>().isActiveWeapon == false)
            {
                hoveredWeapon = ObjHitByRayCast.gameObject.GetComponent<Guns>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;

                //press F to pick up the weapon
                if (Input.GetKeyDown(KeyCode.F))
                {
                 
                    WeaponManager.Instance.PickUpWeapon(ObjHitByRayCast.gameObject);
                }
                
            }
            else
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
            }
        }

    }

}
