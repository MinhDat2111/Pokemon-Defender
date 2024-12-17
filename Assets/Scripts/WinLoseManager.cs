using UnityEngine;
using UnityEngine.UI; // Để sử dụng UI
using UnityEngine.SceneManagement;
using TMPro; // Để chuyển cảnh

public class WinLoseManager : MonoBehaviour // Đổi tên class từ GameManager thành WinManager
{
    public float stoneHealth; // Máu của viên đá trấn giữ
    public TextMeshProUGUI gameOverText; // Text hiển thị khi thua
    public GameObject background; // Background 
    public Button nextLevelButton; // Nút chuyển sang màn tiếp theo
    public Button restartButton; // Nút chơi lại

    private void Start()
    {
        // Ẩn nút và text khi bắt đầu trò chơi
        background.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Kiểm tra nếu máu viên đá <= 0, người chơi thua
        if (stoneHealth <= 0)
        {
            GameOver(false); // Thua
        }
    }

    public void TakeDamage(float damage)
    {
        stoneHealth -= damage; // Giảm máu viên đá
        if (stoneHealth <= 0)
        {
            GameOver(false); // Thua nếu máu <= 0
        }
    }

    public void WinGame()
    {
        GameOver(true); // Gọi hàm thắng
    }

    private void GameOver(bool won)
    {
        Time.timeScale = 0; // Dừng thời gian trong trò chơi

        background.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);

        if (won)
        {
            gameOverText.text = "You Win!";
            nextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            gameOverText.text = "Game Over!";
            restartButton.gameObject.SetActive(true);
        }
    }
}
