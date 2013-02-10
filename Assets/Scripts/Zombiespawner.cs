using UnityEngine;
using System.Collections;

public class Zombiespawner : MonoBehaviour
{
    public GameObject zombie;
    // Update is called once per frame
    void Update()
    {
        if (Random.value > 0.92)
        {
            GameObject[] Platforms = GameObject.FindGameObjectsWithTag("Platform");

            int Rand = (int)(Random.value * ((float)Platforms.Length));
            GameObject platform = Platforms[Rand];

            float xcenter = platform.transform.position.x;
            float xscale = platform.transform.localScale.x / 2;

            float xleft = xcenter - xscale;
            float xright = xcenter + xscale;

            //float randx = (xright - xleft) * Random.value + xleft; // Linear interpolation with random multipler
            float randx = Mathf.Lerp(xright, xleft, Random.value); // Linear interpolation with random multipler
            Instantiate(zombie, new Vector3(randx, platform.transform.position.y + 1, 0), new Quaternion());
        }
    }
}
