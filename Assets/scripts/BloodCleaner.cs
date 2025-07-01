using UnityEngine;
using TMPro; // WICHTIG für TextMeshPro

public class BloodCleaner : MonoBehaviour
{
    public float interactDistance = 5f;
    public string bloodTag = "Disappearable";
    public string broomTag = "Broom";

    public TextMeshProUGUI infoText;   // TMP Textfeld
    public float messageDuration = 2f; // Dauer der Anzeige

    private bool hasBroom = false;
    private float messageTimer = 0f;

    void Update()
    {
        // Besen aufheben
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.collider.CompareTag(broomTag))
                {
                    hasBroom = true;
                    Destroy(hit.collider.gameObject);
                    ShowMessage("I picked up the vacuum cleaner.");
                }
            }
        }

        // Blut aufwischen
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!hasBroom)
            {
                ShowMessage("I need to find a cleaning device first to clean this.");
                return;
            }

            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.collider.CompareTag(bloodTag))
                {
                    hit.collider.gameObject.SetActive(false);
                    ShowMessage("WRRRRRR");
                }
            }
        }

        // Timer für Nachrichtenanzeige
        if (infoText != null && messageTimer > 0)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0)
            {
                infoText.text = "";
            }
        }
    }

    void ShowMessage(string message)
    {
        if (infoText != null)
        {
            infoText.text = message;
            messageTimer = messageDuration;
        }
    }
}
