using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NativeAndroidBluetooth : MonoBehaviour
{
    private const string BLUETOOTH_CLASS = "com.smartpalm.nativebluetoothplugin.BluetoothService";

    //[SerializeField]
    //private Text _searchingLabel;

    [SerializeField]
    private TextMesh _deviceList;

    private GameObject offObj;
    private GameObject onObj;
    private List<string> namesOfNearbyDevices = new List<string>();
    private string jsonOfNearbyDevices;
    private string[] deviceNames;
    private bool currentlySearching = false;
    private bool stateOfIcon = false;
    private float startTime = 0.0f;


    private void Start()
    {
        offObj = GameObject.Find("QuadOff");
        onObj = GameObject.Find("QuadOn");
        onObj.SetActive(stateOfIcon);
    }

    // Update is called once per frame
    void Update()
    {
        using (AndroidJavaClass btServiceClass = new AndroidJavaClass(BLUETOOTH_CLASS))
        {
            string oldJson = jsonOfNearbyDevices;
            jsonOfNearbyDevices = btServiceClass.CallStatic<string>("getNearbyDeviceNamesAsJson");
            if (!oldJson.Equals(jsonOfNearbyDevices))
            {
                updateDeviceList();
            }
        }

        updateCurrentlySearching();
    }

    public void callMethodForGameObject(GameObject obj)
    {
        if (obj.name.Equals("SphereForChangeSearchState"))
        {
            //changeIconDisplay();
            if (currentlySearching)
            {
                stopGetPairedDevices();
            }
            else
            {
                startGetPairedDevices();
            }
        }
        else if (obj.name.Equals("SphereForConnectToNearest"))
        {
            connectToNearestPairedDevice();
        }
    }

    void updateCurrentlySearching()
    {
        using (AndroidJavaClass btServiceClass = new AndroidJavaClass(BLUETOOTH_CLASS))
        {
            AndroidJavaObject btService = btServiceClass.CallStatic<AndroidJavaObject>("getInstance");
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            bool oldSearching = currentlySearching;
            currentlySearching = btServiceClass.CallStatic<bool>("getCurrentlyScanningForNearbyDevices");
            if (oldSearching != currentlySearching)
            {
                changeIconDisplay();
            }
            //_deviceList.text = currentlySearching.ToString();
        }
    }

    void updateDeviceList()
    {
        jsonOfNearbyDevices = jsonOfNearbyDevices.Replace("[", "");
        jsonOfNearbyDevices = jsonOfNearbyDevices.Replace("]", "");
        jsonOfNearbyDevices = jsonOfNearbyDevices.Replace("\"", "");

        deviceNames = jsonOfNearbyDevices.Split(',');

        if (currentlySearching)
        {
            _deviceList.text = "Searching ...\n";
        }
        else
        {
            _deviceList.text = "";
        }
        foreach (string device in deviceNames)
        {
            _deviceList.text = _deviceList.text + device + "\n";
        }
    }

    private void changeIconDisplay()
    {
        offObj.SetActive(stateOfIcon);
        onObj.SetActive(!stateOfIcon);
        stateOfIcon = !stateOfIcon;
    }

    public void askForBluetoothPermission()
    {
        using (AndroidJavaClass btServiceClass = new AndroidJavaClass(BLUETOOTH_CLASS))
        {
            AndroidJavaObject btService = btServiceClass.CallStatic<AndroidJavaObject>("getInstance");
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject context = jc.GetStatic<AndroidJavaObject>("currentActivity");

            btService.Call("setMainContext", context);
            btService.Call("connectToBluetooth");
        }
    }

    public void startGetPairedDevices()
    {
        using (AndroidJavaClass btServiceClass = new AndroidJavaClass(BLUETOOTH_CLASS))
        {
            AndroidJavaObject btService = btServiceClass.CallStatic<AndroidJavaObject>("getInstance");
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject context = jc.GetStatic<AndroidJavaObject>("currentActivity");

            btService.Call("setMainContext", context);
            btService.Call("connectToBluetooth");
            btService.Call("startGettingPairedDevices");
            //_searchingLabel.text = "Searching for devices...";
            jsonOfNearbyDevices = "";
        }
    }

    public void stopGetPairedDevices()
    {
        using (AndroidJavaClass btServiceClass = new AndroidJavaClass(BLUETOOTH_CLASS))
        {
            AndroidJavaObject btService = btServiceClass.CallStatic<AndroidJavaObject>("getInstance");
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject context = jc.GetStatic<AndroidJavaObject>("currentActivity");

            btService.Call("stopGettingPairedDevices");
        }
    }

    public void connectToNearestPairedDevice()
    {
        using (AndroidJavaClass btServiceClass = new AndroidJavaClass(BLUETOOTH_CLASS))
        {
            AndroidJavaObject btService = btServiceClass.CallStatic<AndroidJavaObject>("getInstance");
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject context = jc.GetStatic<AndroidJavaObject>("currentActivity");

            btService.Call("connectToNearestPairedDevice");
        }
    }
}
