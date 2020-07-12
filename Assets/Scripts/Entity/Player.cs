using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : HealthSystem
{
    private int indexCurrentWeapon;

    public Team CurrentTeam { get { return Team.Player; } private set { } }

    public List<Weapon> weapons = new List<Weapon>();
    public Rigidbody2D rigidbody2;
    public Transform transformPlayer;
    public float speedMove;
    public float speedRotation;
    public int damageCollision;

    public void Start()
    {
        InitPlayer();
    }

    public void InitPlayer()
    {
        ChangeWeapon(0);
        InitHeath(CurrentTeam);

        StartCoroutine(MoveController());
        StartCoroutine(ShootController());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();

        if (healthSystem != null && (Team != healthSystem.Team || GameManagers.instance.friendlyFire))
        {
            healthSystem.Damage(damageCollision);
        }
    }

    private void ChangeWeapon(int weaponIndex)
    {
        if (weapons[indexCurrentWeapon] != null)
            weapons[indexCurrentWeapon].DeactivateWeapon();

        indexCurrentWeapon = weaponIndex;
        weapons[indexCurrentWeapon].ActivateWeapon(CurrentTeam);
        GameManagers.instance.battleUIController.SetNameWeapon(weapons[indexCurrentWeapon].nameWeapon);
    }

    private void NextWeapon()
    {
        int index = indexCurrentWeapon + 1;

        if (index >= weapons.Count)
            index = 0;

        ChangeWeapon(index);
    }

    private void PrevWeapon()
    {
        int index = indexCurrentWeapon - 1;

        if (index < 0)
            index = weapons.Count - 1;

        ChangeWeapon(index);
    }

    private void Move()
    {
        Vector3 vectorMove = GetMoveInput();

        rigidbody2.AddForce(vectorMove * speedMove * Time.deltaTime, ForceMode2D.Impulse);
        transformPlayer.rotation = Quaternion.Euler(transformPlayer.rotation.eulerAngles + GetAngleRotation(vectorMove) * speedRotation);
        animator.SetFloat("SpeedMove", vectorMove.magnitude > 0 ? 1 : 0);
    }

    private Vector3 GetAngleRotation(Vector3 vectorMove)
    {
        return - Vector3.forward * Input.GetAxis("Horizontal");
    }

    private Vector3 GetMoveInput()
    {
        return transformPlayer.up * Input.GetAxis("Vertical");
    }

    private IEnumerator MoveController()
    {
        while (true)
        {
            Move();

            yield return null;
        }
    }

    private IEnumerator ShootController()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                weapons[indexCurrentWeapon].Fire();
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                weapons[indexCurrentWeapon].StopFire();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                NextWeapon();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                PrevWeapon();
            }

            yield return null;
        }
    }
}
