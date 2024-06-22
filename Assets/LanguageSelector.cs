using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;
    public DialogueScript dialogueScript;
    public AudioControllerScript audioController;
    public static string current_language = "ro";

    void Start()
    {
        // Populate the dropdown with available languages
        languageDropdown.options.Clear();
        languageDropdown.options.Add(new TMP_Dropdown.OptionData("Română"));
        languageDropdown.options.Add(new TMP_Dropdown.OptionData("English"));

        SetDefaultValue();

        // Add listener for when the value of the Dropdown changes
        languageDropdown.onValueChanged.AddListener(delegate { OnLanguageChanged(languageDropdown); });
    }
    void SetDefaultValue()
    {
        languageDropdown.value = 0;
        languageDropdown.RefreshShownValue(); // Refresh the displayed value
    }
    void OnLanguageChanged(TMP_Dropdown change)
    {
        current_language = change.value == 0 ? "ro" : "en";
        
        Dialogue newDialogue = LocalizationManager.Instance.GetLocalizedText(current_language);

        // Assuming you have a method to update the dialogue lines
        dialogueScript.UpdateDialogue(newDialogue);
        audioController.ChangeLanguage(current_language);
    }
}