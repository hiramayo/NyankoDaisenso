using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDirector : MonoBehaviour
{
    static List<CharacterData> characters;
    static Dictionary<string, CharacterData> characterData; 

    // Start is called before the first frame update
    void Start()
    {
        characterData = LoadCharacterData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO stab
    public static List<CharacterData> GetOrganizedCharacters()
    {
        if (characterData == null) characterData = LoadCharacterData(); 
        if(characters == null)
        {
            characters = new List<CharacterData>();
            characters.Add(characterData["cat"]);
            characters.Add(characterData["tank"]);

        }
        return characters;

    }
    public static Dictionary<string, CharacterData> LoadCharacterData()
    {

        Dictionary<string, CharacterData> data = new Dictionary<string, CharacterData>();
        string id = "cat";
        data.Add(id, new CharacterData()
        {
            Level = 1

        });
        id = "tank";
        data.Add(id, new CharacterData()
        {
            Level = 1
        });

        id = "battle_cat";
        data.Add(id, new CharacterData()
        {
            Level = 1
        });
        id = "kimo_cat";
        data.Add(id, new CharacterData()
        {
            Level = 1
        });
    
        return data; 
	 
    } 


}
