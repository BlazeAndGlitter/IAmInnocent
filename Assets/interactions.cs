using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class interactions : MonoBehaviour
{
    public float interactDistance = 5f;
   

    public TextMeshProUGUI infoText;   // TMP Textfeld
    public float messageDuration = 2f; // Dauer der Anzeige

   // public bool hasBloodsucker = false;
    private float messageTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                //hasBroom = true;
                if (hit.collider.CompareTag("Telefone"))
                {

                    //  Destroy(hit.collider.gameObject);
                    ShowMessage("police called");
                    SceneManager.LoadScene("interrogationSzene");
                }

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
