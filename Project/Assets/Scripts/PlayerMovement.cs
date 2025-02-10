using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float groundDrag;
    public float walkSpeed;
    public float sprintSpeed;
    public float dashSpeed;
    public float dashSpeedChangeFactor;
    public float maxYSpeed;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    public float startYScale;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Keybinds")]
    public KeyCode jumpKey;
    public KeyCode sprintKey;
    public KeyCode crouchKey;
    public KeyCode dashKey;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public MovementState mState;

    public enum MovementState // keeps track of different movement states
    {
        walking,
        sprinting,
        crouching,
        dashing,
        air
    }

    public bool dashing;

    private void Start() // get the reference to the rigid body so we can move and then freeze it so it doesnt fall over
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;

        startYScale = transform.localScale.y;
    }
    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // Debugging movement stuff
        //Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f), Color.green);
        //Debug.Log(mState);
        //Debug.Log(Input.GetKeyDown(crouchKey));
        //Debug.Log(moveSpeed);

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (mState == MovementState.walking || mState == MovementState.sprinting || mState == MovementState.crouching)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        //Debug.Log("Grounded: " + grounded); // fixing jump

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (mState == MovementState.dashing) return;

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput; // move in the direction we are facing, aka the orientation

        if (OnSlope() && !exitingSlope)
            rb.AddForce(20f * moveSpeed * GetSlopeMoveDirection(), ForceMode.Force);
        if (rb.velocity.y > 0)
            rb.AddForce(Vector3.down * 80f, ForceMode.Force);

        // on ground
        if (grounded)
            rb.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(10f * airMultiplier * moveSpeed * moveDirection.normalized, ForceMode.Force);

        // turn gravity off while on slope
        //rb.useGravity = !OnSlope();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            //Debug.Log("Jump key pressed."); // fixing jump
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown); // allows continuous jumping, mnaybe implement bunnyhopping or double jumping here
        }

        if (Input.GetKeyDown(crouchKey) && grounded)
        {
            Debug.Log("crouch pressed");
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z); // "shrinking" the player
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse); // make sure the model isnt floating
        }

        if (Input.GetKeyUp(crouchKey)) // stop crouching
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private float wantedSpeed;
    private float prevWantedSpeed;
    private MovementState lastState;
    private bool keepMomentum;

    // deals with the different movement states
    private void StateHandler()
    {
        // sprinting state
        if (grounded && Input.GetKey(sprintKey) && !Input.GetKey(dashKey)) // have to put not equal to dashkey or else the state on the ground is recognized as either walking or sprinting
        {
            mState = MovementState.sprinting;
            wantedSpeed = sprintSpeed;
        }
        // walking state
        else if (grounded && !Input.GetKey(dashKey))
        {
            mState = MovementState.walking;
            wantedSpeed = walkSpeed;
        }
        // crouch state
        else if (Input.GetKey(crouchKey))
        {
            mState = MovementState.crouching;
            wantedSpeed = crouchSpeed;
        }
        // dash state 
        else if (dashing)
        {
            mState = MovementState.dashing;
            wantedSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
            dashing = false;
        }
        // in air
        else
        {
            mState = MovementState.air;

            // keep momentum after jumping/strafing in air, make movement smoother
            if (wantedSpeed < sprintSpeed)
            {
                wantedSpeed = walkSpeed;
            }
            else wantedSpeed = sprintSpeed;
        }

        bool desiredMoveSpeedHasChanged = wantedSpeed != prevWantedSpeed;
        if (lastState == MovementState.dashing) keepMomentum = true;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                moveSpeed = wantedSpeed;
            }
        }

        prevWantedSpeed = wantedSpeed;
        lastState = mState;
    }

    // lerp, or linearly interpolate, and smoothly transition between movement speeds and momentum
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(wantedSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, wantedSpeed, time / difference);
            time += Time.deltaTime * boostFactor;
            yield return null;
        }

        moveSpeed = wantedSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }

    // checks if player object is on ground by shooting ray
    private bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    // dont exceed max speed
    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        // limit y velocity
        if (maxYSpeed != 0 && rb.velocity.y > maxYSpeed)
            rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
    }

    private void Jump()
    {
        exitingSlope = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    private float speedChangeFactor;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}