using UnityEngine;
using System.Collections.Generic;

public class Player : Human {  
    new void Start()
    {
        base.Start();
        AddAmmo("500mm", 128);
    }

    public void CalcCamera()
    {
        Transform trans = Camera.main.transform;
        trans.position = transform.position + transform.forward * -5 + transform.up * 2;

        trans.LookAt(transform);
    }

    new void Update()
    {
        CalcCamera();
        base.Update();

        if (Input.GetButtonDown("Jump"))
            Jump();
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
}