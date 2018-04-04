﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour 
{
    public Light light;
    public List<GameObject> shoppingList;
    public CamController camController;
    public GameObject garmentDisplay;
    public float garmentDisplayRotSpeed;
    public GameObject curSelectedGarment;
    public List<GameObject> allGarments;
    private string lasthittedpopupname;
    public GameObject popUP;
    public Dropdown dropdown;
    public GameObject ColorOptionsStartPoint;
    List<string> names = new List<string>();
    List<GameObject> items;

    GameObject gPiece;
    ChangeStripes curStripe;
	// Use this for initialization
	void Start () {
	}

    public void DropDown_IndexChanged(int index)
    {
        foreach (Transform child in ColorOptionsStartPoint.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if (index >= 0)
        {
            gPiece = items[index];
            curStripe = gPiece.GetComponent<ChangeStripes>();
            Debug.Log(items[index].name + ", Nummer of materials:"+curStripe.myMat.Count);
            float maxCollums = 2;
            float curCollum = 0;
            float curRow = 0;
            for (int i = 0; i < curStripe.myMat.Count; i++)
            {
                Vector3 ofset;
                if (curCollum < maxCollums)
                {
                    ofset = new Vector3(1 + (curCollum * 2), 1+-(2*curRow), 0);
                    curCollum++;
                }
                else
                {
                    ofset = new Vector3(1 + (curCollum * 2), 1 + -(2 * curRow), 0);
                    curRow++;
                    curCollum = 0;
                }
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = ColorOptionsStartPoint.transform.position + ofset;
                cube.name = i.ToString();
                cube.tag = "btn_color";
                MeshRenderer mr = cube.GetComponent<MeshRenderer>();
                mr.material = curStripe.myMat[i];
                cube.transform.parent = ColorOptionsStartPoint.transform;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if (Input.GetMouseButton(0))
            {
                if (objectHit.name == "Left")
                {
                    garmentDisplay.transform.Rotate(Vector3.up, garmentDisplayRotSpeed * Time.deltaTime);
                }
                if (objectHit.name == "Right")
                {
                    garmentDisplay.transform.Rotate(Vector3.down, garmentDisplayRotSpeed * Time.deltaTime);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (objectHit.name == "btn_add")
                {
                    AddToShoppingList(curSelectedGarment);
                }
                if (objectHit.name == "btn_close")
                {
                    light.shadowStrength = 1;
                    popUP.SetActive(false);
                    camController.enabled = true;
                    Destroy(curSelectedGarment);
                }
                if (objectHit.tag == "btn_color")
                {
                    //curStripe.SetMatByIndex(int.Parse(objectHit.name));
                    if (dropdown.transform.childCount != 4)
                    {
                        curStripe.SetMatByIndex(int.Parse(objectHit.name));
                    }
                    
                }
                if (objectHit.tag == "popup")
                {
                    popUP.SetActive(true);
                    light.shadowStrength = 0;
                    camController.enabled = false;
                    if(lasthittedpopupname == objectHit.name)
                    {
                        if(curSelectedGarment==null)
                        {
                            SpawnGarment(objectHit);
                        }
                        else
                        {
                            Destroy(curSelectedGarment);
                        }
                    }
                    else
                    {
                        Destroy(curSelectedGarment);
                        SpawnGarment(objectHit);
                    }
                    lasthittedpopupname = objectHit.name;
                }
            }
        }
	}

    private void SpawnGarment(Transform objectHit)
    {
        int index = int.Parse(objectHit.name);
        GameObject temp = Instantiate(allGarments[index], garmentDisplay.transform);
        temp.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        SetCurGarment(temp);
    }

    private void PopulateList()
    {
        Debug.Log("PopulateList");
        if (curSelectedGarment == null)
        {
            Debug.Log("curSelectedGarment nog null");
            PopulateList();
        }
        Garment g = curSelectedGarment.GetComponent<Garment>();

        items = g.GetGarmentPieces();
        
        for (int i = 0; i < items.Count; i++)
        {
            names.Add(items[i].name);
        }
        Debug.Log(names);
        dropdown.AddOptions(names);
        DropDown_IndexChanged(0);
    }

    public void AddToShoppingList(GameObject obj)
    {
        shoppingList.Add(obj);
    }

    public void SetCurGarment(GameObject garment)
    {
        curSelectedGarment = garment;
        curSelectedGarment.transform.parent = garmentDisplay.transform;
        PopulateList();
    }

    void OnGUI()
    {
        /*
        if(GUI.Button(new Rect(Screen.width*0.8f,0,Screen.width*0.2f,Screen.width*0.2f),"print"))
        {
            for(int i =0; i <shoppingList.Count;i++)
            {
                Garment garment = shoppingList[i].GetComponent<Garment>();
                garment.PrintGarmentDetails();
            }
        }*/
    }
}
