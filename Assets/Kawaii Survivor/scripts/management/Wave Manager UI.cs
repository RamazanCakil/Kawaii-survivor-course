using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class WaveManagerUI : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI timerText;


public void updateWaveText(string waveString) => waveText.text = waveString;
public void updateTimerText(string TimerString) => timerText.text = TimerString;
}
