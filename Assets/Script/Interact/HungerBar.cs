using UnityEngine;

public class HungerBar : Status
{
    [SerializeField] Health health;
    [SerializeField] float healthDecrease;
    [SerializeField] float hungerValueDecrease;

    //void Start()
    //{
    //    health = GetComponent<Health>();
    //}


    void Update()
    {
        if (currentValue <= 0)
        {
            health.currentValue -= healthDecrease;
        }

        valueImage.fillAmount = currentValue / maxValue;
        currentValue -= hungerValueDecrease * Time.deltaTime;
    }

    public void AddHungerBar(int amount)
    {
        currentValue = Mathf.Min(currentValue + amount, 100);
    }
}
