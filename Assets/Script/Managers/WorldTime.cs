using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldTime : MonoBehaviour
{
    public static WorldTime Instance;

    public List<Transform> npcHome;
    [SerializeField] ObjectPooling objectPooling;
    [SerializeField] float minutesOneHour = 20;

    public event Action OnHourAction;
    public event Action GoOutAction;

    float minutesCount = 0;
    public int hour = 6;
    public int day = 0;

    bool isGoOut = false;

    float percent = 10f;

    void Awake()
    {
        Instance = this;
        foreach (var pos in GameObject.FindGameObjectsWithTag("House"))
        {
            npcHome.Add(pos.transform);
        }
    }

    void OnEnable()
    {
        GoOutAction += SpawnNPC;
    }

    void Update()
    {
        if (hour >= 5 && !isGoOut)
        {
            isGoOut = true;
            GoOutAction?.Invoke();
        }

        CountTime();
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

    void SpawnNPC()
    {
        foreach (var pos in npcHome)
        {
            float randomPercent = Random.Range(0, 100);
            if (randomPercent - percent <= 0f)
            {
                BrainController brainController = objectPooling.GetObjectPooled(pos.position).GetComponent<BrainController>();
                brainController.homeTransform = pos;
            }
        }
    }
}
