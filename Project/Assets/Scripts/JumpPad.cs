using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Range(1000, 5000)]
    public float bounceHeight;

    void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * bounceHeight);
    }
}
