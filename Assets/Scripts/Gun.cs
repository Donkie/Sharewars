using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public Transform target; //Assign to the object you want to rotate
	
	// Donkies shit
	private double FireTimer = 0;
	private bool AllowFire = false;
	public double FireRate = 0.1;
	
	// Gun Specs
	public float MaxClipSize = 32;
	public float AmmoInCurrentClip = 32;
	public float ExtraAmmo = 128;
	public float MaxCarryingAmmo = 256;
	
	//  Bullet
	public GameObject Bullet;
	public GameObject BulletSpawn;
	public GameObject BulletSound;

	//Reloads
	bool  Reloading = false;
	string ReloadName;
	
	void  Update ()
	{
		
    	Vector3 mouse_pos = Input.mousePosition;
    	Vector3 object_pos = Camera.main.WorldToScreenPoint(target.position);
    	Vector3 diff = (mouse_pos - object_pos).normalized;
		float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
    	transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		
	}
	
	void  LateUpdate ()
	{


		if (AmmoInCurrentClip > MaxClipSize)

		AmmoInCurrentClip = MaxClipSize;

		if (ExtraAmmo > MaxCarryingAmmo)

		ExtraAmmo = MaxCarryingAmmo;

		if (MaxClipSize < 0)
		{
			MaxClipSize = 0;
		}
		if (AmmoInCurrentClip < 0)
		{
			AmmoInCurrentClip = 0;
		}
		if (!Reloading && AmmoInCurrentClip < MaxClipSize && ExtraAmmo > 0 && Input.GetButtonDown("Reload"))
		{
			Reloading = true;
		}
	
		if (!Reloading && AmmoInCurrentClip == 0 && ExtraAmmo > 0 && Input.GetButtonDown("Fire1"))
		{
			Reloading = true;
		}
	
		if (Reloading)
		{
			if (ExtraAmmo >= MaxClipSize - AmmoInCurrentClip)
			{
				ExtraAmmo -= MaxClipSize - AmmoInCurrentClip;
				AmmoInCurrentClip = MaxClipSize;
			}

		if (ExtraAmmo < MaxClipSize - AmmoInCurrentClip)
		{
			AmmoInCurrentClip += ExtraAmmo;
			ExtraAmmo = 0;
		}
			Reloading = false;
		}
		
		// Donkies shit
		if(!AllowFire)
		{
			FireTimer += Time.deltaTime;
			if(FireTimer >= FireRate)
			{
				FireTimer = 0;
				AllowFire = true;
			}
		}
		else
		{
			if(Input.GetButton("Fire1") && AmmoInCurrentClip > 0)
			DoFire();
		}
		
	}
	
	// Donkies shit
	public void DoFire()
	{
        AllowFire = false;
        	
			if (Bullet)
			{
				AmmoInCurrentClip -= 1;
				Instantiate(Bullet,BulletSpawn.transform.position,BulletSpawn.transform.rotation);
			}
	}
	
}