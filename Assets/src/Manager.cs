using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private Scene scene;
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
    public Text garmentname;
    public Text garmentprice;
    public Text description;
    public AudioSource futureofshoppingvr;

    // Use this for initialization
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        originalcam = Camera.main;
        curcam = originalcam;
    }

    public void ChangeSize_IndexChanged(int index)
    {
        sizeindex = index;
        sm.SetCurSize(sizeindex);
        while (curSelectedGarment = null)
        {
            curSelectedGarment = sm.GetCurGarment();
        }
    }

    public void DropDown_IndexChanged(int index)
    {
        groupindex = index;
        foreach (Transform child in ColorOptionsStartPoint.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        float maxCollums = 4;
        float curCollum = 0;
        float curRow = 0;
        //Debug.Log("collors:"+sm.GetMatList().Count);
        List<Material> mats = sm.mats;
        //trick to fix location for 3d color buttons//
        Quaternion camrot = camController.gameObject.transform.localRotation;
        float ofsetdist = 1.25f;
        camController.transform.rotation = Quaternion.identity;
        for (int i = 0; i < mats.Count; i++)
        {
            Vector3 ofset;
            if (curCollum < maxCollums)
            {
                ofset = new Vector3(1 + (curCollum * ofsetdist), 1 + -(ofsetdist * curRow), 0);
                curCollum++;
            }
            else
            {
                ofset = new Vector3(1 + (curCollum * ofsetdist), 1 + -(ofsetdist * curRow), 0);
                curRow++;
                curCollum = 0;
            }
            GameObject Cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            Cylinder.transform.localScale -= new Vector3(2, 0.5f, 2);
            if (!scene.name.Contains("vr"))
            {
                Cylinder.transform.eulerAngles = new Vector3(-90, 0, 0);
            }
            if (scene.name.Contains("vr"))
            {
                Cylinder.transform.localEulerAngles = new Vector3(90, 90, 30);
            }
            Cylinder.transform.position = ColorOptionsStartPoint.transform.position + ofset;
            Cylinder.transform.parent = ColorOptionsStartPoint.transform;
            Cylinder.name = i.ToString();
            Cylinder.tag = "btn_color";
            MeshRenderer mr = Cylinder.GetComponent<MeshRenderer>();
            Debug.Log(mats[i].name);
            mr.material = mats[i];
            
        }
        //trick to fix location for 3d color buttons//
        camController.transform.rotation = camrot;
        //trick to fix location for 3d color buttons//

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
                        UnityEngine.Video.VideoPlayer vidplayer = vid1cam.transform.parent.GetComponent<UnityEngine.Video.VideoPlayer>();
                        vidplayer.Stop();
                        futureofshoppingvr.Play();
                        vidplayer.Play();

                    }
                }

                if (Input.GetMouseButton(0))
                {
                    if (objectHit.name == "Left")
                    {
                        RotateGarmentLeft();
                    }
                    if (objectHit.name == "Right")
                    {
                        RotateGarmentRight();
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
                        ClosePopup();
                    }
                    if (objectHit.tag == "btn_color")
                    {
                        Debug.Log("groupindex:"+groupindex);
                        //Debug.Log("groupindex:" + groupindex);
                        sm.ChangeMAT2(groupindex, objectHit.GetComponent<MeshRenderer>().material);
                    }
                    /*
                     * Spawn right GarmentPiece SizeManager
                     * */
                    if (objectHit.tag == "popup")
                    {
                        OpenPopup(objectHit.transform);
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
                        futureofshoppingvr.Stop();
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

    public void RotateGarmentLeft()
    {
        garmentDisplay.transform.Rotate(Vector3.up, garmentDisplayRotSpeed * Time.deltaTime);
    }

    public void RotateGarmentRight()
    {
        garmentDisplay.transform.Rotate(Vector3.down, garmentDisplayRotSpeed * Time.deltaTime);
    }

    public void ClosePopup()
    {
        //light.shadowStrength = 1;
        popUP.SetActive(false);
        camController.enabled = true;
        Destroy(sm.gameObject);
    }

    public void OpenPopup(Transform objectHit)
    {
        Debug.Log("Need to show popup");
        popUP.SetActive(true);
        //Debug.DebugBreak();
        // light.shadowStrength = 0;
        camController.enabled = false;
        if (lasthittedpopupname == objectHit.name)
        {
            if (curSelectedGarment == null)
            {
                SpawnGarment2(objectHit);
            }
            else
            {
                Destroy(curSelectedGarment);
            }
        }

        else
        {
            Destroy(curSelectedGarment);
            SpawnGarment2(objectHit);
        }
        lasthittedpopupname = objectHit.name;
    }

    private void SpawnGarment2(Transform objectHit)
    {
        int index = int.Parse(objectHit.name);
        GameObject temp = Instantiate(allGarments[index], garmentDisplay.transform);
        SetCurGarment2(temp);
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
        groupsnames = sm.GetGroupNames();
        for (int i = 0; i < count; i++)
        {
            //groupsnames.Add("garment group "+(i+1).ToString());
        }
        dropdown.AddOptions(groupsnames);
        DropDown_IndexChanged(0);
    }

    public void AddToShoppingList(GameObject obj)
    {
        shoppingList.Add(obj);
    }

    public void SetCurGarment2(GameObject garment)
    {
        curSelectedGarment = garment;
        sm = garment.GetComponent<SizeManager>();
        garmentname.text = sm.garmentname;
        garmentprice.text = sm.garmentprice;
        description.text = sm.garmentdescription;
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