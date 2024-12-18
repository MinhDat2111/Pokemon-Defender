using UnityEngine;

public class QuitGameButton : MonoBehaviour
{
    public void OnQuitButtonClick()
    {
        #if UNITY_EDITOR
            // Nếu đang chạy trong Unity Editor, dừng play mode 
            UnityEditor.EditorApplication.isPlaying = false;
            
        #else
            // Nếu đang chạy trong build game, thoát game
            Application.Quit();
        #endif
    }
}
