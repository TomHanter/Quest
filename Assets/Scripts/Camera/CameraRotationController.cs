using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotationController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // Ссылка на объект камеры
    [SerializeField] private float rotationSpeed = 100f; // Скорость вращения

    private Controls controls;
    private float rotationInput;

    private void Awake()
    {
        controls = new Controls();
        controls.Game.CameraRotate.performed += ctx => rotationInput = ctx.ReadValue<float>();
        controls.Game.CameraRotate.canceled += ctx => rotationInput = 0f;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        if (rotationInput != 0)
        {
            cameraTransform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
        }
    }
}
