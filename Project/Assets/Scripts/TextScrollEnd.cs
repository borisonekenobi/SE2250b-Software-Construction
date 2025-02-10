using TMPro;
using UnityEngine;

public class TextScrollEnd : MonoBehaviour
{
    public float ScrollSpeed;
    public KeyCode ContinueKey;
    public TMP_Text Text;

    void Start()
    {
        Text.SetText($"After his dangerous journey into the vault’s depths, Kirk Anderson found what he had been looking for. A coveted software engineering internship for the summer of 2024. Kirk was surprised to find that it had been easier slaying the vault’s dangers than working with conventional recruiters. Nevertheless, this newfound step in his career will surely pay dividends in the future.\n\n^Kirk would then go on to add his conquest of the vault to his LinkedIn experience tab, much to the disappointment of his family and close associates.\n\nPress [{ContinueKey}] to continue……");
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * ScrollSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(ContinueKey))
        {
            Application.Quit();
        }
    }
}
