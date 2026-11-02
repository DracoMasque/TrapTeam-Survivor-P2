using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTemp : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
