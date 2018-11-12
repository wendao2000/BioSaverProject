using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{

    DialogueParser dp;

    [Header("Dialogue Parsing")]
    public string characterName;
    public string dialogue;
    public int nextLineNum;
    public bool isSelecting;
    string[] options;

    List<Button> buttons = new List<Button>();

    [Header("Do Not Change, Unless Necessary")]
    public Text nameBox;
    public Text dialogueBox;
    public GameObject choiceBox;


    void Awake()
    {
        dp = FindObjectOfType<DialogueParser>();

        characterName = "";
        dialogue = "";
        nextLineNum = 0;
        isSelecting = false;
        options = new string[0];
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateUI();
        if (Input.GetButtonDown("Interact") && !isSelecting)
        {
            if (nextLineNum == -1)
            {
                print("Exit Dialogue Box!");
                //FindObjectOfType<GameManager>().CloseDialogueBox();
            }

            else
            {
                ParseDialogue();
            }
        }
    }

    public void ParseDialogue()
    {
        if (dp.Check(nextLineNum) == true)
        {
            characterName = "";
            dialogue = "";
            options = dp.GetOptions(nextLineNum);

            CreateButtons();
            isSelecting = true;
        }

        else
        {
            characterName = dp.GetName(nextLineNum);
            dialogue = dp.GetContent(nextLineNum);

            DestroyButtons();
            isSelecting = false;
        }

        nextLineNum = dp.GetNextLine(nextLineNum);
    }

    void UpdateUI()
    {
        nameBox.text = characterName;
        dialogueBox.text = dialogue;
    }

    void CreateButtons()
    {
        for (int i = 0; i < options.Length; i++)
        {
            GameObject button = Instantiate(choiceBox);
            Button b = button.GetComponent<Button>();
            //ControlButton cb = button.GetComponent<ControlButton>();

            //cb.SetText(options[i].Split(',')[0]);
            //cb.destLine = int.Parse(options[i].Split(',')[1]);

            b.transform.SetParent(FindObjectOfType<DialogueManager>().transform);
            b.transform.localPosition = new Vector3(0, -25 + (i * 50));
            b.transform.localScale = new Vector3(1, 1, 1);
            buttons.Add(b);
        }
    }

    void DestroyButtons()
    {
        while (buttons.Count > 0)
        {
            Button b = buttons[0];
            buttons.Remove(b);
            Destroy(b.gameObject);
        }
    }
}
