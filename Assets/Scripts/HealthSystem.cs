using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 2; // ✅ Health starts at 2
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateHealthUI(currentHealth);
        }
    }
}
