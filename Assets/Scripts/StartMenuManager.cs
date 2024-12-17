using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    void Update()
    {
        // Kiểm tra xem người chơi có nhấn phím nào không
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Select Level Scene"); 
        }
    }
}
