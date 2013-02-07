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
	
		if (Physics.Raycast(transform.position, transform.forward,out Hit, Range))
		{
	
			if (HitParticle)
			{
				Instantiate(HitParticle, Hit.point + (Hit.normal * HitParticleSpacing), Quaternion.LookRotation(Hit.normal));
			}
		}
		Destroy(gameObject);
	}
}