using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour 
{
	bool paused = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Esc"))
		{
			paused = togglePause();
		}
	}
	void OnGUI()
	{
		if(paused)
		{
			GUILayout.Label ("Paused!");
			
			if(GUILayout.Button ("Unpause"))
			{
				paused = togglePause();
			}
		}
	}
	bool togglePause()
	{
		if (Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);
		}
	}
}
