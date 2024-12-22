using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected override void Start()
    {
        base.Start(); // Gọi phương thức Start của lớp cha (Enemy)

        // Lấy Waypoints từ tag "Waypoints2"
        waypoints = GameObject.FindGameObjectWithTag("Waypoints3").GetComponent<Waypoints>();

        // Kiểm tra xem có tìm thấy Waypoints không
        if (waypoints == null)
        {
            Debug.LogError("Waypoints2 not found!");
        }
    }

    protected override void MoveTowardsWaypoint()
    {
        if (waypointIndex < waypoints.waypoints.Length && targetAlly == null)
        {
            Vector3 direction = waypoints.waypoints[waypointIndex].position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Đổi từ radian sang độ 
            if (angle >= -45 && angle < 45)
            {
                animator.SetTrigger("MoveRight");
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (angle >= 45 && angle < 135)
            {
                animator.SetTrigger("MoveUp");
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (angle >= 135 || angle < -135)
            {
                animator.SetTrigger("MoveLeft");
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (angle >= -135 && angle < -45)
            {
                animator.SetTrigger("MoveDown");
                transform.localScale = new Vector3(1, 1, 1);
            }
            transform.position = Vector2.MoveTowards(transform.position, waypoints.waypoints[waypointIndex].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoints.waypoints[waypointIndex].position) < 0.1f)
            {
                waypointIndex++;
                if (waypointIndex >= waypoints.waypoints.Length)
                {
                    GameObject stone = GameObject.FindGameObjectWithTag("Stone");
                    if (stone != null)
                    {
                        WinLoseManager winLoseManager = stone.GetComponent<WinLoseManager>();
                        if (winLoseManager != null)
                        {
                            winLoseManager.TakeDamage(10f);
                        }
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    protected override void CheckForAttack()
    {
        base.CheckForAttack(); // Gọi phương thức của lớp cha

        // Bạn có thể thêm logic kiểm tra tấn công riêng cho Boss nếu cần
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
