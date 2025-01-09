using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LinesDrawer : MonoBehaviour
{
    [SerializeField] UserInput userInput;
    [SerializeField] int interactableLayer;

    private Line currentLine;
    private Route currentRoute;

    RaycastDetector raycastDetector = new();
    //events:
    public UnityAction<Route> OnBeginDraw;
    public UnityAction OnDraw;
    public UnityAction OnEndDraw;
    public UnityAction<Route, List<Vector3>> OnParkLinkedToLine;



    private void Start()
    {
        userInput.OnMouseDown += OnMouseDownHandler;
        userInput.OnMouseMove += OnMouseMoveHandler;
        userInput.OnMouseUp += OnMouseUpHandler;
    }


    //BeginDraw-------------------------------
    private void OnMouseDownHandler()
    {
       ContactInfo contactInfo=raycastDetector.RayCast(interactableLayer);
        if (contactInfo.contacted)
        {
            bool isCar = contactInfo.collider.TryGetComponent(out Car _car);
            if (isCar && _car.route.isActive)
            {
                currentRoute = _car.route;
                currentLine = currentRoute.line;
                currentLine.Init();

                OnBeginDraw?.Invoke(currentRoute);
            
            }
        
        
        }
    }
    //Draw------------------------------------
    private void OnMouseMoveHandler()
    {
        if (currentRoute!=null)
        {
            ContactInfo contactInfo = raycastDetector.RayCast(interactableLayer);
            if (contactInfo.contacted)
            {
                Vector3 newPoint = contactInfo.point;
                if (currentLine.lenght>=currentRoute.maxLineLenght)
                {
                    currentLine.Clear();
                    OnMouseUpHandler();
                    return;
                }
                currentLine.AddPoint(newPoint);
                OnDraw?.Invoke();
                bool isPark = contactInfo.collider.TryGetComponent(out Park _park);
               
                if (isPark)
                {
                    Route parkRoute = _park.route;
                    if (parkRoute==currentRoute)
                    {
                        currentLine.AddPoint(contactInfo.transform.position);
                        OnDraw?.Invoke();
                    }
                    else
                    {
                        //delete the line:
                        currentLine.Clear();
                        

                    }
                    OnMouseUpHandler();

                }
            
            
            }
        }
    }
    //End Draw--------------------------------
    private void OnMouseUpHandler()
    {

        if (currentRoute != null)
        {
            ContactInfo contactInfo = raycastDetector.RayCast(interactableLayer);

            if (contactInfo.contacted)
            {
                bool isPark = contactInfo.collider.TryGetComponent(out Park _park);

                if (currentLine.pointsCount<2||!isPark)
                {
                    //delete the line
                    currentLine.Clear();
                }
                else
                {
                    OnParkLinkedToLine?.Invoke(currentRoute, currentLine.points);
                    currentRoute.Disactivate();
                }
            
            }
            else
            {
                //delete line
                currentLine.Clear();
            }
        
        
        }
        ResetDrawer();
        OnEndDraw?.Invoke();
    }
    private void ResetDrawer()
    {
        currentRoute = null;
        currentLine = null;

    }


}
