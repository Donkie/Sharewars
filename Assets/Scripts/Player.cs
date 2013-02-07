using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {  
    public float moveSpeed = 5f;
    public float jumpStrength = 7f;

    #region Ammocode
    public Dictionary<string, int> ammo = new Dictionary<string, int>();
    public int TakeAmmo(string type, int amount)
    {
        if (!ammo.ContainsKey(type))
            return 0;

        int ammotaken = System.Math.Min(GetAmmo(type), amount);
        ammo[type] = GetAmmo(type) - ammotaken;

        return ammotaken;
    }
    public int GetAmmo(string type)
    {
        if (!ammo.ContainsKey(type))
            return 0;

        return ammo[type];
    }
    public void AddAmmo(string type, int amount)
    {
        if (ammo.ContainsKey(type))
        {
            ammo[type] = GetAmmo(type) + amount;
            return;
        }
        ammo.Add(type, GetAmmo(type) + amount);
    }
    #endregion

    void Start()
    {
        AddAmmo("500mm", 128);
    }

    public void CalcCamera()
    {
        Transform trans = Camera.main.transform;
        trans.position = transform.position + transform.forward * -5 + transform.up * 2;

        trans.LookAt(transform);
    }

    private bool jumpAllowed = false;
    void Update()
	{
        CalcCamera();
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