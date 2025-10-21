using System;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    [SerializeField] private Slider brushSize;
    [SerializeField] private GridInst grid;
    [SerializeField] private GameStep gameStep;
    private int gameState;
    private bool erasing;

    void Start()
    {

    }

    void Update()
    {

    }

    public void DrawFirst(Vector3 mousePos)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        int mouseX = (int)Math.Round(mousePosition.x);
        int mouseY = (int)Math.Round(mousePosition.y);
        for (int i = mouseX - (int)brushSize.value; i <= mouseX + (int)brushSize.value; ++i)
            for (int j = mouseY - (int)brushSize.value + Math.Abs(i - mouseX); j <= mouseY + (int)brushSize.value - Math.Abs(i - mouseX); ++j)
                if (i >= 0 && j >= 0 && i < grid.x && j < grid.y)
                {
                    if (i > grid.x / 2 - 1 && gameState == 0 && !grid.isSingle)
                        continue;
                    grid.grid[i][j].GetComponent<Renderer>().material.color = erasing ? grid.emptyColor : grid.live1Color;
                }
        (int, int) count = grid.CountCells();
        gameStep.UpdateScore(count.Item1, count.Item2, false);
    }

    public void DrawSecond(Vector3 mousePos)
    {
        if (grid.isSingle)
        {
            DrawFirst(mousePos);
            return;
        }
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        int mouseX = (int)Math.Round(mousePosition.x);
        int mouseY = (int)Math.Round(mousePosition.y);
        for (int i = mouseX - (int)brushSize.value; i <= mouseX + (int)brushSize.value; ++i)
            for (int j = mouseY - (int)brushSize.value + Math.Abs(i - mouseX); j <= mouseY + (int)brushSize.value - Math.Abs(i - mouseX); ++j)
                if (i >= 0 && j >= 0 && i < grid.x && j < grid.y)
                {
                    if (i < grid.x / 2 && gameState == 0)
                        continue;
                    grid.grid[i][j].GetComponent<Renderer>().material.color = erasing ? grid.emptyColor : grid.live2Color;
                }
        (int, int) count = grid.CountCells();
        gameStep.UpdateScore(count.Item1, count.Item2, false);
    }

    public bool UpdateGameState()
    {
        gameState = 1;
        return true;
    }

    public void ResetGameState()
    {
        gameState = 0;
    }

    public void ToggleErasing()
    {
        erasing = !erasing;
    }
}
