using System;
using TMPro;
using UnityEngine;

public class EndGameScreen : Window
{
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound;

    public event Action OnButtonClicked;

    private void OnEnable()
    {
        ActionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        ActionButton.onClick.RemoveListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        _audioSource.PlayOneShot(_clickSound);

        OnButtonClicked?.Invoke();
    }
}
