using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    public bool IsAlive => health > 0;
    [SerializeField] private UnityEvent onTakeDamage = new UnityEvent();
    [SerializeField] private UnityEvent onDie = new UnityEvent();

    public void TakeDamage(float amount) 
    {
        if (health <= 0.0f)
            return;

        health -= amount;

        if (health <= 0.0f)
            Die();
        else
            onTakeDamage?.Invoke();
    }

    private void Die()
    {
        print("I'm dead!");

        onDie?.Invoke();
    }
}
