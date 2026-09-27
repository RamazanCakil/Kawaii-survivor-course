using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


[RequireComponent(typeof(WaveManagerUI))]
public class waveManager : MonoBehaviour,IGameStateListener
{
    [Header("References")]
    [SerializeField] private Player player;
    private WaveManagerUI UI;

    [Header("Settings")]
    [SerializeField] private float waveDuration;
    private float timer;
    private bool isTimerOn;
    private int currentWaveIndex;

    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    private List<float> localCounters = new List<float>();


    private void Awake()
    {
        UI= GetComponent<WaveManagerUI>();
    }
    void Start()
    {

    }

    void Update()
    {
        if (!isTimerOn)
            return;


        if (timer < waveDuration)
        {
            ManageCurrentWave();

            string timerString = ((int)(waveDuration - timer)).ToString();
            UI.updateTimerText(timerString);

        }
        else
        {
            startWaveTransition();
        }
    }
    private void startWaveTransition() 
    {
        isTimerOn = false;
        DefeatAllEnemies();
        currentWaveIndex++;

        if (currentWaveIndex >= waves.Length)
        {
            UI.updateTimerText("");
            UI.updateWaveText("Stage Complated");

            GameManager.instance.setGameState(GameState.STAGECOMPLATE);

        }

        else
        {
            GameManager.instance.WaveComplateCallback();
        }
    }

    private void DefeatAllEnemies() 
    {
        foreach (Enemy enemy in transform.GetComponentsInChildren<Enemy>())
            enemy.PassAwayAfterWave();
    }

    private void startWave(int waveIndex) 
    {
        UI.updateWaveText("wave " + (currentWaveIndex+1) + " / " + waves.Length);

        localCounters.Clear();
        foreach(WaveSegment segment in waves[waveIndex].segments)
            localCounters.Add(1);

        timer = 0;
        isTimerOn = true;
    }



    private void StartNextWave()
    {
        startWave(currentWaveIndex);

    }



    public void ManageCurrentWave()
    {
        Wave currentWave = waves[currentWaveIndex];

        for (int i = 0; i < currentWave.segments.Count; i++)
        {
            WaveSegment segment = currentWave.segments[i];

            float tStart = segment.tStartEnd.x / 100 * waveDuration;
            float tEnd = segment.tStartEnd.y / 100 * waveDuration;

            if (timer < tStart || timer > tEnd)
                continue;

            float timeSinceSegmentStart = timer - tStart;
            float spawnDelay = 1f / segment.spawnFrequency;

            if (timeSinceSegmentStart / spawnDelay > localCounters[i])
            {
                // Artık düşmanları 0,0 noktasında değil, GetSpawnPosition'dan gelen rastgele konumda üretiyoruz
                Instantiate(segment.prefab, GetSpawnPosition(), Quaternion.identity, transform);
                localCounters[i]++;
            }
        }

        timer += Time.deltaTime;
    }

    private Vector2 GetSpawnPosition()
    {
        // 2. DÜZELTME: 3D Küre (onUnitSphere) yerine 2D Çember (insideUnitCircle) kullandık.
        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector2 offset = direction * Random.Range(6f, 10f);

        // Eğer bu satırda hata alırsan, Unity'de Inspector'dan 'Player' kutusunu doldurmadın demektir!
        Vector2 targetPosition = (Vector2)player.transform.position + offset;

        // Düşmanların ekranın çok dışına veya istenmeyen yerlere çıkmasını engelliyoruz
        targetPosition.x = Mathf.Clamp(targetPosition.x, -10f, 18f);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -8f, 8f);

        return targetPosition;
    }

    public void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:
                StartNextWave(); break;

            case GameState.GAMEOVER:
                isTimerOn = false;
                DefeatAllEnemies();
                break;

        }
    }

}

[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> segments;
}

[System.Serializable]
public struct WaveSegment
{
    [MinMaxSlider(0, 100)] public Vector2 tStartEnd;
    public float spawnFrequency;
    public GameObject prefab;
}