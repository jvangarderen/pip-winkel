using System.Collections;
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
	}
	
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
