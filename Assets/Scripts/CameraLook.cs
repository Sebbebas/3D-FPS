using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float mouseSensitivity = 100f;

    private float mouseX;
    private float mouseY;
    private float xRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if(Cursor.lockState != CursorLockMode.Locked) { Debug.Log("lock"); return; }

        mouseX = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, -0f);
        player.Rotate(Vector3.up * mouseY);
    }
}
