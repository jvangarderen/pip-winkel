using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GatherMaterials : MonoBehaviour {

    public List<Material> mats;
    public List<string> groupnames;
    SizeManager sm;

    private List<List<GameObject>> grouplist = new List<List<GameObject>>();
    private List<GameObject> group1 = new List<GameObject>();
    private List<GameObject> group2 = new List<GameObject>();
    private List<GameObject> group3 = new List<GameObject>();
    private List<GameObject> group4 = new List<GameObject>();
    private List<GameObject> group5 = new List<GameObject>();
    private List<GameObject> group6 = new List<GameObject>();
    private List<GameObject> group7 = new List<GameObject>();
    private List<GameObject> group8 = new List<GameObject>();
    private List<GameObject> group9 = new List<GameObject>();
    private List<GameObject> group10 = new List<GameObject>();


    void Awake()
    {
        foreach (Transform child in transform)
        {
            bool add = true;

            MeshRenderer mr = child.GetComponent<MeshRenderer>();
            for (int i = 0; i < mats.Count; i++)
            {
                if (mats[i].color == mr.material.color)
                {
                    addChildAtIndex(child.gameObject, i);
                    add = false;
                }
            }
            if (add)
            {
                mats.Add(mr.material);
                addChildAtIndex(child.gameObject, mats.Count - 1);
                groupnames.Add(child.name);
            }
        }
        
        // add stuff to the group list
        grouplist.Add(group1);
        grouplist.Add(group2);
        grouplist.Add(group3);
        grouplist.Add(group4);
        grouplist.Add(group5);
        grouplist.Add(group6);
        grouplist.Add(group7);
        grouplist.Add(group8);
        grouplist.Add(group9);
        grouplist.Add(group10);
        //mats = mats.Distinct().ToList();

        int mathshape = 0;
        for (int i = 0; i < grouplist.Count; i++)
        {
            // Debug.Log(grouplist[i].Count);
            for (int j = 0; j < grouplist[i].Count; j++)
            {

                if (grouplist[i][j].name.Contains("MatShape"))
                {
                    //Debug.Log("!!!!!!!!"+grouplist[i][j].name);mathshape++; }
                    //Debug.Log(": " + grouplist[i][j].name);
                }
            }
        }
    }
	void Start () 
    {
        Debug.Log(transform.name);
        sm = gameObject.transform.parent.GetComponent<SizeManager>();
	}

    private void addChildAtIndex(GameObject child, int index)
    {
        switch (index)
        {
            case 0: group1.Add(child.gameObject); break;
            case 1: group2.Add(child.gameObject); break;
            case 2: group3.Add(child.gameObject); break;
            case 3: group4.Add(child.gameObject); break;
            case 4: group5.Add(child.gameObject); break;
            case 5: group6.Add(child.gameObject); break;
            case 6: group7.Add(child.gameObject); break;
            case 7: group8.Add(child.gameObject); break;
            case 8: group9.Add(child.gameObject); break;
            case 9: group10.Add(child.gameObject); break;
        }
    }

    public List<string> GetGroupNames()
    {
        return groupnames;
    }

    public Material GetMatByIndex(int index)
    {
        return mats[index];
    }

    public List<GameObject> GetGroupObj(int index)
    {
        return grouplist[index];
    }
    
    public List<Material> GetMaterials()
    {
        return mats;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
