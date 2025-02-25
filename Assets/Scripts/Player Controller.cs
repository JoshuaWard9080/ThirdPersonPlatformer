using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float playerSpeed;

    private Rigidbody rb;

    public void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
    }

    public void MovePlayer(Vector3 input)
    {
        rb.AddForce(input * playerSpeed);
    }
}
