using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    public CapsuleCollider CapsuleCollider;
    public TMP_Text StoryText;
    public KeyCode EnterKey;

    private bool hasCollided = false;

    void Update()
    {
        if (Input.GetKeyDown(EnterKey)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!hasCollided)
        {
            StoryText.SetText("Hmm, seems like the vault has been abandoned...");
            CapsuleCollider.radius = 0.5f;
            hasCollided = true;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            CapsuleCollider.transform.position = new Vector3(0, 2, -45);

            StoryText.SetText($"Click [{EnterKey}] to explore!");
        }
    }
}
