using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



enum Village
{
    NPC_Female,
    NPC_Male
}



public class WorldTime : MonoBehaviour
{
    public static WorldTime Instance;

    public List<Transform> npcHome;
    public List<Transform> farmPos;
    [SerializeField] ObjectPooling objectPooling;
    [SerializeField] float minutesOneHour = 20;

    public event Action OnHourAction;
    public event Action GoOutAction;

    float minutesCount = 0;
    public int hour = 6;
    public int day = 0;

    public bool isGoOut = false;

    float percent = 10f;

    public List<SpawnAfterDestroy> vegetableList { get; set; }
    public List<SpawnAfterDestroy> treeList { get; set; }


    int femaleType = 9; // Add 1 for random
    int maleType = 17; // Add 1 for random

    void Awake()
    {
        Instance = this;
        SetupPos();

    }
    void Start()
    {


    }

    void SetupPos()
    {
        vegetableList = new List<SpawnAfterDestroy>();
        treeList = new List<SpawnAfterDestroy>();

        foreach (var pos in GameObject.FindGameObjectsWithTag("House"))
        {
            npcHome.Add(pos.transform);
        }


        FindObject("Vegetable", vegetableList);
        FindObject("Tree", treeList);
    }

    void OnEnable()
    {
        GoOutAction += SpawnNPC;
    }

    void Update()
    {
        if (hour == 5 && !isGoOut)
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

    void FindObject(string name, List<SpawnAfterDestroy> list)
    {
        SpawnAfterDestroy[] vegetables = FindObjectsByType<SpawnAfterDestroy>(FindObjectsInactive.Include, sortMode: FindObjectsSortMode.None);

        foreach (var vegetable in vegetables)
        {
            if (vegetable.gameObject.CompareTag(name))
            {
                list.Add(vegetable);
            }
        }
    }

    void SpawnNPC()
    {
        foreach (var pos in npcHome)
        {
            float randomPercent = Random.Range(0, 100);
            if (randomPercent - percent <= 0f)
            {
                string villageName = RandomVillageGender();
                if (villageName != "")
                {
                    BrainController brainController = objectPooling.GetObjectPooled(villageName, pos.position).gameObject.GetComponent<BrainController>();
                    brainController.homeTransform = pos;
                }
            }
        }
    }

    string RandomVillageGender()
    {
        int randomGenderByNum = Random.Range(1, 11);

        if (randomGenderByNum % 2 == 0)
        {
            string female = Village.NPC_Female.ToString();
            int typeNumber = Random.Range(1, femaleType);
            string type = typeNumber.ToString();
            Debug.Log("Type: " + type);
            Debug.Log(string.Concat(female, type));
            return string.Concat(female, type);
        }
        return "";
    }
}
