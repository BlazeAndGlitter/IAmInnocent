using UnityEngine;
using TMPro;
using System.Collections;

public class IntroTextController : MonoBehaviour
{
    public TextMeshProUGUI introText;
    [TextArea] public string[] messages; // Array of messages
    public float typingSpeed = 0.05f;

    private int currentMessageIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        if (introText != null && messages.Length > 0)
        {
            introText.text = "";
            introText.gameObject.SetActive(true);
            typingCoroutine = StartCoroutine(TypeText(messages[currentMessageIndex]));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                // Finish typing immediately
                StopCoroutine(typingCoroutine);
                introText.text = messages[currentMessageIndex];
                isTyping = false;
            }
            else
            {
                currentMessageIndex++;

                if (currentMessageIndex < messages.Length)
                {
                    typingCoroutine = StartCoroutine(TypeText(messages[currentMessageIndex]));
                }
                else
                {
                    introText.gameObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator TypeText(string message)
    {
        isTyping = true;
        introText.text = "";

        foreach (char letter in message.ToCharArray())
        {
            introText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
