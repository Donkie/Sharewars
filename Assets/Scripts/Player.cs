using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {  
      
    public float moveSpeed = 2f;  
    private float movement;
	public float jumpSpeed = 8.0f;
	
	private bool isonground = false;
  
    void Update() {  
        movement = Input.GetAxis("Horizontal") * moveSpeed;
        movement *= Time.deltaTime;
        transform.Translate(movement,0.0f, 0.0f);
		
		Vector3 pos = transform.position;
		pos.z = 0;
		transform.position = pos;
		
		transform.rotation = Quaternion.AngleAxis (0, Vector3.up);
		
    }  
}  