using UnityEngine;

public class TorchFlicker : MonoBehaviour
{
    public float minIntensity;
    public float maxIntensity;
    public float Timer;
    public GameObject fire;

    private new Light light;

    void Start()
    {
        light = GetComponentInChildren<Light>();
        InvokeRepeating(nameof(Flicker), Timer, Timer);
    }

    private void Flicker()
    {
        float intensity = light.intensity;
        float newIntensity = Random.Range(Mathf.Max(minIntensity, intensity - 0.5f), Mathf.Min(maxIntensity, intensity + 0.5f));
        
        light.intensity = newIntensity;
        fire.transform.localScale = new Vector3(newIntensity, newIntensity * 0.2f, newIntensity);
    }
}
