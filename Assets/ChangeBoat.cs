using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ChangeBoat : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int currentIndex = 1; // Start index
    bool IsFinished = false;
    public static bool IsChangedMode = false;
    public GameObject introScene;
    public GameObject secondScene;
    bool isZoomFinished = true;
    CameraZoom cameraZoom;
    private bool isInitialized = false;
    public AudioControllerScript audioController;
    public DialogueScript dialogueScript;
    public TextMeshProUGUI textComponent;
    private void Start()
    {
        if (!isInitialized)
        {
            Initialize();
            isInitialized = true;
        }
    }
    private void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Camera cam = Camera.main;
        cameraZoom = cam.gameObject.AddComponent<CameraZoom>();
        cameraZoom.Initialize(cam, new Vector3(-0.3f, 2.13f,0), 0.8f, 2f, true,onZoomComplete);
        
        //cameraZoom = new CameraZoom(Camera.main, new Vector3(), 0.63f, 2f, true );
        LoadSprite();
    }
    void Update()
    {
        //cameraZoom.Update();
        if (Input.GetMouseButtonDown(0))
        {
            if(!isZoomFinished)
                return;

            if(IsChangedMode && currentIndex == 1)
            {
                 DialogueScript.stopDialogue = true;
            }
            currentIndex++;

            // not scene 3
            if(!IsChangedMode)
            {
                if(currentIndex == 3)
                {
                    DialogueScript.stopDialogue = true;
                }
                if(currentIndex == 7)
                {
                    DialogueScript.stopDialogue = false;
                }
                if (currentIndex == 8)
                {
                    spriteRenderer.sprite = null;
                    return;
                }
                if (currentIndex == 9)
                {
                    // IsFinished = true;
                    ChangeScene(secondScene);
                    return;
                }
            }
           if(IsChangedMode)
            {
               
                if(currentIndex == 10)
                {
                    DialogueScript.stopDialogue = false;
                }
                if (currentIndex == 10)
                {
                    cameraZoom.StartZoom(4);
                    isZoomFinished = false;
                    return;
                }

                if(currentIndex == 15)
                {
                    ChangeBoat.IsChangedMode = false;
                    ChangeScene(introScene);
                    return;
                }
            }
            
            LoadSprite();
        }

    }
    void onZoomComplete()
    {
        isZoomFinished = true;
    }
    private void LoadSprite()
    {

        // Construct the sprite name based on the currentIndex
        string spriteName;
        if (!IsChangedMode)
        {
            spriteName = "boat_" + currentIndex;
        }
        else
        {
            spriteName = "boat_changed_" + currentIndex;
        }

        // Attempt to load the sprite from Resources
        Sprite newSprite = Resources.Load<Sprite>(spriteName);

        // If the sprite w  as loaded successfully, assign it to the SpriteRenderer
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

    void ChangeScene(GameObject scene)
    {
        currentIndex = 1;
        scene.SetActive(true);
        //textComponent.text = string.Empty;
        if(IsChangedMode)
        {
            audioController.ChangeSubfolder(0);
            dialogueScript.ChangeScene(0,true);
        }else{
            audioController.ChangeSubfolder(2);
            dialogueScript.ChangeScene(2, false);
        }
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
   void OnEnable(){
        Initialize();
   }
}
