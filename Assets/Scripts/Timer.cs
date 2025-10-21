using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    
    private float currentTime;
    private bool isRunning = false;
    [SerializeField] private GameStep gameStep;
    [SerializeField] private Slider startTimeSlider;
    
    void Start()
    {
    }
    
    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                TimerFinished();
            }
            UpdateTimerDisplay();
        }
    }
    
    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            
            if (currentTime <= 10f)
            {
                timerText.color = Color.red;
            }
        }
    }

    void TimerFinished()
    {
        isRunning = false;
        gameStep.EndGame();
    }

    public void ToggleTimer()
    {
        if (enabled)
            isRunning = !isRunning;
    }
    
    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        timerText.color = Color.white;
        currentTime = startTimeSlider.value;
        isRunning = false;
        UpdateTimerDisplay();
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        isRunning = false;
        enabled = false;
        timerText.text = "";
    }
}