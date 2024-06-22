using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerScript : MonoBehaviour
{
public AudioSource audioSource;
    public string[] subfolderNames; // Names of subfolders within each language folder
    private Dictionary<string, AudioClip[][]> allAudioClips; // Dictionary to hold audio clips for each language
    private string currentLanguage = "ro"; // Default language
    private static int currentSubfolderIndex;

    void Awake()
    {
        subfolderNames = new string[] {
            "Intro",
            "FirstScene",
            "SecondScene",
            "ThirdScene",
        };
    }

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
            return;
        }

        LoadAllAudioClips();

        // Optionally, play the first audio clip of the first subfolder at the start
        if (allAudioClips.ContainsKey(currentLanguage) && allAudioClips[currentLanguage].Length > 0 && allAudioClips[currentLanguage][0].Length > 0)
        {
            PlayAudioClip(0);
        }
        else
        {
            Debug.LogWarning("No audio clips found in the specified subfolders.");
        }
    }

    void LoadAllAudioClips()
    {
        allAudioClips = new Dictionary<string, AudioClip[][]>();

        // Load clips for each language
        string[] languages = { "en", "ro" }; // Add other languages as needed

        foreach (string language in languages)
        {
            AudioClip[][] languageClips = new AudioClip[subfolderNames.Length][];
            for (int i = 0; i < subfolderNames.Length; i++)
            {
                string folderPath = $"AudioClips/{language}/{subfolderNames[i]}"; // Path to the subfolder within Resources
                languageClips[i] = Resources.LoadAll<AudioClip>(folderPath);
            }
            allAudioClips[language] = languageClips;
        }
    }

    public void PlayAudioClip(int index)
    {
        if (allAudioClips.ContainsKey(currentLanguage) && currentSubfolderIndex >= 0 && currentSubfolderIndex < allAudioClips[currentLanguage].Length)
        {
            if (index >= 0 && index < allAudioClips[currentLanguage][currentSubfolderIndex].Length)
            {
                audioSource.clip = allAudioClips[currentLanguage][currentSubfolderIndex][index];
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Index out of range of audioClips array for the current subfolder.");
            }
        }
        else
        {
            Debug.LogWarning("Current subfolder index or language not found.");
        }
    }

    public void ChangeSubfolder(int subfolderIndex)
    {
         Debug.LogWarning("subfolderIndex " + subfolderIndex);
        if(allAudioClips == null)
            return;
        if (allAudioClips.ContainsKey(currentLanguage) && subfolderIndex >= 0 && subfolderIndex < allAudioClips[currentLanguage].Length)
        {
            currentSubfolderIndex = subfolderIndex;
        }
        else
        {
            Debug.LogWarning("Subfolder index out of range or language not found.");
        }
    }

    public void ChangeLanguage(string language)
    {
        if (allAudioClips.ContainsKey(language))
        {
            currentLanguage = language;
        }
        else
        {
            Debug.LogWarning("Language not found.");
        }
    }
}
