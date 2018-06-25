using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public GameObject selectedOBJ;




    private SteamVR_TrackedController _controller;
    private PrimitiveType _currentPrimitiveType = PrimitiveType.Sphere;
    SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;
    Vector2 touchpad;
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
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (selectedOBJ.name.Contains("stripe"))
            {
                ChangeStripes CS = selectedOBJ.GetComponent<ChangeStripes>();
                CS.ToggleSelected();
            }
        }
        if (selectedOBJ != null)
        {
            ChangeStripes CS = selectedOBJ.GetComponent<ChangeStripes>();
            if (Input.GetKeyUp(KeyCode.Alpha1)) CS.PrevMat();
            if (Input.GetKeyUp(KeyCode.Alpha2)) CS.NextMat();
        }
	}

    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GameObject hittedOBJ = hit.collider.gameObject;


            //als zelfde stripe is disable
            if (hittedOBJ == selectedOBJ)
            {
                if (hittedOBJ.GetComponent<ChangeStripes>() != null)
                {
                    ChangeStripes cs = selectedOBJ.GetComponent<ChangeStripes>();
                    cs.ToggleSelected();
                    selectedOBJ = null;
                }
            }
            else
            {
                //als oude stripe is en nieuwe niet 
                if (hittedOBJ.GetComponent<ChangeStripes>() == null && selectedOBJ.GetComponent<ChangeStripes>() != null)
                {
                    ChangeStripes cs = selectedOBJ.GetComponent<ChangeStripes>();
                    cs.ToggleSelected();
                    selectedOBJ = hittedOBJ;
                }


                // als beide andere stripe is
                if (hittedOBJ.GetComponent<ChangeStripes>() != null && selectedOBJ.GetComponent<ChangeStripes>() != null)
                {
                    ChangeStripes cs = selectedOBJ.GetComponent<ChangeStripes>();
                    cs.ToggleSelected();
                    cs = hittedOBJ.GetComponent<ChangeStripes>();
                    cs.ToggleSelected();
                    selectedOBJ = hittedOBJ;
                }

                //nieuwe stripe oude niks
                if (hittedOBJ.GetComponent<ChangeStripes>() != null)
                {
                    ChangeStripes cs = selectedOBJ.GetComponent<ChangeStripes>();
                    cs.ToggleSelected();
                    selectedOBJ = hittedOBJ;
                }
            }
        }
    }
}
