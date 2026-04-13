using UnityEngine;

public class HungerBar : Status
{
    [SerializeField] Health health;
    [SerializeField] float healthDecrease;
    public float hungerValueDecrease;

    void Update()
    {
        if (currentValue <= 0)
        {
            health.currentValue -= healthDecrease;
        }

        valueImage.fillAmount = (currentValue / maxValue);

    }

    public void AddHungerBar(int amount)
    {
        currentValue = Mathf.Min(currentValue + amount, 100);
    }

    public void SubstractHungerBar()
    {
        currentValue -= hungerValueDecrease * Time.deltaTime * speedToFill;
    }
}
