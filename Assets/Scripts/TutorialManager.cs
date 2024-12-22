using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorials; // Danh sách các GameObject tutorial
    private int currentLineIndex = 0;

    void Start()
    {
        // Hiển thị dòng tutorial đầu tiên
        ShowCurrentTutorial();
    }

    void Update()
    {
        // Kiểm tra nếu người chơi nhấn chuột
        if (Input.GetMouseButtonDown(0))
        {
            NextTutorial();
        }
    }

    void ShowCurrentTutorial()
    {
        // Ẩn tất cả các tutorial
        foreach (GameObject tutorial in tutorials)
        {
            tutorial.SetActive(false);
        }

        // Hiển thị tutorial hiện tại
        if (currentLineIndex < tutorials.Length)
        {
            tutorials[currentLineIndex].SetActive(true);
        }
    }

    void NextTutorial()
    {
        currentLineIndex++;
        if (currentLineIndex < tutorials.Length)
        {
            ShowCurrentTutorial();
        }
        else
        {
            EndTutorial();
        }
    }

    void EndTutorial()
    {
        // Ẩn tất cả tutorial khi kết thúc
        foreach (GameObject tutorial in tutorials)
        {
            tutorial.SetActive(false);
        }
        SceneManager.LoadScene("Start Scene");
    }
}
