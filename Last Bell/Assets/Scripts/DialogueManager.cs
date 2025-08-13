using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    //public TextMeshProUGUI charName;
    public TextMeshProUGUI charDialogue;
    public GameObject continueBtn;

    public UnityEvent dialogueEndEvent;

    private Queue<DialogueLine> lines;
    private string prevName;

    public GameObject choicePanel;
    public Button[] choiceButtons;

    public bool isDialogueActive = false;
    public float typingSpeed;
    public string currentText;

    public DialogueLine currentLine;

    /// <summary>
    /// /////
    /// </summary>
    //public GameObject[] textBoxPrefabs;
    //public GameObject textBoxPanel;
    //public Scrollbar dialogueScrollbar;

    public TMP_FontAsset nameFont;
    public TMP_FontAsset messageFont;

    //public List<GameObject> activeLeftTextList, inactiveLeftTextList, activeRightTextList, 
    //    inactiveRightTextList, activeMiddleTextList, inactiveMiddleTextList;

    //public LayerMask canNextDialogue;
    //Camera cam;
    //public float lineWidth;

    //private bool castRays = true;
    // Start is called before the first frame update


    public enum TextType
    {
        Name,
        MyDialogue,
        OtherDialogue,
        NarratorDialogue,
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
    }
    void Start()
    {
        lines = new Queue<DialogueLine>();
        //cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CheckNextDialogueClick();
        //if(castRays)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        //    {
        //        if(hit.transform.gameObject.layer == canNextDialogue)
        //        {

        //        }
        //    }
        //}
        //Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, canNextDialogue);

    }

    public void CheckNextDialogueClick()
    {
        if (isDialogueActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (charDialogue.text == currentLine.line)
                {

                    currentLine.onEndLineEvent.Invoke();

                    if (currentLine.choices.Count != 0)
                    {
                        DisplayChoices(currentLine.choices);
                        //dialogueScrollbar.value = 0;
                    }
                    else
                    {
                        //dialogueScrollbar.value = 0;
                        DisplayNextDialogueLine();
                    }

                }

                else
                {
                    StopAllCoroutines();
                    charDialogue.text = currentLine.line;
                    continueBtn.SetActive(true);
                    //RefreshContentSize();
                    if (currentLine.choices.Count != 0)
                    {
                        DisplayChoices(currentLine.choices);
                    }
                    //dialogueScrollbar.value = 0;

                }
            }
        }
    }

    public void StartDialogue(Dialogue1 dialogue, UnityEvent dialogueEvent)
    {
        if (dialogue.dialogueLines.Count != 0)
        {
            dialoguePanel.SetActive(true);
            isDialogueActive = true;

            lines.Clear();

            foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
            {
                lines.Enqueue(dialogueLine);
            }

            dialogueEndEvent = dialogueEvent;

            //if (dialogue.dialogueSpeakerImage != null)
            //{
            //    charIcon.gameObject.SetActive(true);
            //    charName.gameObject.SetActive(true);
            //    charIcon.sprite = dialogue.dialogueSpeakerImage;
            //    charName.text = dialogue.dialogueSpeaker;
            //}
            //else
            //{
            //    charName.gameObject.SetActive(false);
            //    charIcon.gameObject.SetActive(false);
            //}

            DisplayNextDialogueLine();
        }
        else
        {
            EndDialogue();
        }
    }

    public void DisplayNextDialogueLine()
    {
        continueBtn.SetActive(false);
        //checks if end of dialogue ands has no choices
        if (lines.Count == 0 && currentLine.hasChoice == false)
        {
            EndDialogue();
            return;
        }

        //currentText = lines.Peek().line;
        if (lines.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            currentLine = lines.Dequeue();
        }


        //charName.text = currentLine.character.name;
        //charIcon.sprite = currentLine.character.icon;
        //InstantiateText(TextType.Name, currentLine.character.name);
        //prevName = currentLine.character.name;    

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        charDialogue.text = "";

        foreach (char c in dialogueLine.line.ToCharArray())
        {

            charDialogue.text += c;
            //charDialogue.GetComponentInParent<ContentSizeFitter>().SetLayoutHorizontal();
            //dialogueScrollbar.value = 0;
            if (dialogueLine.line == charDialogue.text)
            {
                continueBtn.SetActive(true);
            }
            yield return new WaitForSeconds(typingSpeed);
        }

    }
    void EndDialogue()
    {
        isDialogueActive = false;
        StopAllCoroutines();
        //ClearPool();
        prevName = null;
        dialoguePanel.gameObject.SetActive(false);

        dialogueEndEvent.Invoke();
    }

    public void DisplayChoices(List<Dialogue1> choices)
    {
        choicePanel.SetActive(true);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }
        //choicePanel.transform.SetSiblingIndex(textBoxPanel.transform.childCount - 1);
        for (int i = 0; i < choices.Count; i++)
        {
            choiceButtons[i].gameObject.SetActive(true);
            //choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i].dialogueName;
            choiceButtons[i].onClick.RemoveAllListeners();
            int index = i; // Capture the current index
            choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choices[index]));
            choiceButtons[i].gameObject.SetActive(true);
        }

        dialoguePanel.SetActive(false);
    }

    void OnChoiceSelected(Dialogue1 dialogue)
    {
        StartDialogue(dialogue, dialogue.dialogueEndEvent);
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
        choicePanel.SetActive(false);
        //InstantiateText(TextType.Name, "d");
    }

    
}
