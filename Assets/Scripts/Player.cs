using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {  
    public float moveSpeed = 5f;
    public float jumpStrength = 7f;

    void Start()
    {
        rigidbody.freezeRotation = true;
    }

    private bool jumpAllowed = false;
    void Update()
	{  
		
		//transform.rotation = Quaternion.AngleAxis (0, Vector3.up); // Shouldn't need this since we've frozen the rotation

        if (jumpAllowed && rigidbody.velocity.y < 0.01 && Input.GetButtonDown("Jump"))
		{
            jumpAllowed = false;
			rigidbody.AddForce(new Vector3(0,jumpStrength,0), ForceMode.VelocityChange);
		}
    }
	
	void FixedUpdate()
    {
        float movement = Input.GetAxis("Horizontal") * moveSpeed;
        movement *= Time.deltaTime;
        transform.Translate(movement, 0.0f, 0.0f);

        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
	}

    void OnCollisionEnter()
    {
        jumpAllowed = true;
    }
}  