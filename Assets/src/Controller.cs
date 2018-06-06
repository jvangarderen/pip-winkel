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
        try
        {
            loader = GameObject.Find("SceneLoader").GetComponent<SteamVR_LoadLevel>();
        }
        catch(System.Exception e)
        {
            Debug.Log("add sceneloader to the scene");
        }
    }

    void handleHiglight()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // Debug.Log("[Controller.Update] found object " + hit.collider.gameObject.name);
        }
        else
        {
            hlsoundplayed = false;
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject other = hit.collider.gameObject;
        }
    }

    void handleMarlies()
    {
        device = SteamVR_Controller.Input((int)controller.index);
        string currentScene = ""; //SceneManagerHelper.ActiveSceneName;
        if (currentScene == "Marlies Cube 1" || currentScene == "Marlies Cube 2")
        {
            if (_controller.triggerPressed)
            {
                if (!startedBacking)
                {
                    Debug.Log("Doe back shizzle in marlies scene");
                    startedBacking = true;
                    countdown = TimeToBack;
                }
                else
                {
                    countdown -= Time.deltaTime;
                    if (countdown <= 0)
                    {
                        Application.LoadLevel("Marlies");
                    }
                }
            }
            else
            {
                startedBacking = false;
            }
        }
    }

    void Update()
    {

        handleHiglight();
        handleMarlies();
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.blue);

        if (interactiveobj != null)
        {
            //If finger is on touchpad
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
            {
                //Read the touchpad values;
                touchpad = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

                // Handle movement via touchpad
               
            }
        }
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
/*
  * using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

   // public GameObject selectedOBJ;




    private SteamVR_TrackedController _controller;
    private PrimitiveType _currentPrimitiveType = PrimitiveType.Sphere;
    SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;
    Vector2 touchpad;
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



	// Use this for initialization
	void Start () {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
	}/*
	
	// Update is called once per frame
	void Update () {
        Debug.Log(_controller.triggerPressed);
        if (_controller.triggerPressed)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject other = hit.collider.gameObject;
                if (other.tag == "popup")
                {
                    Debug.Log("popuphitted");
                    manager.OpenPopup(hit.transform);
                }
            }
        }
	}

    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        
        //manager.checkRay(ray,true);
    }
}
    */