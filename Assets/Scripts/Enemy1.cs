using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    protected override void Start()
    {
        base.Start(); // Gọi phương thức Start của lớp cha (Enemy)

        // Lấy Waypoints từ tag "Waypoints2"
        waypoints = GameObject.FindGameObjectWithTag("Waypoints2").GetComponent<Waypoints>();

        // Kiểm tra xem có tìm thấy Waypoints không
        if (waypoints == null)
        {
            Debug.LogError("Waypoints2 not found!");
        }
    }

    protected override void MoveTowardsWaypoint()
    {
        base.MoveTowardsWaypoint(); // Gọi phương thức của lớp cha
    }

    protected override void CheckForAttack()
    {
        base.CheckForAttack(); // Gọi phương thức của lớp cha
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage); // Gọi phương thức TakeDamage của lớp cha
    }

    protected override void Attack(Transform target)
    {
        base.Attack(target); // Gọi phương thức tấn công của lớp cha
        
    }
}
