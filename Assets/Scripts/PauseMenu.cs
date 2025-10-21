using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] Button resumeButton;
    [SerializeField] Button quitButton;
    [SerializeField] Menu menu;
    [SerializeField] GameStep gameStep;
    [SerializeField] GridInst gridInst;
    [SerializeField] Draw draw;

    public bool isActive;


    void Start()
    {
        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);

        pauseMenuPanel.SetActive(false);
    }

    void Update()
    {

    }

    void Resume()
    {
        Hide();
    }
    
    public void Show()
    {
        isActive = true;

        pauseMenuPanel.SetActive(true);
    }

    public void Hide()
    {
        isActive = false;

        pauseMenuPanel.SetActive(false);
    }

    void Quit()
    {
        Hide();
        menu.Show();
        gameStep.StopRunning();
        gridInst.Die();
        gameStep.UpdateScore(0, 0, false);
        draw.ResetGameState();
    }
}