using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float currentHealthPoints;

    public Team Team { get; private set; }

    public delegate void EventDamage(float points, float maxPoints);
    public EventDamage eventDamage;

    public Animator animator;
    public float healthPoints;
    [Range(0, 1)]
    public float armor;

    public void InitHeath(Team team)
    {
        Team = team;
        currentHealthPoints = healthPoints;
    }

    public void Damage(float valueDamage)
    {
        currentHealthPoints -= valueDamage * (1 - armor);

        if (currentHealthPoints <= 0)
            currentHealthPoints = 0;

        if (eventDamage != null)
            eventDamage.Invoke(currentHealthPoints, healthPoints);

        CheakDeath();
    }

    private void CheakDeath()
    {
        if (currentHealthPoints <= 0)
        {
            animator.SetTrigger("TriggerDestroy");
            Destroy(gameObject, 0.25f);
        }
    }
}
