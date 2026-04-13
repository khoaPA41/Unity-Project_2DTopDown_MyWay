using UnityEngine;

public class Health : Status
{
    [SerializeField] GameObject GameOverPanel;

    void Update()
    {
        if (currentValue <= 0)
        {
            GameOverPanel.SetActive(true);
        }

        valueImage.fillAmount = (currentValue / maxValue);
    }


    public void DealDame(int damage)
    {
        currentValue = Mathf.Max(currentValue - damage, 0) * Time.deltaTime * speedToFill;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentValue -= 5;
        }
    }

}
