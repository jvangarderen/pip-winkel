using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStripes : MonoBehaviour {

    public List<Material> myMat;
    private int curmatID;
    public bool curSelected;
    MeshRenderer meshrenderer;
	// Use this for initialization
	void Start () {
        curSelected = true;
        curmatID = 0;
        meshrenderer = gameObject.GetComponent<MeshRenderer>();
        meshrenderer.material = myMat[curmatID];
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void NextMat()
    {
        if (curSelected)
        {
            curmatID++;
            if (curmatID > myMat.Count - 1)
            {
                curmatID = 0;
            }
            meshrenderer.material = myMat[curmatID];
        }
    }
    
    public void PrevMat()
    {
        if (curSelected)
        {
            curmatID--;
            if (curmatID < 0)
            {
                curmatID = myMat.Count - 1;
            }
            meshrenderer.material = myMat[curmatID];
        }
    }

    public Material getCurMat()
    {
        return meshrenderer.material;
    }

    public void ToggleSelected()
    {
        curSelected = !curSelected;
    }
}
