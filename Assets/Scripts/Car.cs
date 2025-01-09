using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    public Route route;
    public Transform _bottomTransform;
    public Transform _bodyTransform;
    [SerializeField] Rigidbody rb;
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] ParticleSystem smokeFX;
    [SerializeField] float danceValue;
    [SerializeField] float durationMultiplier;

    private void Start()
    {
        _bodyTransform.DOLocalMoveY(danceValue, .1f).
            SetLoops(-1, LoopType.Yoyo).
            SetEase(Ease.Linear);

          
    
    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Car otherCar))
        {
            StopDancingAnim();
            rb.DOKill(false);
            //add explosion;
            Vector3 hitpoint = collision.contacts[0].point;
            AddExplosionForce(hitpoint);
            smokeFX.Play();

            Game.Instance.OnCarCollison?.Invoke();
        }
    }

    private void AddExplosionForce(Vector3 hitpoint)
    {
        rb.AddExplosionForce(400f, hitpoint, 3f);
        rb.AddForceAtPosition(Vector3.up*2f,hitpoint,ForceMode.Impulse);
        rb.AddTorque(new Vector3(GetRandomAngle(),GetRandomAngle(),GetRandomAngle()));
    }
    private float GetRandomAngle()
    {
        float angle = 10f;
        float rand = Random.value;
        return rand>.5f ?angle : -angle;

    }
    public void Move(Vector3[] path)
    {
        rb.DOLocalPath(path, 2f * durationMultiplier * path.Length).
            SetLookAt(.01f, false).
            SetEase(Ease.Linear);
        
    
    
    }

    public void StopDancingAnim()
    {
        _bodyTransform.DOKill(true);
    }

    public void SetColor(Color color)
    {
        _meshRenderer.sharedMaterials[0].color = color;
    }
}
