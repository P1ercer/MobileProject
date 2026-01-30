using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BaseHealth : MonoBehaviour
{
    public static BaseHealth Instance;
    public string levelToLoad;


    public int maxHealth = 100;
    public int currentHealth;

    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;

        TowerUIManager.Instance?.UpdateBaseHealthUI(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log($"Base took {damage} damage. Remaining HP: {currentHealth}");

        TowerUIManager.Instance?.UpdateBaseHealthUI(currentHealth, maxHealth);

        if (currentHealth <= 0)
            OnBaseDestroyed();
    }

    private void OnBaseDestroyed()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
