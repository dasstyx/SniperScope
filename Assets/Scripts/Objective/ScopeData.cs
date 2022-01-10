using System;
using UnityEngine;

[Serializable]
public class ScopeData
{
    [SerializeField] private Camera _mainCam;
    public Camera mainCam { get => _mainCam; }

    [SerializeField] private Camera _lensCam;
    public Camera lensCam { get => _lensCam; }

    [SerializeField] private Transform _lensTransform;
    public Transform lensTransform { get => _lensTransform; }
    public Vector3 worldStartPos => lensParent.TransformPoint(startPos);


    [HideInInspector] public Vector3 startPos;
    public Transform lensParent => lensCam.transform.parent;

    public float lensRadius;

    [HideInInspector] public MeshRenderer renderer;


    public void Setup()
    {
        startPos = _lensCam.transform.localPosition;
    }
}