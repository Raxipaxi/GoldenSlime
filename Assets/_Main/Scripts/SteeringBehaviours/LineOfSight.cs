using System;
using System.Collections.Generic;
using UnityEngine;
public class LineOfSight : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private LineOfSightStats _stats;
    public bool CanSeeSomeone(Transform target)
    {
        Vector3 diff = target.position - origin.position;
        float distance = diff.magnitude;

        if(distance >= _stats.Range) { return false; }

        Vector3 front = origin.forward;

        if (!IsInVisionAngle(diff, front)) { return false; }

        if (!isInView(diff.normalized, distance, _stats.ObstaclesMask)) { return false; }

        return true;
    }
    public bool isInView(Vector3 normalizedDir, float dist, LayerMask obstacleLayer)
    {
        return Physics.Raycast(origin.position, normalizedDir, dist, obstacleLayer);
    }

    public bool IsInVisionAngle(Vector3 origin, Vector3 target)
    {
        float angleToTarget = Vector3.Angle(origin, target);
        return angleToTarget < (_stats.Angle / 2);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(origin.position, _stats.Range);
    }
}

