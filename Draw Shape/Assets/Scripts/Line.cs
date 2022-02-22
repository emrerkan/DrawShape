using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D rigidbody;

    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;

    float pointsMinDistance = 0.1f; // Minimum distance between line's points
    float circlesColliderRadius;
    
    public void AddPoint(Vector2 newPoint)
    {
        // If distance between last point and new point is less than MinPointsDistance: Don't draw the point.
        if(pointsCount >= 1 && Vector2.Distance(newPoint,GetLastPoint()) < pointsMinDistance)
        {
            return;
        }
        points.Add(newPoint);
        pointsCount++;

        // Add Circle Collider to the Point

        CircleCollider2D circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newPoint;
        circleCollider.radius = circlesColliderRadius;

        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);
        // The edge collider accepts only 2 or more points
        if(pointsCount >1)
        {
            edgeCollider.points = points.ToArray();

        }
    }
    public Vector2 GetLastPoint()
    {
        return (Vector2)lineRenderer.GetPosition(pointsCount - 1);
    }
    public void UsePhysics(bool usePhysics)
    {
        rigidbody.isKinematic = !usePhysics;
    }
    public void SetLineColor(Gradient colorGradient)
    {
        lineRenderer.colorGradient = colorGradient;
    }
    public void SetPointsMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }
    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        circlesColliderRadius = width / 2;
        edgeCollider.edgeRadius = circlesColliderRadius;

    }

}
