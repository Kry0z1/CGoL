using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] Button singleplayerButton;
    [SerializeField] Button multiplayerButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button controlsButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button settingsQuitButton;
    [SerializeField] GridInst gridInst;
    [SerializeField] CameraMove cameraMove;
    [SerializeField] CameraScale cameraScale;
    [SerializeField] InputHandler inputHandler;
    [SerializeField] Timer timer;
    [SerializeField] GameObject grid;

    public bool isActive;


    void Start()
    {
        Application.targetFrameRate = 30;
        
        singleplayerButton.onClick.AddListener(SinglePlay);
        multiplayerButton.onClick.AddListener(MultiPlay);
        settingsButton.onClick.AddListener(ShowSettings);
        quitButton.onClick.AddListener(Quit);
        settingsQuitButton.onClick.AddListener(Quit);
        controlsButton.onClick.AddListener(OpenLink);

        settingsPanel.SetActive(false);

        isActive = true;
        grid.SetActive(false);
    }

    void Update()
    {
    }

    void OpenLink()
    {
        Application.OpenURL("https://google.com");
    }
    
    void ShowSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void Show()
    {
        isActive = true;
        mainMenuPanel.SetActive(true);
    }

    public void Hide()
    {
        isActive = false;
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        timer.ResetTimer();
        grid.SetActive(true);
    }

    public void SinglePlay()
    {
        gridInst.InstSingle();
        cameraMove.MoveToDefault();
        cameraScale.SetScale(15);
        inputHandler.gameEnded = false;
        inputHandler.gameRunning = false;
        Hide();
    }

    public void MultiPlay()
    {
        gridInst.InstMulti();
        cameraMove.MoveToDefault();
        cameraScale.SetScale(15);
        inputHandler.gameEnded = false;
        inputHandler.gameRunning = false;
        Hide();
    }

    public void Quit()
    {
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            return;
        }
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}