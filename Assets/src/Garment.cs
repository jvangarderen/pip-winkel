using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garment : MonoBehaviour {

    public List<GameObject> garmentPieces;
    private List<ChangeStripes> changeStripes;
	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update()
    {
		
	}

    void OnGUI()
    {/*
        GUI.Box(new Rect(0,0,200,Screen.height),"");
        for (int i = 0; i < garmentPieces.Count; i++)
        {
            GUI.Label(new Rect(0, 100 * i, 50, 20), garmentPieces[i].name);
            if (GUI.Button(new Rect(50, 100 * i, 50, 50), "prev"))
            {
                ChangeStripes cs = garmentPieces[i].GetComponent<ChangeStripes>();
                cs.PrevMat();
            }
            if (GUI.Button(new Rect(100, 100 * i, 50, 50), "next"))
            {
                ChangeStripes cs = garmentPieces[i].GetComponent<ChangeStripes>();
                cs.NextMat();
            }
        }

        if (GUI.Button(new Rect(100, 0, 200, 50), "add"))
        {
            Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
            manager.AddToShoppingList(gameObject);
        }*/
    }


    public List<GameObject> GetGarmentPieces()
    {
        return garmentPieces;
    }

    public void PrintGarmentDetails()
    {
        Debug.Log(gameObject.name + ":\n");
        for (int i = 0; i < garmentPieces.Count; i++)
        {
            Debug.Log(garmentPieces[i].name + ":\n\t");
            ChangeStripes cs = garmentPieces[i].GetComponent<ChangeStripes>();
            Debug.Log(cs.getCurMat());
        }
    }
}
