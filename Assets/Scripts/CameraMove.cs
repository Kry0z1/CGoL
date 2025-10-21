using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float scale;
    [SerializeField] GridInst gridInst;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void Move(KeyCode keyCode, float deltaTime)
    {
        if (keyCode == KeyCode.RightArrow)
        {
            transform.Translate(new Vector2(speed * deltaTime, 0));
        }
        if (keyCode == KeyCode.LeftArrow)
        {
            transform.Translate(new Vector2(-speed * deltaTime, 0));
        }
        if (keyCode == KeyCode.DownArrow)
        {
            transform.Translate(new Vector2(0, -speed * deltaTime));
        }
        if (keyCode == KeyCode.UpArrow)
        {
            transform.Translate(new Vector2(0, speed * deltaTime));
        }
    }

    public void MoveToDefault()
    {
        transform.position = new Vector3(gridInst.x/2 * scale, gridInst.y/2 * scale, -10);
    }
}
