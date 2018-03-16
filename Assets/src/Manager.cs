using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public List<GameObject> shoppingList;
    public GameObject garmentDisplay;
    public float garmentDisplayRotSpeed;
    public GameObject curSelectedGarment;
    public List<GameObject> allGarments;
    private string lasthittedpopupname;
	// Use this for initialization
	void Start () {
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
                if (objectHit.tag == "popup")
                {
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

    public void AddToShoppingList(GameObject obj)
    {
        shoppingList.Add(obj);
    }

    public void SetCurGarment(GameObject garment)
    {
        curSelectedGarment = garment;
        curSelectedGarment.transform.parent = garmentDisplay.transform;
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(Screen.width*0.8f,0,Screen.width*0.2f,Screen.width*0.2f),"print"))
        {
            for(int i =0; i <shoppingList.Count;i++)
            {
                Garment garment = shoppingList[i].GetComponent<Garment>();
                garment.PrintGarmentDetails();
            }
        }
    }
}
