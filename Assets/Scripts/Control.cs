using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public void RestartTheGame()
    { 
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Khởi động lại màn chơi hiện tại
    }
    public void NextLevel()
    {
        // Lưu tên của scene hiện tại vào PlayerPrefs với khóa "PreviousScene"
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save(); // Lưu lại thay đổi
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
}
