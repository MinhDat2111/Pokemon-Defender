using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public void RestartTheGame()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Khởi động lại màn chơi hiện tại
    }
}
