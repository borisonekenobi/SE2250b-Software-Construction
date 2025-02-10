using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
