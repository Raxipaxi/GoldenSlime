using System.Collections;
using UnityEngine;

public class Wander : ISteering
{
    private Transform _transform;
    private Vector3 wayPoint;
    private float amplitude = 5f;

    public Wander(Transform self)
    {
        _transform = self;
    }

    public Transform SetTarget
    {
        set
        {
            _transform = value;
        }
    }

    public Vector3 GetDir()
    {
        Vector2 randomPos = new Vector2(Random.Range(-amplitude, amplitude), Random.Range(-amplitude, amplitude));
        wayPoint = new Vector3(randomPos.x, 0, randomPos.y) + _transform.position;
        return wayPoint;
    }
}
