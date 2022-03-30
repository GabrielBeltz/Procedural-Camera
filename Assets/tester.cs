using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour
{
    public Transform PointA, PointB, Value, Result;

    void Update()
    {
        float distanceA, distanceB, distanceTotal;
        distanceA = Vector2.Distance(PointA.position, Value.position);
        distanceB = Vector2.Distance(PointB.position, Value.position);

        float t = Mathf.InverseLerp(0, distanceA + distanceB, distanceA);

        Debug.Log($"Total Distance = {distanceA + distanceB}, t = {t}");
        float yPosition, xPosition, zPosition;

        xPosition = Mathf.Lerp(PointA.position.x, PointB.position.x, t);
        yPosition = Mathf.Lerp(PointA.position.y, PointB.position.y, t);
        zPosition = PointA.position.z;

        Result.position = new Vector3(xPosition, yPosition, zPosition);
    }
}
