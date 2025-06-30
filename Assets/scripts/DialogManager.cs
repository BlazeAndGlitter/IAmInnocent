using UnityEngine;
using TMPro;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI choice1Text;
    public TextMeshProUGUI choice2Text;

    [TextArea] public string[] messages; // First two messages
    [TextArea] public string choice1Result; // Initial response to choice 1
    [TextArea] public string[] choice1ExtraMessages; // Extra lines after choice 1
    [TextArea] public string choice2Result; // Initial response to choice 2
    [TextArea] public string[] choice2ExtraMessages; // Extra lines after choice 2
    public float typingSpeed = 0.05f;

    private int currentMessageIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private bool awaitingChoice = false;
    private int selectedChoice = 0; // 0 = first, 1 = second

    private bool inChoiceExtra = false;
    private int currentExtraIndex = 0;
    private string[] currentExtraMessages;

    void Start()
    {
        dialogText.gameObject.SetActive(false);
        choice1Text.gameObject.SetActive(false);
        choice2Text.gameObject.SetActive(false);
    }

    public void StartDialog()
    {
        currentMessageIndex = 0;
        dialogText.text = "";
        dialogText.gameObject.SetActive(true);
        typingCoroutine = StartCoroutine(TypeText(messages[currentMessageIndex]));
    }

    void Update()
    {
        if (!dialogText.gameObject.activeSelf) return;

        if (awaitingChoice)
        {
            HandleChoiceInput();
            return;
        }

        if (inChoiceExtra)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (isTyping)
                {
                    StopCoroutine(typingCoroutine);
                    dialogText.text = currentExtraMessages[currentExtraIndex];
                    isTyping = false;
                }
                else
                {
                    currentExtraIndex++;
                    if (currentExtraIndex < currentExtraMessages.Length)
                    {
                        typingCoroutine = StartCoroutine(TypeText(currentExtraMessages[currentExtraIndex]));
                    }
                    else
                    {
                        dialogText.gameObject.SetActive(false); // End after extra lines
                        inChoiceExtra = false;
                    }
                }
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogText.text = messages[currentMessageIndex];
                isTyping = false;
            }
            else
            {
                currentMessageIndex++;

                if (currentMessageIndex == 2)
                {
                    ShowChoices();
                }
                else if (currentMessageIndex < messages.Length)
                {
                    typingCoroutine = StartCoroutine(TypeText(messages[currentMessageIndex]));
                }
                else
                {
                    dialogText.gameObject.SetActive(false); // End dialog
                }
            }
        }
    }

    void ShowChoices()
    {
        awaitingChoice = true;
        selectedChoice = 0;
        UpdateChoiceDisplay();

        choice1Text.gameObject.SetActive(true);
        choice2Text.gameObject.SetActive(true);
    }

    void HandleChoiceInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedChoice = 0;
            UpdateChoiceDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedChoice = 1;
            UpdateChoiceDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            awaitingChoice = false;

            choice1Text.gameObject.SetActive(false);
            choice2Text.gameObject.SetActive(false);

            if (selectedChoice == 0)
            {
                dialogText.text = "";
                typingCoroutine = StartCoroutine(TypeText(choice1Result));
                currentExtraMessages = choice1ExtraMessages;
            }
            else
            {
                dialogText.text = "";
                typingCoroutine = StartCoroutine(TypeText(choice2Result));
                currentExtraMessages = choice2ExtraMessages;
            }

            inChoiceExtra = true;
            currentExtraIndex = 0;
        }
    }

    void UpdateChoiceDisplay()
    {
        choice1Text.color = selectedChoice == 0 ? Color.yellow : Color.white;
        choice2Text.color = selectedChoice == 1 ? Color.yellow : Color.white;
    }

    IEnumerator TypeText(string message)
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char letter in message)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
