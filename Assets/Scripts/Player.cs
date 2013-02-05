using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {  
      
    public float moveSpeed = 5f;  
    private float movement;
	
	public static float distanceTraveled;
	public float acceleration;
	public Vector3 jumpVelocity;
	private bool touchingPlatform;
  
    void Update()
	{  
        
		movement = Input.GetAxis("Horizontal") * moveSpeed;
        movement *= Time.deltaTime;
        transform.Translate(movement,0.0f, 0.0f);
		
		Vector3 pos = transform.position;
		pos.z = 0;
		transform.position = pos;
		
		transform.rotation = Quaternion.AngleAxis (0, Vector3.up);
		
		if (touchingPlatform & Input.GetButtonDown ("Jump"))
		{
			rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
			touchingPlatform = false;
		}
		distanceTraveled = transform.localPosition.x;
		
    }
	
	void FixedUpdate()
	{
		if (touchingPlatform)
		{
			rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}
	
	void OnCollisionEnter()
	{
		touchingPlatform = true;
	}
	
	void OnCollisionExit()
	{
		touchingPlatform = false;
	}
	
}  