using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStep : MonoBehaviour
{
    [SerializeField] private GridInst grid;
    [SerializeField] private Slider speed;
    [SerializeField] private TMP_Text text;
    private float timeCounter; // ms
    private bool gameRunning;
    private int stepInterval;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        stepInterval = 1000 - (int)speed.value;
        if (gameRunning)
        {
            timeCounter += Time.deltaTime * 1000;
            if (timeCounter > stepInterval)
            {
                (int, int) score = Step();
                UpdateScore(score.Item1, score.Item2, false);
                timeCounter = 0;
            }
        }
    }

    public void UpdateScore(int score1, int score2, bool end)
    {
        if (grid.isSingle)
            text.text = score1.ToString();
        else
            text.text = score1.ToString() + ":" + score2.ToString();

        if (end && !grid.isSingle)
            text.text += "\n" + (score1 > score2 ? "First player won!" : score1 < score2 ? "Second player won!" : "Draw");
    }

    public void ToggleRunning()
    {
        gameRunning = !gameRunning;
    }

    public void StopRunning()
    {
        gameRunning = false;
    }

    (int, int) Step()
    {
        int totalScore1 = 0;
        int totalScore2 = 0;
        int[][] newGrid = new int[grid.x][];
        Color[,] currentColors = new Color[grid.x, grid.y];

        for (int i = 0; i < grid.x; i++)
        {
            newGrid[i] = new int[grid.y];
            for (int j = 0; j < grid.y; j++)
            {
                currentColors[i, j] = grid.grid[i][j].GetComponent<Renderer>().material.color;
            }
        }

        var scores = new System.Collections.Concurrent.ConcurrentBag<(int, int)>();

        for (int i = 0; i < grid.x; i++)
        {
            int localScore1 = 0;
            int localScore2 = 0;

            for (int j = 0; j < grid.y; j++)
            {
                int count2 = GetLiveAroundCount2(i, j, currentColors);
                int count1 = GetLiveAroundCount1(i, j, currentColors);
                int count = count1 - count2;

                newGrid[i][j] = CalculateNewState(currentColors[i, j], count, count1, count2);

                if (newGrid[i][j] == 1)
                    localScore1++;
                else if (newGrid[i][j] == 2)
                    localScore2++;
            }

            scores.Add((localScore1, localScore2));
        };

        foreach (var (score1, score2) in scores)
        {
            totalScore1 += score1;
            totalScore2 += score2;
        }

        for (int i = 0; i < grid.x; i++)
        {
            for (int j = 0; j < grid.y; j++)
            {
                grid.grid[i][j].GetComponent<Renderer>().material.color =
                    newGrid[i][j] == 0 ? grid.emptyColor :
                    newGrid[i][j] == 1 ? grid.live1Color : grid.live2Color;
            }
        }

        return (totalScore1, totalScore2);
    }

    private int CalculateNewState(Color currentColor, int count, int count1, int count2)
    {
        if (currentColor == grid.live1Color)
        {
            if (count >= 0)
                return count < 2 || count > 3 ? 0 : 1;
            else
                return count == -2 || count == -3 ? 2 : 0;
        }
        else if (currentColor == grid.live2Color)
        {
            if (count <= 0)
                return count < -3 || count > -2 ? 0 : 2;
            else
                return count == 2 || count == 3 ? 1 : 0;
        }
        else
        {
            if (count == 0)
                return 0;
            else if (count1 != 0 && count2 != 0)
                return count1 > 2 || count2 > 2 ? 0 : count > 0 ? 1 : 2;
            else
                return count == 3 ? 1 : count == -3 ? 2 : 0;
        }
    }

    int GetLiveAroundCount2(int x, int y, Color[,] currentColors)
    {
        int sum = 0;
        for (int i = x - 1; i < x + 2; i++)
        {
            for (int j = y - 1; j < y + 2; j++)
            {
                if ((i != x || j != y) && i >= 0 && j >= 0 && i < grid.x && j < grid.y && currentColors[i, j] == grid.live2Color)
                {
                    sum++;
                }
            }
        }

        return sum;
    }

    int GetLiveAroundCount1(int x, int y, Color[,] currentColors)
    {
        int sum = 0;
        for (int i = x - 1; i < x + 2; i++)
        {
            for (int j = y - 1; j < y + 2; j++)
            {
                if ((i != x || j != y) && i >= 0 && j >= 0 && i < grid.x && j < grid.y && currentColors[i, j] == grid.live1Color)
                {
                    sum++;
                }
            }
        }

        return sum;
    }

    public bool EndGame()
    {
        if (grid.isSingle)
            return false;
        StopRunning();
        (int, int) score = Step();
        UpdateScore(score.Item1, score.Item2, true);

        return true;
    }
}
