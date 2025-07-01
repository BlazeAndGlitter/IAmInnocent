using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float totalTime = 300f; // 5 minutes
   // public DialogManager dialogManager; // Reference to dialog script

    public float remainingTime;
    public bool timerFinished = false;

    void Start()
    {
        remainingTime = totalTime;
        timerFinished = false;
    }

    void Update()
    {
        if (!timerFinished)
        {
            print("test");
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timerFinished = true;
                timerText.text = "00:00";
                /*if (dialogManager != null)
                {
                    dialogManager.StartDialog();
                }*/
                //change scene
                SceneManager.LoadScene("interrogationSzene");

            }
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
