using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    public float health = 100f; // Thanh máu của đồng minh
    public float attackDamage;
    public bool IsOnTower = false; // Biến để xác định xem đồng minh có ở trên tower không
    public float attackRange = 1.5f; // Khoảng cách tấn công
    public float attackSpeed = 1f; // Thời gian hồi chiêu giữa các lần tấn công
    public GameObject attackEffectPrefab; // Prefab của hiệu ứng tấn công
    public GameObject bulletPrefab; // Prefab của viên đạn (đã đổi tên thành BulletAlly)

    private GameObject targetEnemy; // Tham chiếu đến kẻ thù mục tiêu
    private float lastAttackTime = 0f; // Thời gian lần tấn công cuối
    private Animator animator; // Tham chiếu đến Animator

    void Start()
    {
        animator = GetComponent<Animator>(); // Lấy component Animator
    }

    public void TakeDamage(float damage)
    {
        health -= damage; // Giảm máu
        // Debug.Log("Ally takes damage: " + damage + ". Current health: " + health);
        
        if (health <= 0)
        {
            Die(); // Gọi phương thức chết nếu máu <= 0
        }
    }

    void Die()
    {
        Debug.Log("Ally died!");
        Destroy(gameObject); // Xóa đồng minh khỏi cảnh
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            LookAtEnemy();
            AttackEnemy();

            // Kiểm tra nếu kẻ thù ra khỏi tầm tấn công
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) > attackRange)
            {
                targetEnemy = null; // Đặt lại targetEnemy để tìm kiếm kẻ thù mới
            }
        }
        else
        {
            GameObject closestEnemy = FindClosestEnemy();
            if (closestEnemy != null && Vector3.Distance(transform.position, closestEnemy.transform.position) <= attackRange)
            {
                SetTargetEnemy(closestEnemy); // Thiết lập kẻ thù mục tiêu nếu trong tầm tấn công
            }
        }
    }

    void LookAtEnemy()
    {
        Vector3 direction = targetEnemy.transform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Đổi từ radian sang độ 

        if (angle >= -45 && angle < 45)
        {
            animator.SetTrigger("AttackRight");
        }
        else if (angle >= 45 && angle < 135)
        {
            animator.SetTrigger("AttackUp");
        }
        else if (angle >= 135 || angle < -135)
        {
            animator.SetTrigger("AttackLeft");
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (angle >= -135 && angle < -45)
        {
            animator.SetTrigger("AttackDown");
        }
    }

    void AttackEnemy()
    {
        if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= attackRange && Time.time >= lastAttackTime + 1f/attackSpeed) 
        {
            lastAttackTime = Time.time;

            if (bulletPrefab != null) // Kiểm tra xem có prefab viên đạn không
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // Tạo viên đạn tại vị trí đồng minh
                
                BulletAlly bulletScript = bullet.GetComponent<BulletAlly>();
                if (bulletScript != null)
                {
                    bulletScript.Initialize(targetEnemy.transform); // Khởi tạo viên đạn với tham chiếu đến kẻ thù
                }
            }

            if (attackEffectPrefab != null)
            {
                ShowAttackEffect(targetEnemy.transform.position); // Hiện hiệu ứng tấn công nếu cần thiết
            }            
            Enemy enemy= targetEnemy.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage); // Giảm 10 máu cho enemy 
            }
        }
    }


    public void SetTargetEnemy(GameObject enemy)
    {
        targetEnemy = enemy; 
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance) 
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy; // Trả về kẻ thù gần nhất nếu có
    }

    private void ShowAttackEffect(Vector3 targetPosition)
    {
        GameObject effect = Instantiate(attackEffectPrefab, targetPosition, Quaternion.identity); // Tạo hiệu ứng tấn công tại vị trí kẻ thù

        Animator effectAnimator = effect.GetComponent<Animator>();
        if (effectAnimator != null)
        {
            AnimationClip[] clips = effectAnimator.runtimeAnimatorController.animationClips;
            if (clips.Length > 0)
            {
                StartCoroutine(MoveAndDestroyEffect(effect, clips[0].length));
            }
        }
    }

    private IEnumerator MoveAndDestroyEffect(GameObject effect, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (targetEnemy != null)
            {
                effect.transform.position = targetEnemy.transform.position; 
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(effect); 
    }
}
