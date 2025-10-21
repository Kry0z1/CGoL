using UnityEngine;
using UnityEngine.UI;

public class GridInst : MonoBehaviour
{
    [SerializeField] private GameObject gridPrefab;
    public Color emptyColor;
    public Color live1Color;
    public Color live2Color;
    public int x;
    public int y;
    [SerializeField] private float probabilityAlive;
    public GameObject[][] grid;
    public bool isSingle;
    [SerializeField] Timer timer;
    [SerializeField] Slider xSlider;
    [SerializeField] Slider ySlider;
    [SerializeField] Slider probSlider;

    void Start()
    {
        
    }

    void Update()
    {

    }

    void CommonInst()
    {
        x = (int)xSlider.value;
        y = (int)ySlider.value;
        probabilityAlive = probSlider.value;
        x = x / 2 * 2;

        grid = new GameObject[x][];
        for (int i = 0; i < x; i++)
        {
            grid[i] = new GameObject[y];
            for (int j = 0; j < y; j++)
            {
                GameObject obj = Instantiate(gridPrefab);
                obj.transform.position = new Vector2(i, j);
                grid[i][j] = obj;
            }
        }
    }

    public void InstSingle()
    {
        CommonInst();
        isSingle = true;
        timer.Disable();
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                grid[i][j].GetComponent<Renderer>().material.color = Random.value < probabilityAlive ? live1Color : emptyColor;
            }
        }
    }

    public void InstMulti()
    {
        CommonInst();
        isSingle = false;
        timer.Enable();
        for (int i = 0; i < x / 2; i++)
        {
            for (int j = 0; j < y; j++)
            {
                grid[i][j].GetComponent<Renderer>().material.color = Random.value < probabilityAlive ? live1Color : emptyColor;
            }
        }

        for (int i = x / 2; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                grid[i][j].GetComponent<Renderer>().material.color = Random.value < probabilityAlive ? live2Color : emptyColor;
            }
        }
    }

    public (int, int) CountCells()
    {
        int score1 = 0;
        int score2 = 0;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (grid[i][j].GetComponent<Renderer>().material.color == live1Color)
                    score1++;
                else if (grid[i][j].GetComponent<Renderer>().material.color == live2Color)
                    score2++;
            }
        }

        return (score1, score2);
    }

    public void Die()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Destroy(grid[i][j]);
            }
        }
    }
}
