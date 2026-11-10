using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class ThirdPersonCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public float distance = 6f;
    public float zoomSensitivity = 0.02f;
    public float mouseSensitivity = 0.15f;

    float scrollInput;
    Vector2 lookInput;
    float pitch = 26.565f;
    bool cursorCaptured;

    void Start()
    {
        CaptureCursor();
    }

    void Update()
    {
        if (cursorCaptured)
        {
            // Mouse delta is a displacement, not a velocity; do not scale by deltaTime.
            transform.Rotate(Vector3.up, lookInput.x * mouseSensitivity, Space.World);
            pitch = Mathf.Clamp(pitch - lookInput.y * mouseSensitivity, -20f, 80f);
        }
        lookInput = Vector2.zero;
    }

    void LateUpdate()
    {
        // Scroll is a delta, so consume it once without multiplying by deltaTime.
        distance = Mathf.Clamp(distance - scrollInput * zoomSensitivity, 2f, 12f);
        scrollInput = 0f;

        Vector3 offsetDirection = Quaternion.Euler(pitch, transform.eulerAngles.y, 0f) * Vector3.back;
        cameraTransform.position = transform.position + offsetDirection * distance;
        cameraTransform.LookAt(transform.position);
    }

    void OnZoom(InputValue value)
    {
        scrollInput = value.Get<float>();
    }

    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void OnReleaseCursor(InputValue value)
    {
        if (value.isPressed)
            ReleaseCursor();
    }

    void OnCaptureCursor(InputValue value)
    {
        if (value.isPressed && !cursorCaptured)
            CaptureCursor();
    }

    void CaptureCursor()
    {
        lookInput = Vector2.zero;
        cursorCaptured = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ReleaseCursor()
    {
        cursorCaptured = false;
        lookInput = Vector2.zero;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            ReleaseCursor();
        }
    }

    void OnDisable()
    {
        scrollInput = 0f;
        ReleaseCursor();
    }
}
