using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Camera _camera;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private ScoreView _scoreView;

    private Color _defaultBackgroundColor;
    private Color _endGameBackgroundColor = new Color(236f / 255f, 136f / 255f, 130f / 255f);

    private void Awake()
    {
        _defaultBackgroundColor = _camera.backgroundColor;
    }

    private void OnEnable()
    {
        _player.OnGameOver += EndGame;
        _startScreen.OnButtonClicked += StartGame;
        _endGameScreen.OnButtonClicked += StartGame;
    }

    private void OnDisable()
    {
        _player.OnGameOver -= EndGame;
        _startScreen.OnButtonClicked -= StartGame;
        _endGameScreen.OnButtonClicked -= StartGame;
    }

    private void Start()
    {
        Time.timeScale = 0f;
        _startScreen.Open();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        _camera.backgroundColor = _endGameBackgroundColor;
        _endGameScreen.Open();
        _scoreView.ShowBestScore();
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        _camera.backgroundColor = _defaultBackgroundColor;
        _startScreen.Close();
        _endGameScreen.Close();
        _scoreView.HideBestScore();

        _player.Reset();
        _enemySpawner.Reset();
    }
}
