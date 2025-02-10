using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public GameObject LightEmitter;
    public Light PointLight1;
    public Light PointLight2;
    public Material LightEmitterLocked;
    public Material LightEmitterUnlocked;
    public List<GameObject> AliveEnemies;

    public KeyCode OpenDoor;
    public TMP_Text StoryText;

    private bool locked;
    private bool playerInArea;

    void Start()
    {
        locked = true;

        PointLight1.color = Color.red;
        PointLight2.color = Color.red;

        LightEmitter.GetComponent<Renderer>().material = LightEmitterLocked;
    }

    void Update()
    {
        if (Input.GetKeyDown(OpenDoor) && !locked && playerInArea) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerObj"))) return;

        playerInArea = true;

        if (!locked) StoryText.SetText($"Click [{OpenDoor}] to enter next level");
        else StoryText.SetText("Door is locked!");
        if (!AllEnemiesDead()) return;

        PointLight1.color = Color.green;
        PointLight2.color = Color.green;

        LightEmitter.GetComponent<Renderer>().material = LightEmitterUnlocked;

        locked = false;
        StoryText.SetText($"Click [{OpenDoor}] to enter next level");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StoryText.SetText(string.Empty);
            playerInArea = false;
        }
    }

    private bool AllEnemiesDead()
    {
        foreach (GameObject enemy in AliveEnemies)
        {
            if (enemy != null) return false;
        }

        return true;
    }
}
