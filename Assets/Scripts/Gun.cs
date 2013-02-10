using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
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
    public int MaxClipSize = 500;
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
        float degangle = angle * Mathf.Rad2Deg;

        bool flip = Mathf.Abs(degangle) > 90;
        transform.eulerAngles = new Vector3(flip ? 180 : 0, 0, flip ? -degangle : degangle);

        Vector3 worldpos = owner.transform.position + (new Vector3(Mathf.Cos(angle) * 0.6f, Mathf.Sin(angle) * 0.6f, 0));
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
        if (Clip == 0 && !Reloading)
        {
            Sounds.PlaySound(ClickSound, BulletSpawn.transform.position);
        }
    }

    private bool isClicked = false;
    void LateUpdate()
    {
        if (!Reloading && Input.GetButtonDown("Reload") && Clip < MaxClipSize && owner.GetAmmo(Ammotype) > 0)
        {
            Reloading = true;
            Sounds.PlaySound(ReloadSound, BulletSpawn.transform.position);
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
        if (!AllowFire)
        {
            FireTimer += Time.deltaTime;
            if (FireTimer >= FireRate)
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
            Instantiate(Bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
            Sounds.PlaySound(GunSound, BulletSpawn.transform.position);
        }
    }
}