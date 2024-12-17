using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; // Tốc độ di chuyển
    public float health = 100f; // Thanh máu
    public float attackDamage = 10f; // Sát thương tấn công
    public float attackRange = 1f; // Khoảng cách tấn công
    public float attackSpeed = 1f; // Thời gian giãn cách giữa các đòn đánh

    private Waypoints waypoints; // Tham chiếu đến đối tượng Waypoints
    private int waypointIndex; // Chỉ số waypoint hiện tại
    private float lastAttackTime = 0f; // Thời gian của lần tấn công cuối cùng
    private GameObject targetAlly; // Đối tượng đồng minh hiện tại mà kẻ thù đang tấn công
    private Animator animator;

    void Start()
    {
        waypoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveTowardsWaypoint();
        CheckForAttack(); // Kiểm tra xem có thể tấn công không
    }

    void MoveTowardsWaypoint()
    {
        
        if (waypointIndex < waypoints.waypoints.Length && targetAlly == null) // Không di chuyển khi đang tấn công
        {
            Vector3 direction = waypoints.waypoints[waypointIndex].position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Đổi từ radian sang độ 
            if (angle >= -45 && angle < 45)
            {
                animator.SetTrigger("MoveRight");
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (angle >= 45 && angle < 135)
            {
                animator.SetTrigger("MoveUp");
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (angle >= 135 || angle < -135)
            {
                animator.SetTrigger("MoveLeft");
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (angle >= -135 && angle <-45)
            {
                animator.SetTrigger("MoveDown");
                transform.localScale = new Vector3(1, 1, 1);
            }
            transform.position = Vector2.MoveTowards(transform.position, waypoints.waypoints[waypointIndex].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, waypoints.waypoints[waypointIndex].position) < 0.1f)
            {
                if (waypointIndex < waypoints.waypoints.Length - 1)
                {
                    waypointIndex++;
                }
                else 
                {
                    // Gây sát thương lên viên đá trấn giữ trước khi xóa kẻ thù
                    GameObject stone = GameObject.FindGameObjectWithTag("Stone"); // Giả sử viên đá có tag là "Stone"
                    if (stone != null)
                    {
                        WinLoseManager winLoseManager = stone.GetComponent<WinLoseManager>();
                        if (winLoseManager != null)
                        {
                            winLoseManager.TakeDamage(1f); // Gây 1 sát thương lên viên đá
                        }
                    }

                    Destroy(gameObject); // Xóa kẻ thù nếu đã đến waypoint cuối cùng
                }
            }
        }
    }

    void CheckForAttack()
    {
        if (targetAlly != null)
        {
            Ally ally = targetAlly.GetComponent<Ally>();
            if (ally == null || ally.health <= 0 || ally.IsOnTower) // Không tấn công nếu đồng minh chết hoặc ở trên tower
            {
                targetAlly = null; // Nếu đồng minh chết hoặc ở trên tower, không còn mục tiêu
                return;
            }
            // Kiểm tra khoảng cách để tấn công
            float distance = Vector2.Distance(transform.position, targetAlly.transform.position);
            if (distance <= attackRange && Time.time >= lastAttackTime + attackSpeed)
            {
                Attack(targetAlly.transform); // Gọi phương thức tấn công
                lastAttackTime = Time.time; // Cập nhật thời gian tấn công cuối cùng
                return; // Dừng lại nếu đã tấn công
            }
        }

        // Nếu không có mục tiêu, tìm kiếm đồng minh mới để tấn công
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject ally in allies)
        {
            Ally allyComponent = ally.GetComponent<Ally>();
            if (allyComponent != null && !allyComponent.IsOnTower) // Chỉ tìm kiếm đồng minh không ở trên tower
            {
                float distance = Vector2.Distance(transform.position, ally.transform.position);
                if (distance <= attackRange && Time.time >= lastAttackTime + attackSpeed)
                {
                    targetAlly = ally; // Lưu trữ mục tiêu mới để tấn công
                    Attack(targetAlly.transform); // Gọi phương thức tấn công ngay lập tức
                    lastAttackTime = Time.time; // Cập nhật thời gian tấn công cuối cùng
                    return; // Dừng vòng lặp nếu đã tìm thấy đồng minh để tấn công
                }
            }
        }
    }

    void Attack(Transform target)
    {
        // Debug.Log("Enemy attacks! Damage dealt: " + attackDamage);
        
        Ally ally = target.GetComponent<Ally>(); 
        if (ally != null)
        {
            ally.TakeDamage(attackDamage); 
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage; 
        if (health <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}