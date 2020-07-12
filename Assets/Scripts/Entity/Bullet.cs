using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Team team;

    public Rigidbody2D rigiBullet;
    public float speedMove;
    public float timeLife = 2;
    public float damage;

    private void Start()
    {
        
    }

    public void InitBullet(Transform startPosition, Team team)
    {
        this.team = team;
        transform.position = startPosition.position + startPosition.up;
        transform.rotation = startPosition.rotation;

        MoveBullet();
    }

    private void MoveBullet()
    {
        rigiBullet.AddForce(transform.up * speedMove, ForceMode2D.Force);
        Destroy(gameObject, timeLife);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthSystem healthSystem = collision.GetComponent<HealthSystem>();

        if (healthSystem != null && (team != healthSystem.Team || GameManagers.instance.friendlyFire))
        {
            healthSystem.Damage(damage);
        }

        Destroy(gameObject);
    }
}
