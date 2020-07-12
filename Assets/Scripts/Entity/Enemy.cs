using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HealthSystem
{
    public Team CurrentTeam { get { return Team.Enemy; } private set { } }

    public Rigidbody2D rigidbody2;
    public Transform transformEnemy;
    public Weapon weapon;
    public float speedMove;
    public float speedRotation;
    public float distanceStopToPlayer;
    public float distanceFireToPlayer;
    public int damageCollision;

    void Start()
    {
        weapon.ActivateWeapon(CurrentTeam);
        InitHeath(CurrentTeam);
        StartCoroutine(EnemyController());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();

        if (healthSystem != null && (Team != healthSystem.Team || GameManagers.instance.friendlyFire))
        {
            healthSystem.Damage(damageCollision);
        }
    }

    private IEnumerator EnemyController()
    {
        Transform target = GameManagers.instance.player.transform;

        while (target != null)
        {
            Vector3 targetDir = target.position - transformEnemy.position;

            transformEnemy.rotation = Quaternion.Lerp(transformEnemy.rotation, Quaternion.FromToRotation(Vector3.up, targetDir), Time.deltaTime * speedRotation);

            if (targetDir.magnitude > distanceStopToPlayer)
            {
                rigidbody2.AddForce(transformEnemy.up * speedMove * Time.deltaTime, ForceMode2D.Impulse);
                animator.SetFloat("SpeedMove", 1);
            }
            else
                animator.SetFloat("SpeedMove", 0);

            if (targetDir.magnitude < distanceFireToPlayer)
                weapon.Fire();
            else
                weapon.StopFire();

            yield return null;
        }

        weapon.StopFire();
    }
}
