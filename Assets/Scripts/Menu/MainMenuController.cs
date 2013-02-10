using UnityEngine;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    public List<GameObject> MenuTexts = new List<GameObject>();
    public MouseLook maincam;

    void Start()
    {
        foreach (GameObject text in GameObject.FindGameObjectsWithTag("HighlightText"))
        {
            MenuTexts.Add(text);
        }
    }

    private GameObject HighlightedObj;
    public void HighlightText(GameObject obj)
    {
        if (!MenuTexts.Contains(obj))
            return;

        if (HighlightedObj)
            HighlightedObj.GetComponent<TextMesh>().renderer.material.color = Color.white;

        HighlightedObj = obj;
        obj.GetComponent<TextMesh>().renderer.material.color = Color.cyan;

        maincam.FlyToObject(obj);
    }

    private void OnClick()
    {
        if (HighlightedObj)
        {
            MenuItem item = (MenuItem)HighlightedObj.GetComponent("MenuItem");
            if (item)
                item.OnActivated();
        }
    }

    private bool MouseDown = false;
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!MouseDown)
            {
                MouseDown = true;
                OnClick();
            }
        }
        else
        {
            MouseDown = false;
        }
    }
}
