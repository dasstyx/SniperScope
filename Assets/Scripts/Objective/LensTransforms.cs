using UnityEngine;


public class LensTransforms
{
    private ScopeData scopeData;

    public LensTransforms(ScopeData scopeData)
    {
        this.scopeData = scopeData;
    }

    public void BindLensCamera()
    {
        Vector3 direction = scopeData.lensTransform.position - scopeData.mainCam.transform.position;
        direction.Normalize();

        Vector3 reflectAxis = scopeData.lensTransform.right;
        Debug.DrawRay(scopeData.worldStartPos, direction, Color.yellow);
        Debug.DrawRay(scopeData.worldStartPos, reflectAxis, Color.black);

        Vector3 reflectedDir = Vector3.Reflect(direction, reflectAxis);
        Debug.DrawRay(scopeData.worldStartPos, reflectedDir, Color.red);

        scopeData.lensCam.transform.localPosition = LensCamPos(direction);
        //lensCam.transform.rotation = Quaternion.LookRotation(direction);
    }

    private Vector3 ProjectOnScope(Vector3 point)
    {
        Vector3 projPlane = scopeData.worldStartPos - scopeData.lensTransform.position;
        projPlane.Normalize();

        Vector3 projected = Vector3.ProjectOnPlane(point, projPlane);
        return projected;
    }

    private Vector3 LensCamPos(Vector3 direction)
    {
        float camDistance = Vector3.Distance(scopeData.mainCam.transform.position, scopeData.lensTransform.position);
        float displace = camDistance / scopeData.lensRadius;
        Vector3 pos = displace * direction;

        Vector3 projPos = ProjectOnScope(pos);
        projPos += scopeData.startPos;
        return projPos;
    }
}