using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public static BaseHealth Instance;

    public int maxHealth = 100;
    public int currentHealth;

    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Base took " + damage + " damage. Remaining HP: " + currentHealth);

    }

}
