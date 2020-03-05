using UnityEngine;

public enum Direction { CW, CCW}

public class LavaAnimation : MonoBehaviour
{
    public float RotateSpeed = 5f;
    public float Radius = 0.1f;
    public Direction direction;
    private Vector2 _centre;
    private float _angle;

    private void Start()
    {
        _centre = transform.position;
    }

    private void Update()
    {
        switch (direction)
        {
            case Direction.CW:
                _angle += RotateSpeed * Time.deltaTime;
                break;
            case Direction.CCW:
                _angle -= RotateSpeed * Time.deltaTime;
                break;
        }

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        transform.position = _centre + offset;
    }
}
