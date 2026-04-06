using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] Image healthUI;
    [SerializeField] float maxHealth;

    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log(currentHealth);
    }


    void Update()
    {
        if (currentHealth <= 0)
        {
            //Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }

        healthUI.fillAmount = currentHealth / maxHealth;
    }


    public void DealDame(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= 5;
        }
    }

}
