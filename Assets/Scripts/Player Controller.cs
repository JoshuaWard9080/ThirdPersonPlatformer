using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform orientation;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    Vector3 moveDir;

    private Rigidbody rb;

    

    public void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
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
        rb.AddForce(moveDir.normalized * playerSpeed, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVelocity.magnitude > playerSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * playerSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }
}
