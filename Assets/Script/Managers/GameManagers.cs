using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagers : MonoBehaviour
{
    [SerializeField] float minutesOneHour = 20;

    public static GameManagers Instance;
    public event Action OnHourAction;

    float minutesCount = 0;
    public int hour = 6;
    public int day = 0;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        CountTime();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CountTime()
    {
        minutesCount += Time.deltaTime;
        if (minutesCount >= minutesOneHour)
        {
            hour++;
            minutesCount = 0;

        }

        if (hour == 23)
        {
            hour = 0;
            day++;
        }

        OnHourAction?.Invoke();
    }
}
