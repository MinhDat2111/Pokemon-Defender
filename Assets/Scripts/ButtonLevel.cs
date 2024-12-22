using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject myButton; // Tham chiếu đến Button

    void Start()
    {
        // Ẩn Button khi bắt đầu
        myButton.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Select Level Scene")
        {
            if (PlayerPrefs.GetString("PreviousScene") == "Win Scene")
            {
                myButton.SetActive(true); // Hiển thị Button
            }
        }
    }
}
