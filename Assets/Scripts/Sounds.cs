using UnityEngine;
using System.Collections.Generic;
/*
dont ask what I did here im fucking stupid
public class ObjectDestroyer : MonoBehaviour
{
    public GameObject source;

    public ObjectDestroyer(GameObject src, float len)
    {
        this.source = src;

        Invoke("Destroy", len);
    }
    public void Destroy()
    {
        Destroy(source);
    }
}
*/
public class Sounds : MonoBehaviour
{
    public static void PlaySound(GameObject sound, Vector3 pos)
    {
        GameObject obj2 = (GameObject)Instantiate(sound, pos, new Quaternion());
        Destroy(obj2, obj2.GetComponent<AudioSource>().clip.length);
        //new ObjectDestroyer(obj2, obj2.GetComponent<AudioSource>().clip.length); here too
    }
}
