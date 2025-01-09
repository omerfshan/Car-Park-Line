using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] LinesDrawer lineDrawer;
    [Space]
    [SerializeField] private CanvasGroup availableCanvasGroup;
    [SerializeField] private GameObject avaliableHolder;
    [SerializeField] private Image availableLineFill;
    private bool isAvailableLineUIActive=false;
    [Space]
    [SerializeField] Image fadePanel;
    [SerializeField] float fadeDuration;

    private Route activeRoute;
    // Start is called before the first frame update
    void Start()
    {
        fadePanel.DOFade(0f, fadeDuration).From(1f);
        availableCanvasGroup.alpha = 0f;
        lineDrawer.OnBeginDraw += OnBeginDrawHandler;
        lineDrawer.OnDraw += OnDrawHandler;
        lineDrawer.OnEndDraw += OnEndDrawHandler;



    }

    private void OnBeginDrawHandler(Route route)
    {
        activeRoute = route;
        
        availableLineFill.color=activeRoute.carColor;
        availableLineFill.fillAmount = 1f;
        availableCanvasGroup.DOFade(1f, .3f).From(0f);
        isAvailableLineUIActive = true;
    }

    private void OnDrawHandler()
    {
        if (isAvailableLineUIActive)
        {
            float maxLineLength=activeRoute.maxLineLenght;
            float lineLength = activeRoute.line.lenght;

            availableLineFill.fillAmount = 1 - (lineLength / maxLineLength);
        }


    }

    private void OnEndDrawHandler()
    {
        if (isAvailableLineUIActive)
        {
            isAvailableLineUIActive = false;
            float maxLineLength = activeRoute.maxLineLenght;
            float lineLength = activeRoute.line.lenght;
            activeRoute = null;
            availableCanvasGroup.DOFade(0f, .3f).From(0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
