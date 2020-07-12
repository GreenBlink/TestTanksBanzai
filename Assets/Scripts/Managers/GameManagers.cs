using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team { Enemy, Player }

public class GameManagers : MonoBehaviour
{
    private List<Enemy> enemiesToBattle = new List<Enemy>();
    private bool isInfinite;

    public static GameManagers instance;

    public List<Enemy> enemyPrefabs;
    public List<Transform> startPositionEnemies;
    public BattleUIController battleUIController;
    public Player player;
    public float timeToRespawnEnemy = 1f;
    public int countEnemyToScene;
    public int countEnemyAll;
    public bool friendlyFire;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        battleUIController.Init(player);
        StartCoroutine(RespawnEnemyController());
    }

    public void Win()
    {
        StopAllCoroutines();
    }

    public void Lose()
    {
        StopAllCoroutines();
    }

    public void DestroyEnemy(Enemy enemy)
    {
        if (!isInfinite)
            countEnemyAll--;

        enemiesToBattle.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void InitEnemy(Enemy enemy)
    {
        Transform startPosition = startPositionEnemies[Random.Range(0, startPositionEnemies.Count)];
        Enemy temp = Instantiate(enemy);
        temp.transformEnemy.position = startPosition.position;
        temp.transformEnemy.rotation = startPosition.rotation;

        enemiesToBattle.Add(temp);
    }

    private IEnumerator RespawnEnemyController()
    {
        WaitForSeconds wait = new WaitForSeconds(timeToRespawnEnemy);
        isInfinite = countEnemyAll == 0;

        while(countEnemyAll != 0 || isInfinite)
        {
            if (enemiesToBattle.Count < countEnemyToScene)
            {
                InitEnemy(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
            }

            yield return wait;
        }

        Win();
    }
}
