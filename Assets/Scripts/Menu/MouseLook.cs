using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{

    //public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    //public RotationAxes axes = RotationAxes.MouseX;
    public float sensitivityX = 1F;
    public float sensitivityY = 1F;

    public float minimumX = -30F;
    public float maximumX = 30F;

    public float minimumY = -15F;
    public float maximumY = 15F;

    public Camera targetcamera;
    public MainMenuController controller;

    float rotationY = 0F;

    private GameObject curflytarg;
    private bool isFlying = false;
    public void FlyToObject(GameObject obj)
    {
        curflytarg = obj;
        isFlying = true;
    }

    void Update()
    {
        if (isFlying)
        {
            Vector3 TargPos = curflytarg.transform.position;
            TargPos.z = transform.position.z;
            TargPos.x = TargPos.x + ((curflytarg.GetComponent<BoxCollider>().size.x * curflytarg.transform.localScale.x) / 2);
            TargPos.y = TargPos.y - ((curflytarg.GetComponent<BoxCollider>().size.y * curflytarg.transform.localScale.y) / 2);

            Vector3 DebugVec = TargPos;
            DebugVec.z = 0;
            Debug.DrawLine(transform.position, DebugVec, Color.red, 1);

            Vector3 NextPos = Vector3.Lerp(transform.position, TargPos, 0.01f);
            transform.position = NextPos;
        }

        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        //transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);


        RaycastHit hit = new RaycastHit();
        Ray ray = targetcamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            GameObject hitobj = hit.collider.gameObject;
            if (hitobj.tag == "HighlightText")
            {
                controller.HighlightText(hitobj);
            }
        }
    }
}

/*
Keeper

if (axes == RotationAxes.MouseXAndY)
{
}
else if (axes == RotationAxes.MouseX)
{
	transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
}
else
{
	rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
	rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
	transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
}
*/