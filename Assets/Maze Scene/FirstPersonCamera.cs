using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    
    public Transform playerHolder;
    public Transform playerSphere;
    public Vector3 offset = new Vector3(0, 1, 0);
    public float mouseSensitivity = 2f;
    float CameraVerticalRotation = 0f;
    private float CameraHorizontalRotation = 0f;
    bool locked = false;

    public float maxHorizontalAngle = 60f;
    public float followSpeed = 5f;

    void Start()
    {

       
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            locked = !locked;
            cursorLock();
        }
       


        

        //get mouse input
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Update horizontal rotation but clamp it
        CameraHorizontalRotation += inputX;
        CameraHorizontalRotation = Mathf.Clamp(CameraHorizontalRotation, -maxHorizontalAngle, maxHorizontalAngle);

        // Rotate the Camera separately (vertical movement)
        CameraVerticalRotation -= inputY;
        CameraVerticalRotation = Mathf.Clamp(CameraVerticalRotation, -90f, 90f);

        // Smoothly follow the sphere's forward direction
        Quaternion targetRotation = Quaternion.LookRotation(playerSphere.forward, Vector3.up);
        float targetYaw = targetRotation.eulerAngles.y;

        // Adjust rotation within limits
        float clampedYaw = Mathf.Clamp(targetYaw + CameraHorizontalRotation, targetYaw - maxHorizontalAngle, targetYaw + maxHorizontalAngle);

        // Set final camera rotation
        transform.rotation = Quaternion.Euler(CameraVerticalRotation, clampedYaw, 0f);

        // Keep camera positioned above the playerSphere
        transform.position = playerSphere.position + offset;

    }

    private void cursorLock()
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked; // locks cursor to center of screen

        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // free cursor
        }
    }



}
