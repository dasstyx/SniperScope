using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Transform gun;
    [SerializeField] private Transform cam;
    [SerializeField] private float sensX, sensY;

    private Vector2 currentRotation;

    [SerializeField] private Animator camAnim;
    [SerializeField] private Animator gunAnim;

    [SerializeField] private bool isAiming;
    
    private void Awake()
    {
        currentRotation = cam.GetChild(0).rotation.eulerAngles;
        StartCoroutine(nameof(SetActiveDelayed));
    }

    private bool isActive = false;
    private IEnumerator SetActiveDelayed()
    {
        yield return new WaitForSeconds(0.2f);
        isActive = true;
    }

    private void AimAnimation()
    {
        if (Input.GetMouseButton(1) == true)
        {
            gunAnim.SetBool("aim", true);
            isAiming = true;
        }
        else
        {
            gunAnim.SetBool("aim", false);
            isAiming = false;
        }
    }

    private void PeekAnimation()
    {
        if (Input.GetMouseButton(2) == true)
        {
            camAnim.SetBool("peek", true);
        }
        else
        {
            camAnim.SetBool("peek", false);
        }
    }

    private void Update()
    {
        if(isActive == false) { return; }

        AimAnimation();
        if (isAiming)
        {
            PeekAnimation();
        }

        Vector2 mouseDelta = new Vector2(
            Input.GetAxis("Mouse X") * sensX,
           -Input.GetAxis("Mouse Y") * sensY );
        
        currentRotation += mouseDelta;
        currentRotation.x = ClampAngle(currentRotation.x);
        currentRotation.y = ClampAngle(currentRotation.y);

        LerpTransform(cam, 6.8f);
        LerpTransform(gun, 5);
    }

    private void LerpTransform(Transform tr, float speed)
    {
        Quaternion target = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        Quaternion lerpRot = Quaternion.Lerp(
            tr.rotation, 
            target,
            Time.deltaTime * speed);

        tr.rotation = lerpRot;
    }

    private float ClampAngle(float angle) => angle % 360;
}
