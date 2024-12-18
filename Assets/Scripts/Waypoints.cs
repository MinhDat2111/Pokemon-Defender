using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform[] waypoints; // Mảng chứa các waypoint

    void Start()
    {
        // Lấy tất cả các waypoint con trong đối tượng này
        Transform[] allWaypoints = GetComponentsInChildren<Transform>();

        // // Nếu có ít nhất 2 phần tử, tạo một mảng mới để chứa các waypoint
        // if (allWaypoints.Length > 1)
        // {
        //     waypoints = new Transform[allWaypoints.Length - 1];
        //     for (int i = 1; i < allWaypoints.Length; i++) // Bắt đầu từ 1 để bỏ qua phần tử đầu tiên
        //     {
        //         waypoints[i - 1] = allWaypoints[i]; // Chuyển các waypoint vào mảng mới
        //     }
        // }
        // else
        // {
        //     waypoints = new Transform[0]; // Nếu không có waypoint nào, khởi tạo mảng rỗng
        // }
    }
}
