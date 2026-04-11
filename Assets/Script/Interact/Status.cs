using UnityEngine;
using UnityEngine.UI;

public abstract class Status : MonoBehaviour
{
    public Image valueImage;
    public float maxValue;

    public float currentValue;

    void Start()
    {
        currentValue = maxValue;
        valueImage.fillAmount = currentValue / maxValue;
    }
}
