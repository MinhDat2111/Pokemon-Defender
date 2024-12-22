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

    protected Waypoints waypoints; // Tham chiếu đến đối tượng Waypoints
    protected int waypointIndex; // Chỉ số waypoint hiện tại
    protected float lastAttackTime = 0f; // Thời gian của lần tấn công cuối cùng
    protected GameObject targetAlly; // Đối tượng đồng minh hiện tại mà kẻ thù đang tấn công
    protected Animator animator;

    public delegate void EnemyDiedHandler();
    public static event EnemyDiedHandler OnEnemyDied;

    protected virtual void Start()
    {
        waypoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveTowardsWaypoint();
        CheckForAttack(); // Kiểm tra xem có thể tấn công không
    }

    protected virtual void MoveTowardsWaypoint()
    {
        if (waypointIndex < waypoints.waypoints.Length && targetAlly == null)
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
                    // Logic khi đến waypoint cuối cùng, ví dụ như gây sát thương lên viên đá trấn giữ
                    GameObject stone = GameObject.FindGameObjectWithTag("Stone");
                    if (stone != null)
                    {
                        WinLoseManager winLoseManager = stone.GetComponent<WinLoseManager>();
                        if (winLoseManager != null)
                        {
                            winLoseManager.TakeDamage(1f);
                        }
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    protected virtual void CheckForAttack()
    {
        if (targetAlly != null)
        {
            float distance = Vector2.Distance(transform.position, targetAlly.transform.position);
            if (distance <= attackRange && Time.time >= lastAttackTime + attackSpeed)
            {
                Attack(targetAlly.transform);
                lastAttackTime = Time.time;
            }
        }

        // Logic tìm kiếm đồng minh để tấn công giống như trong lớp Enemy gốc
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject ally in allies)
        {
            Ally allyComponent = ally.GetComponent<Ally>();
            if (allyComponent != null && !allyComponent.IsOnTower) 
            {
                float distance = Vector2.Distance(transform.position, ally.transform.position);
                if (distance <= attackRange && Time.time >= lastAttackTime + attackSpeed)
                {
                    targetAlly = ally;
                    Attack(targetAlly.transform);
                    lastAttackTime = Time.time;
                    return;
                }
            }
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        // Debug.Log("Enemy takes damage: " + damage + ". Current health: " + health);
        
        if (health <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Debug.Log("Enemy died!");
        OnEnemyDied?.Invoke(); // Gọi sự kiện khi kẻ thù chết
        Destroy(gameObject);
    }

    protected virtual void Attack(Transform target)
    {  
        Ally ally = target.GetComponent<Ally>();
        if (ally != null)
        {
            ally.TakeDamage(attackDamage); // Gây sát thương lên đồng minh nếu cần thiết
            // Debug.Log("Enemy dealt " + attackDamage + " damage to " + ally.name);
        }
    }
}
