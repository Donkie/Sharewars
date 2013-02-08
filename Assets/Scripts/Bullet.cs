using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	
	float Range = 10000f;
	
	public GameObject HitParticle;
	public GameObject HitSound;
	
	void  Update ()
	{
		RaycastHit Hit;
	
		if (Physics.Raycast(transform.position, transform.up, out Hit, Range))
		{
	
			if (HitParticle)
			{
				Instantiate(HitParticle, Hit.point, Quaternion.LookRotation(Hit.normal));
				Instantiate(HitSound, Hit.point, Quaternion.LookRotation(transform.forward));
			}
		}
		
		Destroy(gameObject);
		Debug.DrawRay (transform.position, Hit.point);
	}
}