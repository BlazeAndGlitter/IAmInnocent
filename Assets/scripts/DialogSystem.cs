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
    [TextArea] public string[] buttonTextba;
    [TextArea] public string[] buttonTextbb;
    public int suspicionrate;

    public int currentMessageIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        choice1Button.SetActive(false);
        choice2Button.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        currentMessageIndex = 0;
        dialogText.text=messages[currentMessageIndex];
        suspicionrate = 0;
        //zeige ersten Dialog
    }

    // Update is called once per frame
    void Update()
    {
        //  Cursor.lockState = CursorLockMode.None;
        //Cursor.lockState = CursorLockMode.None;
        //wenn enter pressed, 
        if (currentMessageIndex <2)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //nextDialog
                currentMessageIndex++;
                //zeige aktuellen dialog
                dialogText.text = messages[currentMessageIndex];
            }
        }
        else if (currentMessageIndex ==2) 
        {
            choice1Button.SetActive(true);
            choice1Text.text = buttonTextba[currentMessageIndex-2];
            choice2Button.SetActive(true);
            choice2Text.text = buttonTextbb[currentMessageIndex-2];

        }
        


    }
    public void ShowResult()
    {
        choice1Button.SetActive(false);
        choice2Button.SetActive(false);
        if (suspicionrate == 0)
        {
            dialogText.text = messages[5];
        }
        else if (suspicionrate < 3)
        {
            dialogText.text = messages[6];
        }
        else
        {
            dialogText.text = messages[7];
        }
    }
    public void ShowQuestion()
    {
        dialogText.text = messages[currentMessageIndex];
        choice1Button.SetActive(true);
        choice1Text.text = buttonTextba[currentMessageIndex-2];
        choice2Button.SetActive(true);
        choice2Text.text = buttonTextbb[currentMessageIndex-2];
    }
    public void choiceOne()
    {
        if (currentMessageIndex < 4)
        {
            currentMessageIndex++;
            ShowQuestion();
        }
        else
        {
            ShowResult();
            choice1Button.SetActive(false);
            choice2Button.SetActive(false);
        }
       
    }
    public void choiceTwo()
    {
        suspicionrate++;

        if (currentMessageIndex < 4)
        {
            currentMessageIndex++;
            ShowQuestion();
         

        }
        else
        {
            ShowResult();
            choice1Button.SetActive(false);
            choice2Button.SetActive(false);
        }
    }


}
