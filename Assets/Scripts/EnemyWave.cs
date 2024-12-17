using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của kẻ thù
    public Transform spawnPoint; // Điểm spawn kẻ thù
    public float timeBetweenWaves = 5f; // Thời gian giữa các wave
    public int enemiesPerWave = 5; // Số lượng kẻ thù trong mỗi wave
    public int totalWaves = 3; // Tổng số wave muốn tạo

    private float countdown; // Đếm ngược thời gian cho wave tiếp theo
    private int currentWave = 0; // Chỉ số wave hiện tại

    void Start()
    {
        ResetWave(); // Đặt lại trạng thái wave
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        // yield return new WaitForSeconds(10); // Thời gian trước khi bắt đầu spawn 
        while (currentWave < totalWaves)
        {
            Debug.Log("Preparing to spawn wave " + (currentWave + 1));
            countdown = timeBetweenWaves; // Đặt lại thời gian đếm ngược

            while (countdown > 0f)
            {
                countdown -= Time.deltaTime; // Giảm thời gian đếm ngược
                // Debug.Log("Countdown: " + countdown); // Thêm log để kiểm tra countdown
                yield return null; // Chờ đến frame tiếp theo
            }

            Debug.Log("Starting wave " + (currentWave + 1));
            
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                Debug.Log("Spawned enemy " + (i + 1) + " in wave " + (currentWave + 1));
                yield return new WaitForSeconds(1f); // Thời gian giữa các kẻ thù được spawn
            }

            currentWave++; // Tăng chỉ số wave sau khi spawn xong
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity); // Spawn kẻ thù tại điểm đã chọn
    }

    public void ResetWave()
    {
        currentWave = 0;
        Time.timeScale = 1;
        countdown = timeBetweenWaves; // Đặt lại countdown
    }
}
