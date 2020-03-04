using UnityEngine;

public class Walls : MonoBehaviour
{
    private LevelController controller;

    public Transform[] walls, pairs;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("LevelScroller").GetComponent<LevelController>();
        pairs = new Transform[walls.Length];
        pairs[1] = walls[0];
        pairs[0] = walls[1];
        pairs[3] = walls[2];
        pairs[2] = walls[3];
    }

    private void Update()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            MoveWall(i);
        }
    }

    private void MoveWall(int _index)
    {
        if (walls[_index].position.y > controller.minYPosition)
        {
            walls[_index].position = new Vector2(walls[_index].position.x, walls[_index].position.y - (controller.scrollSpeed * Time.deltaTime));
        }
        else
        {
            walls[_index].position = new Vector2(walls[_index].position.x, pairs[_index].position.y-controller.minYPosition);
        }
    }
}
