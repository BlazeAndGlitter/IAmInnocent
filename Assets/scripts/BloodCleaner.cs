using UnityEngine;

public class BloodCleaner : MonoBehaviour
{
    public float interactDistance = 5f; // Distance for raycast
    public string targetTag = "Disappearable"; // Tag for objects that can be hidden

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.collider.CompareTag(targetTag))
                {
                    hit.collider.gameObject.SetActive(false);
                }
            }
        }
    }
}
