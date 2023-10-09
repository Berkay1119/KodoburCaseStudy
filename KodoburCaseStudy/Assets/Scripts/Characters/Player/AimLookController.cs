using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AimLookController : MonoBehaviour
{
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private new Camera camera;
    [SerializeField] private float sensitivity;
    private float _rotationAroundX;
    private float _rotationAroundY;
    
    void Update()
    {
        float mouseX = -Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        
        _rotationAroundX -= mouseY;
        _rotationAroundY -= mouseX;
        _rotationAroundX = Mathf.Clamp(_rotationAroundX, -90, 90);

        camera.transform.localRotation = Quaternion.Euler(_rotationAroundX, _rotationAroundY, 0);
        playerMovementController.transform.localRotation=Quaternion.Euler(0, _rotationAroundY, 0);
    }
}
