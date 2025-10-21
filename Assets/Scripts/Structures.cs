using UnityEngine;

public class Structures : MonoBehaviour
{

    public static int[,] Glider = new int[,] { { -1, -1 }, { 0, 0 }, { 1, 0 }, { -1, 1 }, { 0, 1 } };
    public static int[,] LWSS = new int[,] { { -2, -1 }, { -1, -2 }, { 0, -2 }, { 1, -2 }, { 2, -2 }, { 2, -1 }, { 2, 0 }, { 1, 1 }, { -2, 1 } };
    public static int[,] MWSS = new int[,] { { -2, -1 }, { 0, -2 }, { 2, -1 }, { 3, 0 }, { 3, 1 }, { 3, 2 }, { 2, 2 }, { 1, 2 }, { 0, 2 }, { -1, 2 }, { -2, 1 } };
    public static int[,] HWSS = new int[,] { { -3, -1 }, { -1, -2 }, { 0, -2 }, { 2, -1 }, { 3, 0 }, { 3, 1 }, { 3, 2 }, { 2, 2 }, { 1, 2 }, { 0, 2 }, { -1, 2 }, { -2, 2 }, { -3, 1 } };

    void Start()
    { }
    void Update()
    { }
}
