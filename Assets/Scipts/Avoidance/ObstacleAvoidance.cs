using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance
{
    private Transform _origin;
    private LayerMask _layerMask;
    private float _radius;
    private float _angle;
    private Collider2D[] _colliders;

    public ObstacleAvoidance(Transform origin, LayerMask layerMask, int obstacleLength, float radius, float angle)
    {
        _origin = origin;
        _layerMask = layerMask;
        _radius = radius;
        _angle = angle;
        _colliders = new Collider2D[obstacleLength];
    }

    public Vector2 GetDir()
    {
        int countObstacle = Physics2D.OverlapCircleNonAlloc(_origin.position, _radius, _colliders, _layerMask);
        Vector2 dirToAvoid = Vector2.zero;
        int detectedObs = 0;
        for (int i = 0; i < countObstacle; i++)
        {
            Collider2D currObs = _colliders[i];
            Vector2 closePoint = currObs.ClosestPoint(_origin.position);
            Vector2 diffPoint = closePoint - (Vector2)_origin.position;
            float angleToObs = Vector2.Angle(_origin.up, diffPoint);

            if (angleToObs > _angle / 2) continue;
            float dist = diffPoint.magnitude;
            detectedObs++;
            dirToAvoid += -(diffPoint).normalized * (_radius - dist);
        }

        if (detectedObs > 0)
        {
            dirToAvoid /= detectedObs;
        }
        return dirToAvoid.normalized;
    }
}
