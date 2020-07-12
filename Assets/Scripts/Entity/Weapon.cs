using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Team team;
    private bool isFire;

    public GameObject gameObjectWeapon;
    public Transform transformWeapon;
    public Bullet bullet;
    public string nameWeapon;
    public float magazine;
    public float timeReloading;
    public float timeFire;

    public void ActivateWeapon(Team team)
    {
        this.team = team;
        gameObjectWeapon.SetActive(true);
        StartCoroutine(FireProcess());
    }

    public void DeactivateWeapon()
    {
        StopFire();
        gameObjectWeapon.SetActive(false);
        StopAllCoroutines();
    }

    public void Fire()
    {
        isFire = true;
    }

    public void StopFire()
    {
        isFire = false;
    }

    private IEnumerator FireProcess()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        float currentTimeFire = 0;
        float currentMagazin = magazine;
        float currentTimeReloading = 0;

        while (true)
        {
            yield return wait;

            if (isFire && currentTimeFire <= 0 && currentMagazin > 0)
            {
                Bullet bulletTemp = Instantiate(bullet);
                bulletTemp.InitBullet(transformWeapon, team);

                currentTimeFire = timeFire;
                currentMagazin--;
            }

            if (currentTimeFire > 0)
                currentTimeFire -= Time.deltaTime;

            if (currentMagazin == 0 && currentTimeReloading <= 0)
                currentTimeReloading = timeReloading;

            if (currentTimeReloading > 0)
                currentTimeReloading -= Time.deltaTime;

            if (currentTimeReloading <= 0 && currentMagazin == 0)
                currentMagazin = magazine;
        }
    }
}
