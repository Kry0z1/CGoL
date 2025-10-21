using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] Menu menu;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] CameraMove cameraMove;
    [SerializeField] CameraScale cameraScale;
    [SerializeField] Draw draw;
    [SerializeField] GameStep gameStep;
    [SerializeField] Timer timer;

    public bool gameRunning = false;
    public bool gameEnded = false;

    readonly KeyCode[] arrows = new KeyCode[] { KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.UpArrow };

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.isActive)
                menu.Quit();
            else if (pauseMenu.isActive)
                pauseMenu.Hide();
            else
                pauseMenu.Show();
        }
        
        if (!menu.isActive && !pauseMenu.isActive)
        {
            if (Input.GetKeyDown(KeyCode.Q) && gameRunning)
            {
                gameEnded = gameStep.EndGame();
                timer.PauseTimer();
                gameRunning = !gameEnded && gameRunning;
            }

            for (int i = 0; i < 4; i++)
                if (Input.GetKey(arrows[i]))
                    cameraMove.Move(arrows[i], Time.deltaTime);

            cameraScale.Scale(Input.GetAxis("Mouse ScrollWheel"));

            if (gameEnded)
                return;

            if (Input.GetMouseButton(0))
                draw.DrawFirst(Input.mousePosition);
            else if (Input.GetMouseButton(1))
                draw.DrawSecond(Input.mousePosition);

            if (Input.GetKeyDown(KeyCode.S))
            {
                bool drawEnded = draw.UpdateGameState();
                if (drawEnded)
                {
                    gameStep.ToggleRunning();
                    timer.ToggleTimer();
                    gameRunning = true;
                }
                    
            }
            if (Input.GetKeyDown(KeyCode.E))
                draw.ToggleErasing();
        }
    }
}
