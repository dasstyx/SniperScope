using System;
using UnityEngine;

[Serializable]
public class ScopeCulling
{
    private ScopeData scopeData;
    [SerializeField] private float cullingAngle;
    [SerializeField] private float cullingDistance;

    [SerializeField] private bool _isActive = true;
    public bool isActive
    {
        get { return _isActive; }
        set
        {
            if (value) { EnableLens(); }
            else { DisableLens(); }
        }
    }
    

    public void Setup(ScopeData scopeData)
    {
        this.scopeData = scopeData;
    }

    public bool CullingTest()
    {
        bool useAngle = cullingAngle != 0;
        bool useDistance = cullingDistance != 0;
        if (useAngle || useDistance)
        {
            if (useAngle && CullAngle() == true  ||
                useDistance && CullDistance() == true)
            {
                if (isActive == true) { isActive = false; }
                return false;
            }
            if (isActive == false) { isActive = true; }
        }
        return true;
    }



    private bool CullAngle()
    {
        Vector3 lensPosition = scopeData.lensTransform.position;
        Vector3 camPosition = scopeData.mainCam.transform.position;

        Vector3 camDirection = lensPosition - camPosition;
        camDirection.Normalize();
        Vector3 lensDirection = scopeData.lensTransform.forward;

        float angle = Vector3.Angle(camDirection, lensDirection);
        return angle > cullingAngle;
    }

    private bool CullDistance()
    {
        Vector3 lensPosition = scopeData.lensTransform.position;
        Vector3 camPosition = scopeData.mainCam.transform.position;

        float distance = Vector3.Distance(lensPosition, camPosition);
        return distance > cullingDistance;
    }

    private void EnableLens()
    {
        scopeData.lensCam.enabled = true;
        scopeData.renderer.enabled = true;
        _isActive = true;
        Debug.Log("Scope lens enabled!");
    }
    private void DisableLens()
    {
        scopeData.lensCam.enabled = false;
        scopeData.renderer.enabled = false;
        _isActive = false;
        Debug.Log("Scope lens disabled!");
    }
}