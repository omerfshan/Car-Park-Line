using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    public Route route;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] ParticleSystem fx;
    private ParticleSystem.MainModule mainModule;

    private void Start()
    {
        mainModule = fx.main;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            if (car.route==route)
            {
                Game.Instance.OnCarEntersPark?.Invoke(route);
                StartFX();
            }
        }
    }

    private void StartFX()
    {
       mainModule.startColor = route.carColor;
        fx.Play();
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

}
