using System.Collections.Generic;
using UnityEngine;



public class BrainController : MonoBehaviour
{
    [SerializeField] NPCStateMachine stateMachine;

    [SerializeField] Schedule npcSchedule;

    NPCTask currentTask = NPCTask.Idle;

    public GameObject[] famerObjects { get; set; }

    public Transform homeTransform { get; set; }
    public Transform chopDownTransform { get; set; }

    Stack<State> states;

    void Start()
    {
        states = new Stack<State>();
        //homeTransform = this.transform;
        //famerObjects = GameObject.FindGameObjectsWithTag("Vegetable");
        //chopDownTransform = GameObject.FindGameObjectWithTag("Tree").transform;
    }

    void OnEnable()
    {
        if (WorldTime.Instance != null)
        {
            //homeTransform = this.transform;
            WorldTime.Instance.OnHourAction += OnHourChanged;
        }
    }

    void OnDisable()
    {
        if (WorldTime.Instance != null)
        {
            WorldTime.Instance.OnHourAction -= OnHourChanged;
        }
    }

    public void OnHourChanged()
    {
        if (npcSchedule.dailyList.Count == 0 || npcSchedule == null)
        {
            return;
        }

        NPCTask scheduleTask = GetTask(WorldTime.Instance.hour);

        if (scheduleTask != currentTask)
        {
            currentTask = scheduleTask;
            switch (currentTask)
            {
                case NPCTask.Famer:
                    stateMachine.SwitchState(new NPCFarmerState(stateMachine));
                    break;
                case NPCTask.GoOut:
                    stateMachine.SwitchState(new NPCGoOutState(stateMachine));
                    break;
                case NPCTask.GoHome:
                    stateMachine.SwitchState(new NPCGoHomeState(stateMachine));
                    break;
                default:
                    Debug.Log("Not Exits State");
                    break;
            }
        }
    }


    public NPCTask GetTask(int hour)
    {
        foreach (var task in npcSchedule.dailyList)
        {
            if (task.hour == hour)
            {
                return task.task;
            }
        }

        return NPCTask.Idle;
    }
}
