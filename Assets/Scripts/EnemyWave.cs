using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của kẻ thù
    public Transform spawnPoint; // Điểm spawn kẻ thù
    public float timeBetweenWaves; // Thời gian giữa các wave
    public float timeStartWave = 0;
    public int enemiesPerWave = 5; // Số lượng kẻ thù trong mỗi wave
    public int totalWaves = 3; // Tổng số wave muốn tạo

    private int enemiesDied = 0; // Số lượng kẻ thù đã chết
    private static int totalEnemiesSpawned = 0; // Tổng số kẻ thù đã được sinh ra từ tất cả các instance
    private int totalEnemiesToSpawn; // Tổng số kẻ thù sẽ spawn trong wave này
    public WinLoseManager winLoseManager;

    private float countdown; // Đếm ngược thời gian cho wave tiếp theo
    private int currentWave = 0; // Chỉ số wave hiện tại

    void Start()
    {
        totalEnemiesToSpawn = enemiesPerWave * totalWaves; // Tính tổng số kẻ thù sẽ spawn trong wave này
        totalEnemiesSpawned += totalEnemiesToSpawn; // Cập nhật tổng số kẻ thù spawn từ tất cả các instance
        winLoseManager = FindObjectOfType<WinLoseManager>();
        ResetWave(); // Đặt lại trạng thái wave
        StartCoroutine(SpawnWaves());

        // Đăng ký với sự kiện OnEnemyDied
        Enemy.OnEnemyDied += HandleEnemyDied;
    }

    private void HandleEnemyDied()
    {
        enemiesDied++;
        Debug.Log("Total enemies died in this wave: " + enemiesDied);

        // Kiểm tra nếu tất cả kẻ thù đã chết so với tổng số kẻ thù đã spawn từ tất cả instance.
        if (enemiesDied >= totalEnemiesSpawned)
        {
            winLoseManager.WinGame(); // Gọi hàm của bạn ở đây khi tất cả kẻ thù trong wave này đã chết.
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(timeStartWave);
        while (currentWave < totalWaves)
        {
            // Debug.Log("Preparing to spawn wave " + (currentWave + 1));
            countdown = timeBetweenWaves;

            while (countdown > 0f)
            {
                countdown -= Time.deltaTime;
                yield return null;
            }
            // Debug.Log("Starting wave " + (currentWave + 1));
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(1f); // Thời gian giữa các lần spawn.
            }

            currentWave++;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity); 
    }

    public void ResetWave()
    {
        currentWave = 0;
        enemiesDied = 0; // Đặt lại số lượng kẻ thù đã chết khi reset wave.
        countdown = timeBetweenWaves;
    }

    private void OnDestroy()
    {
        // Hủy đăng ký sự kiện khi đối tượng bị hủy.
        Enemy.OnEnemyDied -= HandleEnemyDied;
    }
}
