﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SizeManager : MonoBehaviour {

    public List<GameObject> clothSizes;
    public List<Garment> garments;
    public GatherMaterials gatherMats;
    public GameObject curGarmentOBJ;


    public List<Material> mats = new List<Material>();

    [System.Serializable]
    public struct rowData
    {
        public List<GameObject> gameobject;
    }

    public List<rowData> numberOfGroups = new List<rowData>();

	// Use this for initialization
	void Start () {
        
        for (int i = 0; i < clothSizes.Count; i++)
        {
            garments.Add(clothSizes[i].GetComponent<Garment>());
        }

        //temp test large
        gatherMats = clothSizes[0].GetComponent<GatherMaterials>();
	}

    // Update is called once per frame
    void Update()
    {
		
	}


    public void SetCurSize(int index)
    {
        for (int i = 0; i < clothSizes.Count; i++)
        {
            if (i == index)
            {
                curGarmentOBJ = clothSizes[index];
                curGarmentOBJ.active = true;
            }
            else
            {
                clothSizes[i].active = false;
            }
        }
    }

    public void ChangeSelectedGroupMat(int groupIndex, int newMatIndex)
    {

        Material newmat = gatherMats.GetMatByIndex(newMatIndex);
        List<GameObject> garmentPieces = new List<GameObject>();
        foreach (GameObject obj in clothSizes)
        {
            GatherMaterials gm = obj.GetComponent<GatherMaterials>();
            List<GameObject> temp =  gm.GetGroupObj(groupIndex);
            foreach (GameObject piece in temp)
            {
                garmentPieces.Add(piece);
            }
        }

        for (int i = 0; i < garmentPieces.Count; i++)
        {
            MeshRenderer mr = garmentPieces[i].GetComponent<MeshRenderer>();
            mr.material = mats[newMatIndex];
        }
    }

    public List<Material> GetMatList()
    {
        return gatherMats.GetMaterials();
    }
    public void SetMatList(List<Material> mat)
    {
        mats = mat;
    }

    public int GetGroupNumber()
    {
        gatherMats = curGarmentOBJ.GetComponent<GatherMaterials>();
        return gatherMats.GetMaterials().Count;
    }

    public GameObject GetCurGarment()
    {
        return curGarmentOBJ;
    }
}
