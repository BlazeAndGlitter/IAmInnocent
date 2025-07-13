using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    [SerializeField] private float _rotationAmp;
    [SerializeField] private float _rotationFreq;
    private Vector3 _startPos;
    private Vector3 _startRotation;

    [SerializeField, HideInInspector] private bool x;
    [SerializeField, HideInInspector] private bool y;
    [SerializeField, HideInInspector] private bool z;

    private void Start()
    {
        _startPos = transform.position;
        _startRotation = transform.localEulerAngles;
        Debug.Log(gameObject.name + " is at: " + _startPos);
    }
    private void Update()
    {
        transform.position = new Vector3(_startPos.x, _startPos.y + Mathf.Sin(Time.time * _frequency) * _amplitude, _startPos.z);

        Vector3 rotation = transform.localEulerAngles;

        //Checked um welche Axe das Object rotiert werden soll
        //Und rotiert das Objekt dann um diese
        if (x)
        {
            rotation.x = _startRotation.x + Mathf.Sin(Time.time * _rotationFreq) * _rotationAmp;
        }
        if (y)
        {
            rotation.y = _startRotation.y + Mathf.Sin(Time.time * _rotationFreq) * _rotationAmp;
        }
        if (z)
        {
            rotation.z = _startRotation.z + Mathf.Sin(Time.time * _rotationFreq) * _rotationAmp;
        }
        transform.localEulerAngles = rotation;
    }
}
