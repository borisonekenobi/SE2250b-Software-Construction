using TMPro;
using UnityEngine;

public class Player : Mob
{
    public float Score;
    public Transform Spawn;

    public TMP_Text HealthNum;
    public TMP_Text ScoreNum;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.freezeRotation = true;

        Reset();
    }

    void Update()
    {
        HealthNum.text = $"{Health}";
        ScoreNum.text = $"${Score:0.00}";
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        if (Health <= 0) Reset();
    }

    public void PickUp(Currency currency)
    {
        Score += currency.Value;
    }

    private void Reset()
    {
        transform.position = Spawn.position;
        Health = 200;
    }
}
