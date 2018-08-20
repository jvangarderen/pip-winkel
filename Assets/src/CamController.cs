using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float rotSpeed;
    private bool mouseDown = false;
    private bool skipmousecheck = true;

    private float lastX, lastY;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) skipmousecheck = !skipmousecheck;
        if (!skipmousecheck && Input.GetMouseButton(1) == false && Input.GetMouseButton(0) == false) mouseDown = false;
        else mouseDown = true;
        if (!mouseDown)
        {
            //transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            eulerAngles += new Vector3(0, rotSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.Euler(eulerAngles);
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                float difX = Input.mousePosition.x - lastX;
                float difY = Input.mousePosition.y - lastY;
                transform.Rotate(Vector3.down * difX * Time.deltaTime);
                //  transform.Rotate(Vector3.right * difY * Time.deltaTime);
            }
        }

        lastX = Input.mousePosition.x;
        lastY = Input.mousePosition.y;
    }
}
