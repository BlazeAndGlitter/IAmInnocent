using UnityEngine;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class interactable : MonoBehaviour
{
    
    // public TextMeshProUGUI TextName;
    // public TextMeshProUGUI TextDescription;
    // public GameObject Textpannel;
   // public GameObject Handobject;

    public bool shoeInRange;
    public GameObject Player;
    public int scene = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //&& SelectionManager.Instance.onTarget
        if (Input.GetKeyDown(KeyCode.Mouse0) && shoeInRange)
        {
            print("shoe");
            

        }



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shoe"))
        {
            shoeInRange = true;
            HideEvidence.Instance.KnifeOnShoe = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shoe"))
        {
            shoeInRange = false;
            HideEvidence.Instance.KnifeOnShoe = false;
        }

    }
   
}
