using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;

    public int Score { get; private set; }
    public int BestScore { get; private set; }

    public event Action OnScoreChanged;

    private void OnEnable()
    {
        _enemySpawner.OnEnemyDied += GetPoints;
    }

    private void OnDisable()
    {
        _enemySpawner.OnEnemyDied -= GetPoints;
    }

    public void GetPoints(int points)
    {
        Score += points;
        BestScore = Score > BestScore ? Score : BestScore;

        OnScoreChanged?.Invoke();
    }

    public void Reset()
    {
        Score = 0;
        OnScoreChanged?.Invoke();
    }
}
