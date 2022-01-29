using System.Collections;
using UnityEngine;

public class Wander : ISteering
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private Transform _transform;
    private Vector3 wayPoint;
    private float amplitude = 5f;
    private float rotationSpeed = 2f;

    public Wander(Transform self,float moveSpeed,float rotSpeed)
    {

        _speed = moveSpeed;
        _transform = self;
    }
    public void Wanderer()
    {
        Vector2 randomPos = new Vector2(Random.Range(-amplitude,amplitude),Random.Range(-amplitude, amplitude));
        wayPoint = new Vector3(randomPos.x, 0, randomPos.y) + _transform.position;
    }

    public Vector3 GetDir()
    {
       return wayPoint;
    }
}
