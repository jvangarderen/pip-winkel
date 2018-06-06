using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private SteamVR_TrackedController _controller;
    private PrimitiveType _currentPrimitiveType = PrimitiveType.Sphere;
    SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;
    Vector2 touchpad;
    private GameObject interactiveobj;
    private SteamVR_LoadLevel loader;
    public bool triggerButtonDown = false;


    //Marlies back stuff
    public float TimeToBack;
    public bool startedBacking = false;
    public float countdown;
    public AudioSource hlsound;
    private bool hlsoundplayed = false;
    private Manager manager;

    private void OnEnable()
    {
        _controller = GetComponent<SteamVR_TrackedController>();
        _controller.TriggerClicked += HandleTriggerClicked;
        
    }

    private void OnDisable()
    {
        _controller.TriggerClicked -= HandleTriggerClicked;
    }
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        controller = gameObject.GetComponent<SteamVR_TrackedObject>();
    }


    void Update()
    {
        //If finger is on touchpad
        /*
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            
            //Read the touchpad values;
            touchpad = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
            Debug.Log(touchpad.x);
            if (touchpad.x < 0) manager.RotateGarmentLeft();
            if (touchpad.x > 0) manager.RotateGarmentRight();
            // Handle movement via touchpad

        }*/
    }

    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject other = hit.collider.gameObject;
            if (other.tag == "popup")
            {
                manager.OpenPopup(hit.transform);
            }
            if (other.name == "btn_close")
            {
                manager.ClosePopup();
            }
        }
    }
}