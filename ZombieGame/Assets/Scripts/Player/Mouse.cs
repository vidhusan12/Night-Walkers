using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private float mouseSensitivity; // Sensitivity of mouse movement
    [SerializeField] private Transform arms; // Reference to the transform of the arms
    [SerializeField] private Transform body; // Reference to teh transform of the body

    private float rotationX; // Rotation angle around teh X-axis

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Get the mouse X movement input
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Get the mouse Y movement input

        RotateArms(mouseY); //Rotate the arms based on the mouse Y input
        RotateBody(mouseX); //Roate the body based on teh mouse X input
  
    }

    //Roate the arms based on the mouse Y input
    private void RotateArms(float mouseY)
    {
        rotationX -= mouseY; // Update the rotation angle around the X-axis
        rotationX = Mathf.Clamp(rotationX, -90, 90); // Clamp the rotation angle within a range of -90 to 90 degrees
        arms.localRotation = Quaternion.Euler(new Vector3(rotationX, 0, 0)); // Apply the rotation to the arms
    }

    //Rotate the body based on the mouse X input
    private void RotateBody(float mouseX)
    {
        body.Rotate(new Vector3(0, mouseX, 0)); // Rotate the body around the Y-axis based on the mouse X input

    }

}
