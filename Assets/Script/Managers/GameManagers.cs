using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagers : MonoBehaviour
{
    //[SerializeField]

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
