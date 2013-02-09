using UnityEngine;
using System.Collections;

public class Zombie : Human {
    void Start()
    {
        base.Start();
        Health = 10;
    }

    void FixedUpdate()
    {
        if (Alive)
        {
            GameObject ply = GameObject.FindWithTag("Player");

            double xdiff = Mathf.Clamp(ply.transform.position.x - transform.position.x, -1, 1);
            transform.Translate(new Vector3((float)(xdiff * 0.01), 0, 0));
        }
    }

    public override void Kill()
    {
        base.Kill();
        Destroy(this.gameObject, 0.1f);
    }
}
