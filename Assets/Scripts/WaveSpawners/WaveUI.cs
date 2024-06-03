using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WaveUI : MonoBehaviour
{
    public TextMeshProUGUI waveStatus;
    public WaveSpawner waveSpawner;

    void Start()
    {
        waveSpawner = FindAnyObjectByType<WaveSpawner>();

        waveSpawner.OnTimeChange += OnTimeChange;
        waveSpawner.OnWavesChange += OnWavesChanged;
        waveSpawner.OnWavesCompleted += OnWavesCompleted;

        waveStatus.text = $"Wave 0 / {waveSpawner.waveCount}";
    }

    public void OnTimeChange(float timeLeft)
    {
        waveStatus.text = $"Wave Starts In: {timeLeft}";
    }

    public void OnWavesChanged(int wave)
    {
        waveStatus.text = $"Wave {wave} / {waveSpawner.waveCount}";
    }

    public void OnWavesCompleted()
    {
        waveStatus.text = $"Face the Boss!!";
    }

}
