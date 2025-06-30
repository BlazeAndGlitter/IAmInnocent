using UnityEngine;
using TMPro;
using System.Collections;

public class DialogSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI choice1Text;
    public string choice1TextText;
    public TextMeshProUGUI choice2Text;
    public string choice2TextText;
    public GameObject choice1Button;
    public GameObject choice2Button;

    [TextArea] public string[] messages; // First two messages
    [TextArea] public string choice1Result; // Initial response to choice 1
    [TextArea] public string[] choice1ExtraMessages; // Extra lines after choice 1
    [TextArea] public string choice2Result; // Initial response to choice 2
    [TextArea] public string[] choice2ExtraMessages; // Extra lines after choice 2
    public int currentMessageIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        choice1Button.SetActive(false);
        choice2Button.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        currentMessageIndex = 0;
        dialogText.text=messages[currentMessageIndex];
        //zeige ersten Dialog
    }

    // Update is called once per frame
    void Update()
    {
        //  Cursor.lockState = CursorLockMode.None;
        //Cursor.lockState = CursorLockMode.None;
        //wenn enter pressed, 
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //nextDialog
            currentMessageIndex++;
            //zeige aktuellen dialog
            dialogText.text = messages[currentMessageIndex];
        }
        if (currentMessageIndex == 1)
        {
            choice1Button.SetActive(true);
            choice1Text.text = choice1TextText;
            choice2Button.SetActive(true);
            choice2Text.text = choice2TextText;
        }


    }
    public void choiceOne()
    {
        dialogText.text = choice1Result;
        
    }
    public void choiceTwo()
    {
        dialogText.text = choice2Result;
    }
}
