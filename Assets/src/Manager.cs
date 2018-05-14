using System.Collections;
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
    List<string> groupsnames = new List<string>();
    List<GameObject> items;
    Camera originalcam;
    public Camera vid1cam;
    private Camera curcam;
    GameObject gPiece;
    ChangeStripes curStripe;
    private int sizeindex;
    int groupindex;
    private SizeManager sm;

	// Use this for initialization
	void Start () {
        originalcam = Camera.main;
        curcam = originalcam;
	}

    public void ChangeSize_IndexChanged(int index)
    {
        sizeindex = index;
        sm.SetCurSize(sizeindex);
        //curSelectedGarment = sm.GetCurGarment();
    }

    public void DropDown_IndexChanged(int index)
    {
        groupindex = index;
        foreach (Transform child in ColorOptionsStartPoint.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        float maxCollums = 2;
        float curCollum = 0;
        float curRow = 0;
        //Debug.Log("collors:"+sm.GetMatList().Count);
        List<Material> mats = sm.GetMatList();
        //trick to fix location for 3d color buttons//
        Quaternion camrot = camController.gameObject.transform.localRotation;
        camController.transform.rotation = Quaternion.identity;
        for (int i = 0; i < mats.Count; i++)
        {
            Vector3 ofset;
            if (curCollum < maxCollums)
            {
                ofset = new Vector3(1 + (curCollum * 2), 1 + -(2 * curRow), 0);
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
            cube.transform.parent = ColorOptionsStartPoint.transform;
            cube.name = i.ToString();
            cube.tag = "btn_color";
            MeshRenderer mr = cube.GetComponent<MeshRenderer>();
            mr.material = mats[i];
        }
        //trick to fix location for 3d color buttons//
        camController.transform.rotation = camrot;
        //trick to fix location for 3d color buttons//

        /*
        if (index >= 0)
        {
            gPiece = items[index];
            curStripe = gPiece.GetComponent<ChangeStripes>();
            Debug.Log(items[index].name + ", Nummer of materials:"+curStripe.myMat.Count);
            float maxCollums = 2;
            float curCollum = 0;
            float curRow = 0;

            //trick to fix location for 3d color buttons//
            Quaternion camrot = camController.gameObject.transform.localRotation;
            camController.transform.rotation = Quaternion.identity;
            //trick to fix location for 3d color buttons//
            /*
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
                cube.transform.position = ColorOptionsStartPoint.transform.position+ofset;
                cube.transform.parent = ColorOptionsStartPoint.transform;
                cube.name = i.ToString();
                cube.tag = "btn_color";
                MeshRenderer mr = cube.GetComponent<MeshRenderer>();
                mr.material = curStripe.myMat[i];
            }

            //trick to fix location for 3d color buttons//
            camController.transform.rotation = camrot;
            //trick to fix location for 3d color buttons//
        }*/
    }

    // Update is called once per frame
    void Update() 
    {
        RaycastHit hit;

        if (curcam == originalcam)
        {
            Ray ray = curcam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (Input.GetMouseButtonUp(0))
                {
                    if (objectHit.name == "vid1")
                    {
                        curcam = vid1cam;
                        originalcam.enabled = false;
                        vid1cam.enabled = true;
                        camController.enabled = false;
                    }                    
                }

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
                        //light.shadowStrength = 1;
                        popUP.SetActive(false);
                        camController.enabled = true;
                        Destroy(curSelectedGarment);
                    }
                    if (objectHit.tag == "btn_color")
                    {
                        sm.ChangeSelectedGroupMat(groupindex, int.Parse(objectHit.name));
                    }
                    /*
                     * Spawn right GarmentPiece SizeManager
                     * */
                    if (objectHit.tag == "popup")
                    {
                        Debug.Log("Need to show popup");
                        popUP.SetActive(true);
                        Debug.DebugBreak();
                       // light.shadowStrength = 0;
                        camController.enabled = false;
                        if (lasthittedpopupname == objectHit.name)
                        {
                            if (curSelectedGarment == null)
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
                            //SpawnGarment(objectHit);
                            SpawnGarment2(objectHit);
                        }
                        lasthittedpopupname = objectHit.name;
                        
                    }
                }
            }
        }
        else
        {
            Ray ray = curcam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("in raycast");
                Transform objectHit = hit.transform;
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("Mouse down");
                    if (objectHit.name == "vid1")
                    {
                        Debug.Log("back out of video");
                        originalcam.enabled = true;
                        vid1cam.enabled = false;
                        curcam = originalcam;
                        camController.enabled = true;
                    }
                }
            }
        }
	}

    private void SpawnGarment2(Transform objectHit)
    {
        int index = int.Parse(objectHit.name);
        GameObject temp = Instantiate(allGarments[index], garmentDisplay.transform);
        SetCurGarment2(temp);
    }

    private void SpawnGarment(Transform objectHit)
    {
        int index = int.Parse(objectHit.name);
        GameObject temp = Instantiate(allGarments[index], garmentDisplay.transform);

        //for originall garment directly not neceserry when garment is under parent
        //temp.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        SetCurGarment(temp);
    }

    private void PopulateList()
    {
        //Debug.Log("PopulateList");
        if (curSelectedGarment == null)
        {
            Debug.Log("curSelectedGarment nog null");
            PopulateList();
        }

        sm = curSelectedGarment.GetComponent<SizeManager>();
        sm.SetCurSize(sizeindex);
        //Debug.Log(sm.transform.name);
        Debug.Log(curSelectedGarment.name);
        Debug.Log(sm.GetCurGarment().name);
        Garment g = sm.GetCurGarment().GetComponent<Garment>();
        items = g.GetGarmentPieces();
        
        for (int i = 0; i < items.Count; i++)
        {
            names.Add(items[i].name);
        }
        
        Debug.Log(names);
        dropdown.AddOptions(names);
        DropDown_IndexChanged(0);
    }


    private void PopulateList2()
    {
        //Debug.Log(curSelectedGarment.name);
        SizeManager sm = curSelectedGarment.GetComponent<SizeManager>();
        sm.SetCurSize(sizeindex);
        //Debug.Log(sm.GetGroupNumber());
        int count = sm.GetGroupNumber();
        dropdown.options.Clear();
       // dropdown.ClearOptions();
        groupsnames = new List<string>();
        for (int i = 0; i < count; i++)
        {
            groupsnames.Add("garment group "+(i+1).ToString());
        }
        dropdown.AddOptions(groupsnames);
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

    public void SetCurGarment2(GameObject garment)
    {
        curSelectedGarment = garment;
        sm = garment.GetComponent<SizeManager>();
        curSelectedGarment.transform.parent = garmentDisplay.transform;
        PopulateList2();
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
