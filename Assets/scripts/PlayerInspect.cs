using UnityEngine;
using TMPro;

public class PlayerInspect : MonoBehaviour
{
    public float inspectDistance = 3f;
    public KeyCode inspectKey = KeyCode.E;
    public TextMeshProUGUI inspectText;

    private Camera playerCamera;
    private bool textVisible = false;

    void Start()
    {
        playerCamera = Camera.main;
        inspectText.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, inspectDistance))
        {
            Inspectable inspectable = hit.collider.GetComponent<Inspectable>();
            if (inspectable != null)
            {
                if (Input.GetKeyDown(inspectKey))
                {
                    ShowInspectText(inspectable.message);
                }
                return; // Exit to avoid hiding text immediately
            }
        }

        // If not looking at inspectable or out of range
        if (textVisible)
        {
            HideInspectText();
        }
    }

    void ShowInspectText(string message)
    {
        inspectText.text = message;
        inspectText.gameObject.SetActive(true);
        textVisible = true;
    }

    void HideInspectText()
    {
        inspectText.gameObject.SetActive(false);
        textVisible = false;
    }
}
