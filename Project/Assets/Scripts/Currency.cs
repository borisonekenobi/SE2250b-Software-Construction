using UnityEngine;

public abstract class Currency : Collectible
{
    public float Value;
    public float SpinSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(0, SpinSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            PlayerScript.PickUp(this);
            Destroy(gameObject);
        }
    }
}
