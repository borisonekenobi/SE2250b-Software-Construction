using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScrollStart : MonoBehaviour
{
    public float ScrollSpeed;
    public KeyCode ContinueKey;
    public TMP_Text Text;

    void Start()
    {
        Text.SetText($"You are Kirk Anderson, renowned collector of strange goods and software engineering student in his spare time. Recently, you were informed of a mysterious treasure at the bottom of vault 4372, an abandoned genetic research facility that has long been discontinued. You do not know what awaits you in its depths, only that your desire for its treasure outweighs your caution for danger. What is this mysterious, McGuffin waiting for Kirk?\n\nPress [{ContinueKey}] to find out……");
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * ScrollSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(ContinueKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
