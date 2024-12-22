using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Tham chiếu đến canvas của menu tạm dừng
    public Button resumeButton; // Tham chiếu đến nút Resume
    public Button restartButton;
    public Button backToMainMenu; // Tham chiếu đến nút Select Level

    private bool isPaused = false;

    void Start()
    {
        // Ẩn menu tạm dừng khi bắt đầu trò chơi
        pauseMenuUI.SetActive(false);

        // Đăng ký sự kiện cho các nút
        resumeButton.onClick.AddListener(Resume);
        restartButton.onClick.AddListener(Restart);
        backToMainMenu.onClick.AddListener(BackToMainMenu);
    }

    void Update()
    {
        // Kiểm tra xem phím ESC có được nhấn không
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true); // Hiện menu tạm dừng
        Time.timeScale = 0f; // Dừng thời gian trò chơi
        isPaused = true;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false); // Ẩn menu tạm dừng
        Time.timeScale = 1f; // Tiếp tục thời gian trò chơi
        isPaused = false;
    }

    void BackToMainMenu()
    {

        SceneManager.LoadScene("Start Scene");
    }
    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
