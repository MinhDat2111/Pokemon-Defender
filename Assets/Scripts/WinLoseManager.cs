using UnityEngine;
using UnityEngine.UI; // Để sử dụng UI
using UnityEngine.SceneManagement;
using TMPro; // Để chuyển cảnh

public class WinLoseManager : MonoBehaviour // Đổi tên class từ GameManager thành WinManager
{
    public float stoneHealth; // Máu của viên đá trấn giữ
    public TextMeshProUGUI stoneHealthText;
    public TextMeshProUGUI WinLoseText; // Text hiển thị khi thua
    public GameObject background; // Background 
    public Button nextLevelButton; // Nút chuyển sang màn tiếp theo
    public Button restartButton; // Nút chơi lại

    private void Start()
    {
        // Ẩn nút và text khi bắt đầu trò chơi
        background.gameObject.SetActive(false);
        WinLoseText.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        stoneHealthText.text = stoneHealth.ToString();
        // Kiểm tra nếu máu viên đá <= 0, người chơi thua
        if (stoneHealth <= 0)
        {
            WinLose(false); // Thua
        }
    }

    public void TakeDamage(float damage)
    {
        stoneHealth -= damage; // Giảm máu viên đá
        Debug.Log("Energy Stone: "+ stoneHealth);
        if (stoneHealth <= 0)
        {
            WinLose(false); // Thua nếu máu <= 0
        }
    }

    public void WinGame()
    {
        WinLose(true); // Gọi hàm thắng
    }

    private void WinLose(bool won)
    {
        Time.timeScale = 0; // Dừng thời gian trong trò chơi

        background.gameObject.SetActive(true);
        WinLoseText.gameObject.SetActive(true);

        if (won)
        {
            WinLoseText.text = "You Win!";
            nextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            WinLoseText.text = "Game Over!";
            restartButton.gameObject.SetActive(true);
        }
    }
}
