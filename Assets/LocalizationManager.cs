using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Dialogue
{
    public string[][] text_1;
    public Dialogue(string[][] text1)
    {
        text_1 = text1;
    }
}
public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    private Dictionary<string, Dialogue> localizedTexts;

    void Awake()
    {
        // Ensure this is the only instance of LocalizationManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLocalizedTexts();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadLocalizedTexts()
    {
        // Initialize the dictionary
        localizedTexts = new Dictionary<string, Dialogue>();

        // Load English phrases
        string[][] englishText1 = new string[][]
        {
            new string[] {
                "Welcome to the Ship of Theseus Paradox!",
                "In the ancient port city of Athens, a magnificent ship rests anchored at the dock. This is the legendary ship of Theseus, the hero who once sailed to slay the Minotaur. Over the years, the ship has undergone numerous repairs. Every plank, sail, and nail has been replaced at some point. Yet, it remains a cherished symbol of Athenian pride and heroism.",

                "To understand what is the paradox about let\'s play a game. Click on the screen to start.",
                "If we take a part of the ship, is this the same ship?",
                "Let\'s take another part. Is this the same ship?",
                "Keep taking parts.",

                "well, now we removed the whole ship, so we've gotten too far. But when we should stop for the ship to be the same?",

                "let\'s try another approach.",

                "click on the ship to remove the upper part. Is this the same ship, if we remove half of it?",
                "okay, now let\'s replace piece by piece the ship and see when the ship becomes not the original one.",

                "now, we have only the flag as the original part of the Ship of Theseus, is this enough this to call it the ship of theseus or is this another ship?",

                "All these questions make this the paradox of the Theseus\'s Ship.",

                "The Theseus Paradox illustrates how identity persists through change, mirroring our own lives. Just as the Ship of Theseus remains the same ship despite all its parts being replaced, we retain our identity through constant physical, mental, and experiential transformations. Our sense of self is rooted in a continuous narrative shaped by our memories, relationships, and purposes, demonstrating that identity is more than just the sum of our parts; it is the enduring essence that persists through all changes.",
            },

        };
        localizedTexts["en"] = new Dialogue(englishText1);
        // Load Romanian phrases
        string[][] romanianText1 = new string[][]
        {
            new string[] {
                "Bine ați venit la paradoxul corabiei lui Theseus!",
                "În vechiul oraș portuar Atena, o corabie magnifică stă ancorată la doc. Aceasta este nava legendară a lui Theseus, eroul care a navigat cândva pentru a-l ucide pe Minotaur. De-a lungul anilor, nava a suferit numeroase reparații.",
                "Fiecare scândură, velă și cui a fost înlocuit la un moment dat. Cu toate acestea, rămâne un simbol prețios al mândriei și eroismului atenian.",
                "Pentru a înțelege în ce constă paradoxul, haideți să jucăm un joc. Faceți click pe ecran pentru a începe.",
            },
            new string[] {
                 "Dacă luăm o piesă din navă. este ea aceeași navă?",
                "Să luăm o altă piesă. Este aceeași navă?",
                "Continuați să luați piese.",
                "Ei bine, acum am eliminat întreaga navă, deci am ajuns prea departe. Dar, când ar trebui să ne oprim pentru ca nava să fie la fel?",
                "Să încercăm o altă abordare.",



            },
             new string[] {
                "Faceți click pe navă pentru a elimina partea superioară. Este aceeași navă, dacă îndepărtăm jumătate din ea?",
            },
            new string[] {
                "Bine, acum să înlocuim nava bucată cu bucată și să vedem când nava devine alta decât cea originală.",
                "Acum, avem doar steagul ca parte originală a navei lui Theseus, este suficient pentru a o numi nava lui Theseu sau este o altă navă?",
            },
            new string[] {
                "Toate aceste întrebări crează paradoxul corabiei lui Theseus.",
                "Paradoxul lui Theseus ilustrează modul în care identitatea persistă prin schimbare, reflectând propriile noastre vieți.",
                "La fel cum corabia lui Theseus rămâne aceeași în ciuda înlocuirii tuturor pieselor sale, noi ne păstrăm identitatea prin transformări fizice, mentale și de experiență constante.",
                "Simțul nostru de sine este înrădăcinat într-o narațiune continuă modelată de amintirile, relațiile și scopurile noastre, demonstrând că identitatea este mai mult decât suma părților noastre; este esența durabilă care persistă prin toate schimbările.",
            }
        };
        localizedTexts["ro"] = new Dialogue(romanianText1);

    }
    public Dialogue GetLocalizedText(string languageCode)
    {
        if (localizedTexts.ContainsKey(languageCode))
        {
            return localizedTexts[languageCode];
        }
        else
        {
            Debug.LogWarning("Localized text not found for language: " + languageCode);
            return null;
        }
    }
}
