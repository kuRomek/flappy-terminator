using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int Score { get; private set; }
    public int BestScore { get; private set; }

    public event Action OnScoreChanged;

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
