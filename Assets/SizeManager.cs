using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour {

    public List<GameObject> clothSizes;
    public List<Garment> garments;
    public GameObject curGarmentOBJ;
	// Use this for initialization
	void Start () {
        
        for (int i = 0; i < clothSizes.Count; i++)
        {
            garments.Add(clothSizes[i].GetComponent<Garment>());
        }

        //temp test large
        
	}

    // Update is called once per frame
    void Update()
    {
		
	}


    public void SetCurSize(int index)
    {
        //Debug.Log(clothSizes.Count);
       // index = 2;
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

    public GameObject GetCurGarment()
    {
        return curGarmentOBJ;
    }
}
