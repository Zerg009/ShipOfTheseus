using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondSceneScript : BaseSpriteChanger
{
protected override bool CheckStopConditions()
    {
        // Custom stop condition for the boat
        if (currentIndex == 6)
        {
            spriteRenderer.sprite = null;
            IsFinished = true;
            ChangeScene();
            return true;
        }

        return base.CheckStopConditions(); // Call base method for default conditions
    }

    protected override void Start()
    {
        base.Start();
        // Additional initialization for the custom boat
    }
    protected override void Update()
    {
        base.Update();
        // Additional logic to run in Update if necessary
    }
    // protected override void OnMouseDown()
    // {
    //     base.OnMouseDown();
    //     // Additional logic when the boat is clicked
    // }
}
