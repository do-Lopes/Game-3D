using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptCamera : MonoBehaviour
{
    private Quaternion rotIniX;
    public float velRotX;
    private float countX;
    // Start is called before the first frame update
    void Start()
    {
        velRotX = 40;
        rotIniX = transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        countX += Input.GetAxisRaw("Mouse Y") * velRotX * Time.deltaTime;

        countX = Mathf.Clamp(countX, -60, 60);

        Quaternion rotX = Quaternion.AngleAxis(countX, Vector3.left);

        transform.localRotation = rotIniX * rotX;
    }
}
