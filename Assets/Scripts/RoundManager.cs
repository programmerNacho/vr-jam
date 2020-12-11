using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    [System.Serializable]
    public class Round
    {
        public List<string> enemyOrder = new List<string>();
    }

    [SerializeField]
    private List<Round> rounds = new List<Round>();
    [SerializeField]
    private float restingTimeUntilNextRound = 5f;
    [SerializeField]
    private EnemySpawner enemySpawner = null;

    private int currentRoundIndex = 0;

    private int numberOfEnemiesToKill = 0;

    private WaitForSeconds restingWait = null;

    private void Awake()
    {
        restingWait = new WaitForSeconds(restingTimeUntilNextRound);

        InitiateCurrentRound();
    }

    private void OnEnable()
    {
        enemySpawner.OnEnemySpawn.AddListener(EnemyCreated);
    }

    private void OnDisable()
    {
        enemySpawner.OnEnemySpawn.RemoveListener(EnemyCreated);
    }

    private void InitiateCurrentRound()
    {
        numberOfEnemiesToKill = rounds[currentRoundIndex].enemyOrder.Count;
        enemySpawner.QueueEnemies(rounds[currentRoundIndex].enemyOrder);
    }

    private void EnemyCreated(Enemy enemy)
    {
        enemy.GetComponent<Health>().OnDeath.AddListener(EnemyDead);
    }

    public void EnemyDead()
    {
        numberOfEnemiesToKill -= 1;

        if(numberOfEnemiesToKill <= 0)
        {
            RoundEnded();
        }
    }

    private void RoundEnded()
    {
        if(currentRoundIndex + 1 >= rounds.Count)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            StartCoroutine(WaitAndStartNextRound());
        }
    }

    private IEnumerator WaitAndStartNextRound()
    {
        yield return restingWait;

        currentRoundIndex += 1;

        InitiateCurrentRound();
    }
}
