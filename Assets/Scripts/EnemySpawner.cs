using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnEnemySpawn : UnityEvent<Enemy> { }

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyIdentification
    {
        public string label = "";
        public Enemy enemyPrefab = null;
    }


    [SerializeField]
    private List<EnemyIdentification> enemyIdentification = new List<EnemyIdentification>();
    [SerializeField]
    private List<Transform> possibleSpawns = new List<Transform>();
    [SerializeField]
    private float spawnCooldown = 1f;

    private List<Enemy> enemySpawnQueue = new List<Enemy>();

    private float spawnTimer = 0f;

    public OnEnemySpawn OnEnemySpawn = new OnEnemySpawn();

    public void QueueEnemies(List<string> labels)
    {
        if (labels.Count == 0)
        {
            Debug.LogError("Labels list is empty.");
            return;
        }

        enemySpawnQueue.Clear();

        foreach (string l in labels)
        {
            enemySpawnQueue.Add(SelectPrefab(l));
        }

        spawnTimer = spawnCooldown;
    }

    public Enemy SpawnEnemy(string label)
    {
        if (string.IsNullOrEmpty(label))
        {
            Debug.LogError("Label is empty.");
            return null;
        }

        Enemy enemyPrefab = SelectPrefab(label);
        Transform spawnTransform = GetRandomSpawnTransform();

        Enemy enemyClone = Instantiate(enemyPrefab, spawnTransform.position, spawnTransform.rotation);

        return enemyClone;
    }

    private void Update()
    {
        if(!IsQueueEmpty())
        {
            spawnTimer -= Time.deltaTime;

            if(spawnTimer <= 0f)
            {
                spawnTimer = spawnCooldown;

                Enemy enemyClone = SpawnEnemy(enemySpawnQueue[0]);

                OnEnemySpawn.Invoke(enemyClone);

                enemySpawnQueue.RemoveAt(0);
            }
        }
    }

    private bool IsQueueEmpty()
    {
        return enemySpawnQueue.Count == 0;
    }

    private Transform GetRandomSpawnTransform()
    {
        Transform randomSpawnTransform = possibleSpawns[Random.Range(0, possibleSpawns.Count)];
        return randomSpawnTransform;
    }

    private Enemy SpawnEnemy(Enemy enemyPrefab)
    {
        Transform spawnTransform = possibleSpawns[Random.Range(0, possibleSpawns.Count)];

        Enemy enemyClone = Instantiate(enemyPrefab, spawnTransform.position, spawnTransform.rotation);

        return enemyClone;
    }

    private Enemy SelectPrefab(string label)
    {
        foreach (EnemyIdentification e in enemyIdentification)
        {
            if (e.label == label)
            {
                return e.enemyPrefab;
            }
        }

        Debug.LogError("No Enemy found with that name. Name: " + label);
        return null;
    }
}
