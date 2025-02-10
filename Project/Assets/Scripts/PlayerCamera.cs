using UnityEngine;
using DG.Tweening;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;

    float xRotation;
    float yRotation;

    public KeyCode adsKey = KeyCode.Mouse1; // right click

    private void Start() // lock the cursor and make it invisible
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Debug.Log(GetComponent<Camera>().fieldOfView);

        // Check for right mouse button input
        if (Input.GetKeyDown(adsKey))
        {
            // Change field of view to zoomedFOV when aiming down sights
            GetComponent<PlayerCamera>().DoFOV(45);
        }
        else if (Input.GetKeyUp(adsKey))
        {
            // Restore normal field of view when not aiming down sights
            GetComponent<PlayerCamera>().DoFOV(60);
        }
    }

    private void FixedUpdate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // handle the rotations
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // restrict cam movement to 90 degrees

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void DoFOV(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }
}
