using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpriteChanger : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    protected int currentIndex = 1; // Start index
    protected bool IsFinished = false;
    public static bool IsChangedMode = false;
    public GameObject nextScene;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadSprite();
    }

    // protected virtual void OnMouseDown()
    // {
    //     if (!IsFinished)
    //     {
    //         currentIndex++;
    //         LoadSprite();
    //     }
    // }
 protected virtual void Update()
    {
        if (!IsFinished && Input.GetMouseButtonDown(0)) // Detect left mouse click
        {
            currentIndex++;
            LoadSprite();
        }
    }
    protected void LoadSprite()
    {
        if (CheckStopConditions())
        {
            return;
        }

        // Construct the sprite name based on the currentIndex
        string spriteName = IsChangedMode ? "boat_changed_" + currentIndex : "boat_" + currentIndex;

        // Attempt to load the sprite from Resources
        Sprite newSprite = Resources.Load<Sprite>(spriteName);

        // If the sprite was loaded successfully, assign it to the SpriteRenderer
        if (newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            // Optionally handle if the sprite could not be loaded (e.g., end of sprites)
            Debug.LogWarning("Sprite " + spriteName + " not found in Resources.");
        }
    }

    protected virtual bool CheckStopConditions()
    {
        // Default stop conditions
        // if (currentIndex == 8 && !IsChangedMode)
        // {
        //     spriteRenderer.sprite = null;
        //     IsFinished = true;
        //     ChangeScene();
        //     return true;
        // }

        // if (IsChangedMode && currentIndex == 9)
        // {
        //     IsFinished = true;
        //     ChangeScene();
        //     return true;
        // }

        return false;
    }

    protected void ChangeScene()
    {
        if (nextScene != null)
        {
            nextScene.SetActive(true);
        }
        IsChangedMode = true;
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
