using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;
    [HideInInspector] public List<Route> readyRoutes = new();

    private int totalRoutes;
    private int successfullParks;
    //:events
    public UnityAction<Route> OnCarEntersPark;
    public UnityAction OnCarCollison;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalRoutes = transform.GetComponentsInChildren<Route>().Length;
       successfullParks = 0;
        OnCarEntersPark += OnCarEntersParkHandler;
        OnCarCollison += OnCarCollisonHandler;
    }

    private void OnCarCollisonHandler()
    {
        Debug.Log("Gameover");
        DOVirtual.DelayedCall(2f, () =>
        {

            int currentLevel = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentLevel);
        });
    }

    private void OnCarEntersParkHandler(Route route)
    {
        route.car.StopDancingAnim();
        successfullParks++;
        if (successfullParks==totalRoutes)
        {
            Debug.Log("Win");
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            DOVirtual.DelayedCall(1.3f, () =>
            {
                if (nextLevel < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextLevel);

                }
                else
                    Debug.LogWarning("No next level to load");
            });
        }
    }

    public void RegisterRoute(Route route)
    {
        readyRoutes.Add(route);
        if (readyRoutes.Count==totalRoutes)
        {
            MoveAllCars();
        }
    }
    private void MoveAllCars()
    {
        foreach (var route in readyRoutes)
        {
            route.car.Move(route.linePoints);
        }
    }
}
