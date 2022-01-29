using System.Collections;
using UnityEngine;

public class Wander : ISteering
{
    private Transform _transform;
    private Vector3 steering;
    private Vector3 _velocity;
    private float wanderAngle;
    public float _maxForce;
    public float _changeDir;
    public float count;
    public Wander(Transform self, float changeDir, float maxForce)
    {
        _transform = self;
        _changeDir = changeDir;
        _maxForce = maxForce;
        _velocity = new Vector3(1, -0.23f, 1);
    }

    public Transform SetTarget
    {

        set
        {
            _velocity = new Vector3(1, -0.23f, 1);
            
            _transform = value;
        }

    }

    public Vector3 GetDir()
    {
        if (count == _changeDir)
        {
            steering = Wanderer();
            count = 0;

        }
        else
        {
            count++;
        }
        steering = Vector3.ClampMagnitude(steering, _maxForce);

        _velocity = Vector3.ClampMagnitude(_velocity + steering, _maxForce);
        _velocity.y = 0;
        Debug.Log(_velocity);
        return _velocity;

    }
    public Vector3 Wanderer()
    {
        // Calculate the circle center
        Vector3 circleCenter;
        Vector3 circleDir;
        Vector3 displacement;
        Vector3 wanderForce;
        Vector3 distance;

        circleCenter = _velocity;

        circleDir = Vector3.Normalize(circleCenter);
        distance = circleCenter - _transform.position;
        circleCenter = circleDir * distance.magnitude;
        // Calculate the displacement force

        displacement = new Vector3(0, -1);
        displacement = displacement * circleCenter.magnitude;
        //
        // Randomly change the vector direction
        // by making it change its current angle
        displacement = SetAngle(displacement, wanderAngle);
        //
        // Change wanderAngle just a bit, so it
        // won't have the same value in the
        // next game frame.
        float angle = Vector3.Angle(displacement, circleCenter);
        wanderAngle = (Random.Range(0, 7) * angle) - (angle * 0.8f);
        //
        // Finally calculate and return the wander force
        wanderForce = circleCenter + displacement;
      

        return wanderForce;
    }

    public Vector3 SetAngle(Vector3 displacement, float wanderAngle)
    {
        float magnitud = displacement.magnitude;
        displacement.x = Mathf.Cos(wanderAngle) * magnitud;
        displacement.z = Mathf.Sin(wanderAngle) * magnitud;


        return displacement;
    }
}
