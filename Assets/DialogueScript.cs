using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public AudioControllerScript audioController;
    public TextMeshProUGUI textComponent;
    public GameObject intro;
    public GameObject firstScene;
    public string[][] allLines; // Two-dimensional array to hold lines for different scenes
    public float textSpeed;
    public int index;
    private int currentScene;
    private Dialogue currentDialogue;
    public TMP_Dropdown languageDropdown;
    public bool autoplay = true; // Flag to enable autoplay
    public static bool stopDialogue = false;

    void Start()
    {
        textComponent.text = string.Empty;
        currentScene = 0; // Start with the first scene
        currentDialogue = LocalizationManager.Instance.GetLocalizedText(LanguageSelector.current_language);
        autoplay = true;
        StartDialogue(currentDialogue.text_1); // Start with text_1 as default
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !autoplay && !stopDialogue)
        {
            if (allLines != currentDialogue.text_1)
            {
                allLines = currentDialogue.text_1;
            }

            if (!IsPointerOverUIElement())
            {

                if (textComponent.text == allLines[currentScene][index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = allLines[currentScene][index];
                }
            }
        }
    }
    bool IsPointerOverUIElement()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<TMP_Dropdown>() != null)
            {
                return true;
            }
        }

        return false;
    }

    void StartDialogue(string[][] text)
    {
        StopAllCoroutines();
        index = 0;
        allLines = text;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        audioController.PlayAudioClip(index);
        foreach (char c in allLines[currentScene][index].ToCharArray())
        {
            textComponent.text += c;
            //Debug.Log("Writinng in dialogue script coroutine " + allLines[currentScene][index]);
            yield return new WaitForSeconds(textSpeed);
        }
        if (autoplay)
        {
            yield return new WaitForSeconds(0.3f); // Wait 1 second before showing the next line
            NextLine();
        }
    }

    public void NextLine()
    {
        StopAllCoroutines();
        if (index < allLines[currentScene].Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            textComponent.text = string.Empty;
            //gameObject.SetActive(false);
        }
    }

    // Method to change the current scene
    public void ChangeScene(int sceneIndex, bool enableAutoplay = false)
    {
        textComponent.text = string.Empty;
        if(autoplay)
            autoplay = enableAutoplay;
        if (sceneIndex >= 0 && sceneIndex < allLines.Length)
        {
            currentScene = sceneIndex;
            StartDialogue(currentDialogue.text_1);
        }
        else
        {
            Debug.LogWarning("Scene index out of range.");
        }
    }
    void OnEnable()
    {
        textComponent.text = string.Empty;
    }
    public void UpdateDialogue(Dialogue newDialogue)
    {
        currentDialogue = newDialogue;
    }
}
