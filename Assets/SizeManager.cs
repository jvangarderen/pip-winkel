using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SizeManager : MonoBehaviour {
    
    public List<GameObject> clothSizes;
    public List<Garment> garments;
    public GatherMaterials gatherMats;
    public GameObject curGarmentOBJ;
    public string garmentname;
    public string garmentprice;
    public string garmentdescription;
    public bool skipFirstGroup=false;
    public List<Material> mats ;//= new List<Material>();

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

        //temp test s
        gatherMats = clothSizes[0].GetComponent<GatherMaterials>();
	}

    // Update is called once per frame
    void Update()
    {
		
	}


    public void SetCurSize(int index)
    {
        if (clothSizes.Count > 1)
        {
            for (int i = 0; i < clothSizes.Count; i++)
            {
                if (i == index)
                {
                    curGarmentOBJ = clothSizes[index];
                    curGarmentOBJ.active = true;
                    Debug.Log("activate:"+i);
                }
                else
                {
                    Debug.Log("deactivate:" + i);
                    clothSizes[i].active = false;
                }
            }
        }
        else
        {
            curGarmentOBJ = clothSizes[0];
            curGarmentOBJ.active = true;
        }
        
        
    }

    public void ChangeSelectedGroupMat(int groupIndex, int newMatIndex)
    {
        
       // Material newmat = gatherMats.GetMatByIndex(newMatIndex);
        Material newmat = mats[newMatIndex];
        Debug.Log(groupIndex + " , newmatname:" + newmat); 
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
            //mr.material = mats[newMatIndex];
            mr.material = newmat;
        }
    }

    public void ChangeMAT2(int groupIndex, Material newMat)
    {
        List<GameObject> garmentPieces = new List<GameObject>();
        foreach (GameObject obj in clothSizes)
        {
            GatherMaterials gm = obj.GetComponent<GatherMaterials>();
            List<GameObject> temp = gm.GetGroupObj(groupIndex);
            foreach (GameObject piece in temp)
            {
                garmentPieces.Add(piece);
            }
        }

        for (int i = 0; i < garmentPieces.Count; i++)
        {
            MeshRenderer mr = garmentPieces[i].GetComponent<MeshRenderer>();
            mr.material = newMat;
        }
    }

    public List<Material> GetMatList()
    {
        return mats;
        //return gatherMats.GetMaterials();
    }
    public void SetMatList(List<Material> mat)
    {
        mats = mat;
    }

    public int GetGroupNumber()
    {
        int count;
        gatherMats = curGarmentOBJ.GetComponent<GatherMaterials>();
        count = gatherMats.GetMaterials().Count;
        if (skipFirstGroup) count = count - 1;
        return count;
    }

    public GameObject GetCurGarment()
    {
        return curGarmentOBJ;
    }
}
