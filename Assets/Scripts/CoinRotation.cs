using UnityEngine;

public class CoinRotation : MonoBehaviour
{

    public float rotateSpeed;

    private Vector3 startPosition;
    [SerializeField] private float frequency = 5;
    [SerializeField] private float magnitude = 5;
    [SerializeField] private float offset = 0;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        transform.position = startPosition + transform.up * Mathf.Sin(Time.time + frequency + offset) * magnitude;
    }
}
