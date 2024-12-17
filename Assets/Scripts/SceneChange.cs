using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    // Tên của scene mà bạn muốn chuyển đến
    public string sceneToLoad;

    // Phương thức này sẽ được gọi khi người dùng click vào GameObject
    public void OnButtonClick()
    {
        // Chuyển đến scene đã chỉ định
        SceneManager.LoadScene(sceneToLoad);
    }
}
