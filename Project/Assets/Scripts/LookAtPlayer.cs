using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform cam;

    void LateUpdate()
    {
        transform.LookAt(cam);
    }
}
