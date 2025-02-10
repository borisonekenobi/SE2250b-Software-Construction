using System.Collections;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public TMP_Text ammoNum;
    public TMP_Text hitMarker;
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;

    float timeSinceLastShot;

    private void Start()
    {
        gunData.currentAmmo = gunData.magSize + 1;
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (cam == null) return;

        if (!gunData.reloading && gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        if (gunData.currentAmmo > 0)
        {
            yield return new WaitForSeconds((gunData.reloadTime) * (2f / 3f));

            gunData.currentAmmo = gunData.magSize + 1;
        }
        else
        {

            yield return new WaitForSeconds(gunData.reloadTime);

            gunData.currentAmmo = gunData.magSize;
        }

        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f) && gameObject.activeSelf == true;

    private void Shoot()
    {
        if (cam == null) return;
        if (gunData.currentAmmo <= 0) return;
        if (!CanShoot()) return;

        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance))
        {
            IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
            damageable?.Damage(gunData.damage);
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                StartCoroutine(AddText());
            }
        }

        gunData.currentAmmo--;
        ammoNum.SetText(gunData.currentAmmo.ToString());
        timeSinceLastShot = 0;
        OnGunShot();
    }

    private IEnumerator AddText()
    {
        hitMarker.SetText("\\ /\n/ \\");
        yield return new WaitForSeconds(0.2f);
        hitMarker.SetText("");
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        ammoNum.SetText(gunData.currentAmmo.ToString());

        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance);
    }

    private void OnGunShot() { }
}
