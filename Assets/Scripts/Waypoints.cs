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
    }
}
