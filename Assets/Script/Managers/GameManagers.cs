using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagers : MonoBehaviour
{
    public static GameManagers Instance;

    public List<SpawnAfterDestroy> vegetableList { get; set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        vegetableList = new List<SpawnAfterDestroy>();
        FindVegetable();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FindVegetable()
    {
        SpawnAfterDestroy[] vegetables = FindObjectsByType<SpawnAfterDestroy>(FindObjectsInactive.Include, sortMode: FindObjectsSortMode.None);

        foreach (var vegetable in vegetables)
        {
            if (vegetable.gameObject.CompareTag("Vegetable"))
            {
                vegetableList.Add(vegetable);
            }
        }
    }
}
