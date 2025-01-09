using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    [SerializeField] float minPointsDistance;

    [HideInInspector] public List<Vector3> points = new();
    [HideInInspector] public int pointsCount = 0;
    [HideInInspector] public float lenght=0f;
    private float pointFixedYaxis;
    private Vector3 prevPoint;
    private void Start()
    {
        pointFixedYaxis = lineRenderer.GetPosition(0).y;
        Clear();
    }
    public void Init()
    {
        gameObject.SetActive(true);
    }
    public void Clear()
    {
        gameObject.SetActive(false);
        lineRenderer.positionCount = 0;
        pointsCount = 0;
        points.Clear();
        lenght = 0f;
    }
    public void AddPoint(Vector3 newPoint)
    {
        newPoint.y = pointFixedYaxis;
        if (pointsCount>=1 && Vector3.Distance(newPoint,GetLastPoint())<minPointsDistance)
            return;
        
      //else;
      if(pointsCount==0)
        prevPoint = newPoint;

        points.Add(newPoint);
        pointsCount++;

        lenght+=Vector3.Distance(prevPoint,newPoint);
        prevPoint = newPoint;

        //linerenderer update
        lineRenderer.positionCount=pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);
    
    
    }
    private Vector3 GetLastPoint()
    {
        return lineRenderer.GetPosition(pointsCount - 1);
    }
    public void SetColor(Color color)
    {
        lineRenderer.sharedMaterials[0].color = color;
    }  
}
