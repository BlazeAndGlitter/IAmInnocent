using UnityEngine;
using TMPro;

public class CountdownTimer3D : MonoBehaviour
{
    public float timeRemaining = 120f; // seconds (2 minutes)
    public TextMeshPro counterText;
    public bool countingDown = true;

    void Update()
    {
        if (countingDown && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
                timeRemaining = 0;
        }

        // Convert seconds → mm:ss format
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        counterText.text = $"{minutes:00}:{seconds:00}";
    }
}
