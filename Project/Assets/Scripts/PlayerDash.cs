using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMovement pm; // reference to movement script

    [Header("Dashing")]
    public float dashForce;
    public float upwardForce;
    public float dashDuration;
    public float maxYDashSpeed;

    [Header("Cooldown")]
    public float dashCooldown;
    private float dashCooldownTime;

    [Header("Input")]
    public KeyCode dashKey;

    [Header("CameraEffects")]
    public PlayerCamera cam;
    public float dashFOV;

    [Header("Setting")]
    public bool useCameraForward;
    public bool allowAllDirections;
    public bool disableGravity;
    public bool resetVel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }
        if (dashCooldownTime > 0)
        {
            dashCooldownTime -= Time.deltaTime;
        }
    }

    private void Dash()
    {
        if (dashCooldownTime > 0 || pm.mState == PlayerMovement.MovementState.dashing) // Check if already dashing or on cooldown
            return;
        else dashCooldownTime = dashCooldown; // if we can dash, set the cooldown after dash
        pm.dashing = true;
        pm.mState = PlayerMovement.MovementState.dashing; // Set movement state to dashing
        pm.maxYSpeed = maxYDashSpeed;

        cam.DoFOV(dashFOV); // zoom in FOV on dash

        Transform forwardT;
        // if user camera forward is enabled, only dash in camera direction, else use the orientation to dictate dash direction
        if (useCameraForward)
            forwardT = playerCam;
        else
            forwardT = orientation;

        Vector3 direction = GetDirection(forwardT);
        Vector3 forceToApply = direction * dashForce + orientation.up * upwardForce;

        if (disableGravity)
            rb.useGravity = false;

        delayedForce = forceToApply;
        Invoke(nameof(DelayDash), 0.025f);
        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForce;

    private void DelayDash()
    {
        if (resetVel)
            rb.velocity = Vector3.zero;
        rb.AddForce(delayedForce, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        pm.dashing = false;
        pm.maxYSpeed = 0;
        if (disableGravity) rb.useGravity = true;

        cam.DoFOV(60f); // reset the FOV back to the default
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction;
        if (allowAllDirections) direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else direction = forwardT.forward;

        if (verticalInput == 0 && horizontalInput == 0) direction = forwardT.forward;

        return direction.normalized;
    }
}