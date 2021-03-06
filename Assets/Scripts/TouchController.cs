﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TouchController : MonoBehaviour {


    private static int Y_FINGER_DIFFERENCE_THREE = 550;
    private static int Y_FINGER_DIFFERENCE_TWO = 350;
    private static int X_FINGER_DIFFERENCE = 150;
    private static int X_SWIPE_DIFFERENCE = 50;
    private static int X_SWIPE_DIFFERENCE_SMALL = 35;
    private static int Y_SWIPE_DIFFERENCE = 35;

    //listener events
    public static string EVENT_DOUBLE_TAP = "doubleTap";
    public static string EVENT_DOUBLE_TAP_TWO = "threeDoubleTap";
    public static string EVENT_SCROLL_VERTICAL = "scrollVertical";
    public static string EVENT_SCROLL_HORIZONTAL = "scrollHorizontal";

    public static string EVENT_LAYER_UP = "layerUp";
    public static string EVENT_LAYER_DOWN = "layerDown";

    private static string ORIENTATION_LANDSCAPE = "orientationLandscape";
    private static string ORIENTATION_PORTRAIT = "orientationPortrait";

    //private GameObject countField;
    private Text counter, position, direction, eventText, orientation, position3;
    Vector2 startTouch1, startTouch2, startTouch3;
    bool switchXOrientation, switchYOrientation, switchLayer, doubleTapCooldown;
    float cooldownStarted;

    // Use this for initialization
    void Start () {
        /*countField = GameObject.Find("DisplayText");
        counter = countField.GetComponent<Text>();

        GameObject positionField = GameObject.Find("Position");
        position = positionField.GetComponent<Text>();*/

        //GameObject directionField = GameObject.Find("Direction");
        //direction = directionField.GetComponent<Text>();

        /*GameObject eventField = GameObject.Find("Event");
        eventText = eventField.GetComponent<Text>();

        GameObject orientationField = GameObject.Find("Orientation");
        orientation = orientationField.GetComponent<Text>();*/

        //GameObject position3Field = GameObject.Find("Position3");
        //position3 = position3Field.GetComponent<Text>();

        switchXOrientation = false;
        switchYOrientation = false;
        switchLayer = false;
        doubleTapCooldown = true;
        cooldownStarted = 0f;
    }

    // Update is called once per frame
    void Update () {

        if(!doubleTapCooldown)
        {
            if((Time.time - cooldownStarted) > 1f)
            {
                doubleTapCooldown = true;
            }
        }

        //checkNumberOfTouches();
        if(Input.touchCount == 1)
        {
            touchWith1Finger();
        }
        else if(Input.touchCount == 2)
        {
            touchWith2Finger();
        }
        else if(Input.touchCount == 3)
        {
            touchWith3Finger();
        }
    }

    // 3 Finger touch gestures
    void touchWith3Finger()
    {
        bool start = false;

        //direction.text = "X: " + switchXOrientation + "    Y: " + switchYOrientation;

        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);
        Touch touch3 = Input.GetTouch(2);

        if (touch1.position.y < Screen.height / 2 && touch2.position.y < Screen.height / 2 && touch3.position.y < Screen.height/2)
        {
            if (touch3.phase == TouchPhase.Began)
            {
                start = true;
            }

            if (start == true)
            {
                startTouch1 = touch1.position;
                //position.text = "Start 1: " + startTouch1;
                startTouch2 = touch2.position;
                //eventText.text = "Start 2: " + startTouch2;
                startTouch3 = touch3.position;
                //position3.text = "Start 3: " + startTouch3;


                if(touch1.position.x > touch2.position.x)               // touch1.x is bigger touch2.x
                {
                    if(touch1.position.x > touch3.position.x)           // touch1.x is bigger touch3.x and touch2.x
                    {
                        if(touch2.position.x > touch3.position.x)       // touch2 is bigger touch3.x
                        {
                            if ((touch1.position.x - touch3.position.x) <= X_FINGER_DIFFERENCE)
                            {
                                switchXOrientation = true;
                            }
                        }
                        else                                            //touch3.x is bigger touch2.x
                        {
                            if ((touch1.position.x - touch2.position.x) <= X_FINGER_DIFFERENCE)
                            {
                                switchXOrientation = true;
                            }
                        }
                    }
                    else
                    {
                        if ((touch3.position.x - touch2.position.x) <= X_FINGER_DIFFERENCE)
                        {
                            switchXOrientation = true;
                        }
                    }
                }
                else if(touch2.position.x > touch3.position.x)
                {
                    if(touch3.position.x > touch1.position.x)
                    {
                        if ((touch2.position.x - touch1.position.x) <= X_FINGER_DIFFERENCE)
                        {
                            switchXOrientation = true;
                        }
                    }
                    else
                    {
                        if ((touch2.position.x - touch3.position.x) <= X_FINGER_DIFFERENCE)
                        {
                            switchXOrientation = true;
                        }
                    }
                }
                else
                {
                    if((touch3.position.x - touch1.position.x) <= X_FINGER_DIFFERENCE)
                    {
                        switchXOrientation = true;
                    }
                }

                if (touch1.position.y > touch2.position.y)               // touch1.x is bigger touch2.x
                {
                    if (touch1.position.y > touch3.position.y)           // touch1.x is bigger touch3.x and touch2.x
                    {
                        if (touch2.position.y > touch3.position.y)       // touch2 is bigger touch3.x
                        {
                            if ((touch1.position.y - touch3.position.y) <= Y_FINGER_DIFFERENCE_THREE)
                            {
                                switchYOrientation = true;
                            }
                        }
                        else                                            //touch3.x is bigger touch2.x
                        {
                            if ((touch1.position.y - touch2.position.y) <= Y_FINGER_DIFFERENCE_THREE)
                            {
                                switchYOrientation = true;
                            }
                        }
                    }
                    else
                    {
                        if ((touch3.position.y - touch2.position.y) <= Y_FINGER_DIFFERENCE_THREE)
                        {
                            switchYOrientation = true;
                        }
                    }
                }
                else if (touch2.position.y > touch3.position.y)
                {
                    if (touch3.position.y > touch1.position.y)
                    {
                        if ((touch2.position.y - touch1.position.y) <= Y_FINGER_DIFFERENCE_THREE)
                        {
                            switchYOrientation = true;
                        }
                    }
                    else
                    {
                        if ((touch2.position.y - touch3.position.y) <= Y_FINGER_DIFFERENCE_THREE)
                        {
                            switchYOrientation = true;
                        }
                    }
                }
                else
                {
                    if ((touch3.position.y - touch1.position.y) <= Y_FINGER_DIFFERENCE_THREE)
                    {
                        switchYOrientation = true;
                    }
                }

                start = false;
            }



            if (switchXOrientation == switchYOrientation == true)
            {
                //counter.text = "Änderung: " + (startTouch1.x - touch1.position.x);
                if ((startTouch1.x - touch1.position.x) > X_SWIPE_DIFFERENCE && (startTouch2.x - touch2.position.x) > X_SWIPE_DIFFERENCE && (startTouch3.x - touch3.position.x) > X_SWIPE_DIFFERENCE && Screen.orientation == ScreenOrientation.Portrait)
                {
                    Screen.orientation = ScreenOrientation.LandscapeLeft;
                    switchXOrientation = switchYOrientation = false;
                    //orientation.text = "Landscape";
                    GameObject.Find("VideoManager").GetComponent<VideoManager>().displayVideoLandscape(true);
                    EventManager.TriggerEvent(ORIENTATION_LANDSCAPE, 0);
                }
                else if ((startTouch1.x - touch1.position.x) < -X_SWIPE_DIFFERENCE && (startTouch2.x - touch2.position.x) < -X_SWIPE_DIFFERENCE && (startTouch3.x - touch3.position.x) < -X_SWIPE_DIFFERENCE && Screen.orientation == ScreenOrientation.Landscape)
                {
                    Screen.orientation = ScreenOrientation.Portrait;
                    switchXOrientation = switchYOrientation = false;
                    //orientation.text = "Portrait";
                    GameObject.Find("VideoManager").GetComponent<VideoManager>().displayVideoLandscape(false);
                    EventManager.TriggerEvent(ORIENTATION_PORTRAIT, 0);
                }
            }

            if (touch1.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled || touch2.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Canceled || touch3.phase == TouchPhase.Ended || touch3.phase == TouchPhase.Canceled)
            {
                switchXOrientation = switchYOrientation = false;
            }
        }
    }

    // 1 Finger touch gestures
    void touchWith1Finger()
    {
        Touch touch1 = Input.GetTouch(0);

        if (touch1.position.y < Screen.height / 2)
        {

            if(touch1.phase == TouchPhase.Began)
            {
                startTouch1 = touch1.position;
            }

            if (touch1.tapCount == 2 && doubleTapCooldown)
            {
                doubleTapCooldown = false;
                cooldownStarted = Time.time;
                EventManager.TriggerEvent(EVENT_DOUBLE_TAP, 0);
                
            }
            if ((touch1.position.y - startTouch1.y) > Y_SWIPE_DIFFERENCE)
            {
                if (touch1.deltaPosition.x < 15 && touch1.deltaPosition.x > -15)
                {
                    //direction.text = "Scrollen nach oben";
                    EventManager.TriggerEvent(EVENT_SCROLL_VERTICAL, touch1.deltaPosition.y);
                }
            }
            else if ((touch1.position.y - startTouch1.y) < -Y_SWIPE_DIFFERENCE)
            {
                if (touch1.deltaPosition.x < 15 && touch1.deltaPosition.x > -15)
                {
                    //direction.text = "Scrollen nach unten";
                    EventManager.TriggerEvent(EVENT_SCROLL_VERTICAL, touch1.deltaPosition.y);
                }
            }
            else if((touch1.position.x - startTouch1.x) > X_SWIPE_DIFFERENCE_SMALL)
            {
                if(touch1.deltaPosition.y < 15 && touch1.deltaPosition.y > -15)
                {
                    EventManager.TriggerEvent(EVENT_SCROLL_HORIZONTAL, touch1.deltaPosition.x);
                }
            }
            else if((touch1.position.x - startTouch1.x) < -X_SWIPE_DIFFERENCE_SMALL)
            {
                if (touch1.deltaPosition.y < 15 && touch1.deltaPosition.y > -15)
                {
                    EventManager.TriggerEvent(EVENT_SCROLL_HORIZONTAL, touch1.deltaPosition.x);
                }
            }
        }
    }

    // 2 Finger touch gestures
    void touchWith2Finger()
    {
        bool start = false;

        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        if (touch1.position.y < Screen.height / 2 && touch2.position.y < Screen.height / 2)
        {
            if (touch2.phase == TouchPhase.Began)
            {
                start = true;
                startTouch1 = touch1.position;
                //position.text = "Start 1: " + startTouch1;
                startTouch2 = touch2.position;
                //position3.text = "Start 2: " + startTouch2;
            }

            if (touch1.tapCount == 2 && touch2.tapCount == 2 && doubleTapCooldown)
            {
                doubleTapCooldown = false;
                cooldownStarted = Time.time;
                EventManager.TriggerEvent(EVENT_DOUBLE_TAP_TWO, 0);
                return;
            }

            if (start == true)
            {
                if (touch1.position.x > touch2.position.x)
                {
                    if (touch1.position.y > touch2.position.y)
                    {
                        if ((touch1.position.x - touch2.position.x) < X_FINGER_DIFFERENCE)
                        {
                            if ((touch1.position.y - touch2.position.y) < Y_FINGER_DIFFERENCE_TWO)
                            {
                                switchLayer = true;
                            }
                        }
                    }
                    else
                    {
                        if ((touch1.position.x - touch2.position.x) < X_FINGER_DIFFERENCE)
                        {
                            if ((touch2.position.y - touch1.position.y) < Y_FINGER_DIFFERENCE_TWO)
                            {
                                switchLayer = true;
                            }
                        }
                    }
                }
                else
                {
                    if (touch1.position.y > touch2.position.y)
                    {
                        if ((touch2.position.x - touch1.position.x) < X_FINGER_DIFFERENCE)
                        {
                            if ((touch1.position.y - touch2.position.y) < Y_FINGER_DIFFERENCE_TWO)
                            {
                                switchLayer = true;
                            }
                        }
                    }
                    else
                    {
                        if ((touch2.position.x - touch1.position.x) < X_FINGER_DIFFERENCE)
                        {
                            if ((touch2.position.y - touch1.position.y) < Y_FINGER_DIFFERENCE_TWO)
                            {
                                switchLayer = true;
                            }
                        }
                    }
                }
                start = false;
            }

            if (switchLayer == true)
            {
                //counter.text = "Änderung: " + (startTouch1.x - touch1.position.x);
                if ((startTouch1.x - touch1.position.x) < -X_SWIPE_DIFFERENCE_SMALL && (startTouch2.x - touch2.position.x) < -X_SWIPE_DIFFERENCE_SMALL)
                {
                    switchLayer = false;
                    //direction.text = "Eine Ebene nach unten";
                    EventManager.TriggerEvent(EVENT_LAYER_DOWN, 0);
                }
                else if ((startTouch1.x - touch1.position.x) > X_SWIPE_DIFFERENCE_SMALL && (startTouch2.x - touch2.position.x) > X_SWIPE_DIFFERENCE_SMALL)
                {
                    switchLayer = false;
                    //direction.text = "Eine Ebene nach oben";
                    EventManager.TriggerEvent(EVENT_LAYER_UP, 0);
                }
            }

            if (touch1.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled || touch2.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Canceled)
            {
                switchLayer = false;
            }
        }
    }

}
