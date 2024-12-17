using UnityEngine;

public class BulletAlly : MonoBehaviour // Đổi tên lớp từ Bullet thành BulletAlly
{
    public float speed = 10f; // Tốc độ di chuyển của viên đạn
    private Transform targetEnemy; // Tham chiếu đến kẻ thù mà viên đạn sẽ theo dõi

    public void Initialize(Transform enemy)
    {
        targetEnemy = enemy; // Thiết lập kẻ thù làm mục tiêu
    }

    private void Update()
    {
        if (targetEnemy != null)
        {
            // Tính toán hướng di chuyển về phía kẻ thù
            Vector3 direction = (targetEnemy.position - transform.position).normalized; // Tính toán hướng
            transform.position += direction * speed * Time.deltaTime; // Cập nhật vị trí của viên đạn

            // Kiểm tra nếu viên đạn đã đến gần kẻ thù
            if (Vector3.Distance(transform.position, targetEnemy.position) < 0.1f)
            {
                Destroy(gameObject); // Hủy viên đạn khi đến gần kẻ thù
            }
        }
        else
        {
            Destroy(gameObject); // Hủy viên đạn nếu không còn mục tiêu
        }
    }
}
