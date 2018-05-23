﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

    private GameObject currentDisplayedObject;
    private bool canHaveAction;

	// Use this for initialization
	void Start () {

        EventManager.StartListening(TouchController.EVENT_SCROLL_HORIZONTAL, onScrollHorizontal);

        EventManager.StartListening(TouchController.EVENT_SCROLL_VERTICAL, onScrollVertical);

        canHaveAction = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void onScrollHorizontal( float changed)
    {
        if(canHaveAction)
        {
            currentDisplayedObject.transform.Rotate(0, changed * 2, 0);
        }
    }

    void onScrollVertical( float changed )
    {
        if(canHaveAction)
        {
            currentDisplayedObject.transform.Rotate(changed * 2, 0, 0);
        }
    }

    public void setGameObject(string objectName, bool canHaveAction)
    {
        currentDisplayedObject = GameObject.Find(objectName);
        this.canHaveAction = canHaveAction;
    }
}