using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoHandler : MonoBehaviour {

    public GameObject videoFrame;
    public Component videoplayer;
	// Use this for initialization
	void Start () {
        videoplayer = videoFrame.GetComponent("Video Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
