using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    public Player owner;

	// Donkies shit
	private double FireTimer = 0;
	private bool AllowFire = false;
	public double FireRate = 0.1;
	
	public GameObject GunSound;
	public GameObject ReloadSound;
	public GameObject ClickSound;
	
	// Gun Specs
    public string Ammotype = "500mm";
	public int MaxClipSize = 5;
    private int m_clip = 0;
    public int Clip
    {
        get
        {
            return m_clip;
        }
        set
        {
            m_clip = System.Math.Min(value, MaxClipSize); // Limit it to clipsize
        }
    }

    bool Reloading = false;
    double ReloadTimer = 0d;
    public float ReloadTime = 2f;
	
	//  Bullet
	public GameObject Bullet;
	public GameObject BulletSpawn;

    void Start()
    {
        Clip = MaxClipSize;
        transform.parent = owner.transform;
    }

	void Update()
	{
    	Vector3 mouse_pos = Input.mousePosition;
    	Vector3 object_pos = Camera.main.WorldToScreenPoint(owner.transform.position);
    	Vector3 diff = (mouse_pos - object_pos).normalized;
		float angle = Mathf.Atan2(diff.y, diff.x);
        transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);

        Vector3 worldpos = owner.transform.position + (new Vector3(Mathf.Cos(angle)*1, Mathf.Sin(angle)*1, 0));
        transform.position = worldpos;
	}

    /*
     * Called when firebutton is being held down
     */
    void FireDown()
    {
        if (Clip > 0)
            DoFire();
    }
    /*
     * Called when the firebutton has been clicked
     */
    void FireClick()
    {
        if (Clip == 0)
            Instantiate(ClickSound, BulletSpawn.transform.position, Quaternion.LookRotation(transform.forward)); //wat
    }

    private bool isClicked = false;
	void LateUpdate()
	{
        if (!Reloading && Input.GetButtonDown("Reload") && Clip < MaxClipSize && owner.GetAmmo(Ammotype) > 0)
        {
            Reloading = true;
            Object obj = Instantiate(ReloadSound, BulletSpawn.transform.position, Quaternion.LookRotation(transform.forward));
            
        }
        if (Reloading)
        {
            ReloadTimer += Time.deltaTime;
            if (ReloadTimer >= ReloadTime)
            {
                Reloading = false;
                ReloadTimer = 0d;
                int am = owner.TakeAmmo(Ammotype, MaxClipSize);
                if (am > 0)
                    Clip = am;
            }
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
            if (Input.GetButton("Fire1"))
            {
                FireDown();
                if (!isClicked)
                    FireClick();
                isClicked = true;
            }
            else
            {
                isClicked = false;
            }
		}
		
	}
	
	// Donkies shit
	public void DoFire()
	{
        AllowFire = false;
        	
		if (Bullet)
		{
			Clip -= 1;
			Instantiate(Bullet,BulletSpawn.transform.position,BulletSpawn.transform.rotation);
			Instantiate(GunSound, BulletSpawn.transform.position, Quaternion.LookRotation(transform.forward));
		}
	}
}