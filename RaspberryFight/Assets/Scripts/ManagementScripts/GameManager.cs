using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Character[] characters;

    public List<Character> selectedCharacters = new List<Character>();

    // Singleton GameManager instance to be used by other scripts
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    public void SetCharacter(Character character)
    {
        selectedCharacters.Add(character);
    }

}
