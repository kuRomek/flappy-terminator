using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void OnEnable()
    {
        _scoreCounter.OnScoreChanged += UpdateView;
    }

    private void OnDisable()
    {
        _scoreCounter.OnScoreChanged -= UpdateView;
    }

    private void UpdateView()
    {
        _currentScore.text = _scoreCounter.Score.ToString();
    }

    public void ShowBestScore()
    {
        _bestScore.text = $"Best score: {_scoreCounter.BestScore}";
        _bestScore.enabled = true;
    }

    public void HideBestScore()
    {
        _bestScore.enabled = false;
    }

    public void Reset()
    {
        _scoreCounter.Reset();
    }
}
