using UnityEngine;
using System.Collections;

public class PlayGame : MenuItem
{
    public override void OnActivated()
    {
        base.OnActivated();
        Application.LoadLevel("world");
    }
}
