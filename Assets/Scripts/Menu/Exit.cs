using UnityEngine;
using System.Collections;

public class Exit : MenuItem
{
    public override void OnActivated()
    {
        base.OnActivated();
        Application.Quit();
    }
}
