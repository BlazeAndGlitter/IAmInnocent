using UnityEngine;
using UnityEngine.UI;


public class HideEvidence : MonoBehaviour
{
    public float pickupRange = 3f;
    public Transform holdParent;
    public float holdSmoothness = 10f;
    public float interactionRange = 1.5f;


    [Header("UI")]
    public GameObject interactionHintUI;


    [Header("Game State")]
    public bool knifeDroppedOnShoe = false;
    public bool corpseLaunched = false;


    private GameObject heldObject = null;
    private Rigidbody heldRB = null;


    private GameObject potentialTarget = null;
    private bool isBalloon = false;
    private bool canActivate = false;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
                TryPickup();
            else
                DropObject();
        }


        if (heldObject != null)
        {
            MoveObject();


            if (isBalloon)
            {
                CheckProximityForAttachment();


                if (interactionHintUI != null)
                    interactionHintUI.SetActive(canActivate);


                if (canActivate && Input.GetKeyDown(KeyCode.E))
                {
                    if (interactionHintUI != null)
                        interactionHintUI.SetActive(false);


                    StartCoroutine(DockAndLaunch(heldObject, potentialTarget));
                }
            }
        }
        else
        {
            if (interactionHintUI != null)
                interactionHintUI.SetActive(false);
        }
    }


    void TryPickup()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            GameObject target = hit.transform.gameObject;


            if (target.CompareTag("Evidence") && target.TryGetComponent<Rigidbody>(out Rigidbody rb) && !rb.isKinematic)
            {
                heldObject = target;
                heldRB = rb;


                heldRB.useGravity = false;
                heldRB.linearDamping = 10;
                heldRB.constraints = RigidbodyConstraints.FreezeRotation;


                isBalloon = target.name.ToLower().Contains("ballon") || target.name.ToLower().Contains("balloon");
            }
        }
    }


    void MoveObject()
    {
        Vector3 targetPosition = holdParent.position;
        Vector3 moveDirection = (targetPosition - heldObject.transform.position);
        heldRB.linearVelocity = moveDirection * holdSmoothness;
    }


    void DropObject()
    {
        bool wasKnife = heldObject.name.ToLower().Contains("messer") || heldObject.name.ToLower().Contains("knife");
        Vector3 dropPosition = heldObject.transform.position;


        heldRB.useGravity = true;
        heldRB.linearDamping = 0;
        heldRB.constraints = RigidbodyConstraints.None;


        // Prüfen, ob auf einem Schuh gedroppt wurde
        Collider[] colliders = Physics.OverlapSphere(dropPosition, 0.3f);
        foreach (var col in colliders)
        {
            if (col.CompareTag("Evidence") && col.gameObject != heldObject)
            {
                bool isShoe = col.gameObject.name.ToLower().Contains("schuh") || col.gameObject.name.ToLower().Contains("shoe");


                if (wasKnife && isShoe)
                {
                    Debug.Log("Messer auf Schuh gedroppt!");
                    knifeDroppedOnShoe = true;
                }
            }
        }


        heldObject = null;
        heldRB = null;
        potentialTarget = null;
        canActivate = false;
        isBalloon = false;


        if (interactionHintUI != null)
            interactionHintUI.SetActive(false);
    }


    void CheckProximityForAttachment()
    {
        Collider[] nearby = Physics.OverlapSphere(heldObject.transform.position, interactionRange);
        foreach (var col in nearby)
        {
            if (col.CompareTag("Evidence") && col.gameObject != heldObject)
            {
                potentialTarget = col.gameObject;
                canActivate = true;
                return;
            }
        }
        canActivate = false;
        potentialTarget = null;
    }


    System.Collections.IEnumerator DockAndLaunch(GameObject balloon, GameObject target)
    {
        // Ballon positionieren
        Vector3 dockOffset = Vector3.up * 0.5f;
        balloon.transform.position = target.transform.position + dockOffset;


        DropObject();


        Rigidbody balloonRB = balloon.GetComponent<Rigidbody>();
        Rigidbody targetRB = target.GetComponent<Rigidbody>();


        if (targetRB == null)
            targetRB = target.AddComponent<Rigidbody>();


        balloonRB.useGravity = false;
        targetRB.useGravity = false;


        // ✅ Zustand speichern
        corpseLaunched = true;


        yield return new WaitForSeconds(0.3f);


        while (true)
        {
            Vector3 upward = Vector3.up * Time.deltaTime * 1.0f;
            balloonRB.MovePosition(balloonRB.position + upward);
            targetRB.MovePosition(targetRB.position + upward);
            yield return null;
        }
    }
}
