using System;
using System.Collections.Generic;
using UnityEngine;

public enum NPCTask
{
    Idle,
    Famer,
    Plant,
    ChopDown,
    Mining,
    Harvest,
    GoHome,
    GoOut
}



[Serializable]
public class ScheduleEvent
{
    [Range(0, 23)]
    public int hour;
    public NPCTask task;
}

[CreateAssetMenu(fileName = "Schedule", menuName = "Scriptable Objects/Schedule")]
public class Schedule : ScriptableObject
{
    public List<ScheduleEvent> dailyList = new List<ScheduleEvent>();
}
