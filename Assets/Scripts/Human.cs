using UnityEngine;
using System.Collections.Generic;

public class Human : MonoBehaviour {

    public float moveSpeed = 5f;
    public float jumpStrength = 7f;

    #region Healthcode
    // Health & Dmg
    public int maxHealth = 100;
    private int m_health = 100;
    public int Health
    {
        get
        {
            return m_health;
        }
        set
        {
            m_health = Mathf.Min(maxHealth, value);
        }
    }
    public bool Alive = true;

    public void TakeDamage(int amount)
    {
        int newhp = Mathf.Max(Health - amount, 0);
        if (newhp == 0)
            this.Kill();

        Health = newhp;
    }

    public virtual void Kill()
    {
        Alive = false;
        Debug.Log("DED");
    }
    #endregion

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

    public void Start()
    {
    }

    private bool jumpAllowed = false;
    public void Jump()
    {
        if (jumpAllowed && rigidbody.velocity.y < 0.01)
        {
            jumpAllowed = false;
            rigidbody.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.VelocityChange);
        }
    }

    public void Update()
    {
    }

    void OnCollisionEnter()
    {
        jumpAllowed = true;
    }
}
