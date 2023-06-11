using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;


public class CharacterSelectionUI : MonoBehaviour
{
    public GameObject optionPrefab;
    public Transform prevCharacter;
    public Transform selectedCharacter;
    public GameObject textIndicator;
    private void Start()
    {
        textIndicator = GameObject.Find("Indicator");
        foreach (Character c in GameManager.instance.characters)
        {
            GameObject option = Instantiate(optionPrefab, transform);
            Button button = option.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                if (GameManager.instance.selectedCharacters.Count < 2)
                {
                    GameManager.instance.SetCharacter(c);
                }
                if (selectedCharacter != null)
                {
                    prevCharacter = selectedCharacter;
                }
                selectedCharacter = option.transform;
            });
            Text text = option.GetComponentInChildren<Text>();
            text.text = c.name;
            Image image = option.GetComponentInChildren<Image>();
            image.sprite = c.icon;
        }
    }
    void Update()
    {
        if (GameManager.instance.selectedCharacters.Count == 1)
        {
            textIndicator.GetComponent<Text>().text = "Player 2 choose his fighter !";
        }
        if (GameManager.instance.selectedCharacters.Count == 2)
        {
            textIndicator.GetComponent<Text>().text = "Click the button to launch the game !";
        }
        if (selectedCharacter != null)
        {
            selectedCharacter.localScale = Vector3.Lerp(selectedCharacter.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 10);
        }
        if (prevCharacter != null)
        {
            prevCharacter.localScale = Vector3.Lerp(prevCharacter.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10);
        }
    }
}
