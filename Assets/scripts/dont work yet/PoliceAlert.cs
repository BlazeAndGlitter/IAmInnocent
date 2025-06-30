using UnityEngine;
using UnityEngine.UI;

public class PoliceAlert : MonoBehaviour
{
    public AudioSource policeSiren;
    public Text alertText;

    public float firstDelay = 5f;  // Time before showing text
    public float secondDelay = 10f; // Time before second siren

    void Start()
    {
        alertText.gameObject.SetActive(false);
        StartCoroutine(HandlePoliceAlert());
    }

    private System.Collections.IEnumerator HandlePoliceAlert()
    {
        // Play the first siren
        policeSiren.Play();

        // Wait for the first delay
        yield return new WaitForSeconds(firstDelay);

        // Show the alert text
        alertText.gameObject.SetActive(true);

        // Wait for the second delay
        yield return new WaitForSeconds(secondDelay);

        // Play the siren again
        policeSiren.Play();
    }
}
