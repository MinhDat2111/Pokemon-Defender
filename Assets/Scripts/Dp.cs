using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dp : MonoBehaviour
{
    private int dp = 0; // Giá trị dp hiện tại
    public TextMeshProUGUI dpText; // Tham chiếu đến TextMeshProUGUI để hiển thị dp

    private float increaseInterval = 1f; // Thời gian giữa các lần tăng dp
    private float nextIncreaseTime = 0f; // Thời gian tiếp theo để tăng dp
    public int CurrentDp
    {
        get { return dp; }
        set { dp = value; }
    }
    void Start()
    {
        UpdateDpText(); // Cập nhật UI ban đầu
    }

    void Update()
    {
        // Kiểm tra xem đã đến thời điểm để tăng dp chưa
        if (Time.time >= nextIncreaseTime)
        {
            IncreaseDp(); // Tăng giá trị dp
            nextIncreaseTime = Time.time + increaseInterval; // Cập nhật thời gian tiếp theo
        }
    }

    void IncreaseDp()
    {
        dp++; // Tăng giá trị dp
        UpdateDpText(); // Cập nhật UI để hiển thị giá trị mới
    }

    void UpdateDpText()
    {
        dpText.text = dp.ToString(); // Cập nhật văn bản hiển thị dp
    }
}
