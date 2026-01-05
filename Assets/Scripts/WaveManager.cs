using System;
using TMPro;
using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI waveText;

    public static WaveManager Instance;

    bool waveRunning = true;
    int currentWave = 0;
    int currentWaveTime;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        StartNewWave();
        timeText.text = "30";
        waveText.text = "Wave: 1";
    }

    private void Update()
    {
        // for test
        if (Input.GetKeyDown(KeyCode.Space))
            StartNewWave();
    }

    public bool WaveRunning() => waveRunning;

    private void StartNewWave()
    {
        AudioManager.Instance.PlayBgm();//
        StopAllCoroutines();
        timeText.color = Color.white;
        currentWave++;
        waveRunning = true;
        currentWaveTime = 30;
        waveText.text = "Wave: " + currentWave;
        StartCoroutine(WaveTimer());
    }

    IEnumerator WaveTimer()
    {
        while (waveRunning)
        {
            yield return new WaitForSeconds(1f);
            currentWaveTime --;
            timeText.text = currentWaveTime.ToString();
            if (currentWaveTime <= 0)
                WaveComplete();
        }

        yield return null;
    }

    private void WaveComplete()
    {
        AudioManager.Instance.StopBgm();//
        StopAllCoroutines();
        EnemyManager.Instance.DestroyEnemies();
        waveRunning = false;
        currentWaveTime = 30;
        timeText.text = currentWaveTime.ToString();
        timeText.color = Color.red;
    }

}
