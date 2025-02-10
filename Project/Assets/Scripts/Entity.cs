using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Rigidbody Rigidbody;

    public GameObject Player => GameObject.FindGameObjectWithTag("Player");//.GetComponentInChildren<CapsuleCollider>().gameObject;
    public Player PlayerScript => Player.GetComponentInParent<Player>();
}
