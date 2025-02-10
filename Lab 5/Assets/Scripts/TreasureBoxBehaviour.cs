using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreasureBoxBehaviour : MonoBehaviour
{
    CharacterController controller;
    Vector3 moveDirection;
    public float moveSpeed;

    public TMP_Text scoreText;
    public TMP_Text statusText;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal") * moveSpeed;
        moveDirection.z = Input.GetAxis("Vertical") * moveSpeed;

        //Debug.Log(gameObject.transform.position);
        controller.Move(moveDirection);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Jem")
        {
            statusText.text = "WIN: Jem was collected";
            scoreText.text = (int.Parse(scoreText.text) + 1).ToString();
        }
        else
        {
            statusText.text = "Clutter Collected";
        }
        Destroy(collision.gameObject);
    }
}
