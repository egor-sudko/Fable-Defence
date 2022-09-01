using UnityEngine;
using UnityEngine.UI;

public abstract class BaseEnemy : MonoBehaviour, IDamagable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float speed;

    private int currentHealth;

    [SerializeField] private Image healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Moving();
    }

    public virtual void Moving()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void ApplyDamage(int damageValue)
    {
        currentHealth -= damageValue;
        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " destroyed");
            RestartEnemy();
        }
        else
        {
            UpdateHealtBar();
        }
    }

    private void UpdateHealtBar()
    {
        float hpPercent = (float)currentHealth / maxHealth;
        healthBar.fillAmount = hpPercent;
    }

    private void RestartEnemy()
    {
        gameObject.SetActive(false);
        currentHealth = maxHealth;
        UpdateHealtBar();
    }
}
