using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [HideInInspector] public bool isActive=true;
    [HideInInspector] public Vector3[] linePoints;
    [SerializeField] LinesDrawer linesDrawer;
    public float maxLineLenght;
    [Space]
    public Line line;
    public Car car;
    public Park park;

    [Space]
    [Header("Colors :")]
    public Color carColor;
    [SerializeField] Color lineColor;
    private void Start()
    {
        linesDrawer.OnParkLinkedToLine += OnParkLinkedToLineHandler;
    }
    private void OnParkLinkedToLineHandler(Route route,List<Vector3> points)
    {
        if (route==this)
        {
            linePoints = points.ToArray();
            Game.Instance.RegisterRoute(this);
        }
    }


    public void Disactivate()
    {
        isActive = false;

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying && line!=null && car!=null && park!=null)
        {
            line.lineRenderer.SetPosition(0, car._bottomTransform.position);
            line.lineRenderer.SetPosition(1, park.transform.position);

            car.SetColor(carColor);
            park.SetColor(carColor);
            line.SetColor(lineColor);
        }
    }
#endif



}
