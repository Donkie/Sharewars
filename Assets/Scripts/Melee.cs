using UnityEngine;
using System.Collections;

public class Melee : MonoBehaviour 
{
	public Player owner;
	
	float attackTimer;
	float coolDown;
	// Use this for initialization
	void Start () 
	{
		attackTimer = 0;
		coolDown = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (attackTimer > 0)
			attackTimer -= Time.deltaTime;
		
		if(attackTimer < 0)
			attackTimer = 0;
		
		if (Input.GetKeyUp(KeyCode.F))
		{
			if (attackTimer  == 0)
			{
				Attack();
				attackTimer = coolDown;
			}
		}
	}
	
	public GameObject FindClosestEnemy()
	{
		GameObject[] target;
		target = GameObject.FindGameObjectsWithTag("Zombie");
		GameObject closest = null;
		float distance2 = Mathf.Infinity;
		Vector3 position = transform.position;
		
		foreach (GameObject go in target)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if(curDistance < distance2)
			{
				closest = go;
				distance2 = curDistance;
			}
		}
		
		return closest;
	}
	
	private void Attack()
	{
		
		float distance = Vector3.Distance(FindClosestEnemy().transform.position, transform.position);
		
		Vector3 dir = (FindClosestEnemy().transform.position - transform.position).normalized;
		
		float direction = Vector3.Dot(dir, transform.up);
		
		Debug.Log (direction);
		
		
		if (distance < 1.6)
		{

				Human hit = (Human)FindClosestEnemy().collider.GetComponent("Zombie");
				hit.TakeDamage(10);

		}
	}

}
