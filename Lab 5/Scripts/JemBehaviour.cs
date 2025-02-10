using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JemBehaviour : MonoBehaviour
{
    public TMP_Text statusText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            statusText.text = "MISS: Jem hit the ground";
        }
        Destroy(gameObject);
    }
}
