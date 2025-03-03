using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform orientation;
    private Rigidbody rb;
    [SerializeField] private InputManager inputManager;
    
    Vector3 moveDir;

    [Header("Movement")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float maxAirSpeed;
    [SerializeField] private float groundDrag;

    [SerializeField] private float jumpForce;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float jumpCooldown;
    private bool readyToJump = true;


    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded;


    public void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnSpacePressed.AddListener(Jump);
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        SpeedControl();

        if (grounded)
            rb.linearDamping = groundDrag;
        else 
            rb.linearDamping = 0;
    }

    public void MovePlayer(Vector3 input)
    {
        moveDir = orientation.forward * input.z + orientation.right * input.x;
        if(grounded)
            rb.AddForce(moveDir.normalized * playerSpeed, ForceMode.Force);

        else if(!grounded)
            rb.AddForce(moveDir.normalized * playerSpeed * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVelocity.magnitude > playerSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * playerSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }

        if(!grounded && flatVelocity.magnitude > maxAirSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * maxAirSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    public void Jump()
    {
        if (readyToJump && grounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            readyToJump = false;

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
