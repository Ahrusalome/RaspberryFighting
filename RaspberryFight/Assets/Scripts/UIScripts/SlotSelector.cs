using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SlotSelector : MonoBehaviour
{
    private RectTransform navigator;

    private GameManager gameManager;

    private bool isActive = true;
    private bool isStarted = false;
    public Image img;

    public TextMeshProUGUI text;

    void Start()
    {
        navigator = GetComponent<RectTransform>();
        gameManager = GameManager.instance;
    }

    int navPos = 0;
    Vector2 movement = Vector2.zero;
    public RectTransform[] slots = new RectTransform[4];

    void OnMove(InputValue val)
    {
        if (gameObject.name == "Mouse P1" && !isStarted)
        {
            Destroy(text);
            isStarted = true;
        }
        if (gameObject.name == "Mouse P2" && !isStarted)
        {
            Destroy(text);
            isStarted = true;
        }

        if (isActive)
        {
            movement = val.Get<Vector2>();
            if (movement.x > 0)
            {
                MoveNav(1);
            }
            if (movement.x < 0)
            {
                MoveNav(-1);
            }
        }
        img.sprite = GameManager.instance.characters[navPos].icon;
        img.enabled = true;
    }

    void MoveNav(int change)
    {
        if (change > 0)
        {
            if (navPos + change < slots.Length - 1)
            {
                navPos += change;
            }
            else
            {
                navPos = slots.Length - 1;
            }
        }
        else
        {
            if (navPos + change >= 0)
            {
                navPos += change;
            }
            else
            {
                navPos = 0;
            }
        }
        navigator.position = slots[navPos].position;
    }

    void OnValid()
    {
        if (isActive)
        {
            gameManager.SetCharacter(GameManager.instance.characters[navPos]);
        }
        isActive = false;
    }

    // void Update()
    // {
    //     if (gameManager.selectedCharacters.Count == 2)
    //     {
    //         SceneManager.LoadSceneAsync("FightScene");
    //     }
    // }
}