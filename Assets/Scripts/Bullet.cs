using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	float Range = 10000f;
	float HitParticleSpacing = 0.001f;
	
	public GameObject HitParticle;
	
	
	void  Update ()
	{
		RaycastHit Hit;
	
		if (Physics.Raycast(transform.position, transform.up, out Hit, Range))
		{
	
			if (HitParticle)
			{
				Instantiate(HitParticle, Hit.point, Quaternion.LookRotation(Hit.normal));
			}
		}
		
		Destroy(gameObject);
		Debug.DrawRay (transform.position, Hit.point);
	}
}