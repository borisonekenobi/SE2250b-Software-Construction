using UnityEngine;

public class Paystub : Currency
{
    void Start()
    {
        Value = Random.Range(5000, 200000) / 100f;
    }
}
