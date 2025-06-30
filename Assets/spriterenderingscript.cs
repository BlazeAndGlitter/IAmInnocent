using UnityEngine;

public class spriterenderingscript : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        Vector3 cameraPosition
        =MainCamera.transform.position;
        cameraPosition.y
        = transform.position.y;
        transform.LookAt(cameraPosition);
        transform.Rotate(0f,180f,0f);
    }
    }

