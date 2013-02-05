using UnityEngine;
using System.Collections;
 
public class MainCamera : MonoBehaviour {
	
	float CameraSpeed = 0.05F;
	
    void Update () {
		
        transform.position = new Vector3(Input.mousePosition.x+50,0,Input.mousePosition.z-100)*CameraSpeed;
    }
}