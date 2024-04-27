using FishNet.Managing;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkRig : NetworkBehaviour
{
    public HardwareRig hardwareRig;
    public NetworkHand leftNetworkHand;
    public NetworkHand rightNetworkHand;
    public NetworkHead networkHead;
    //public GameObject NetworkManager;
    private ConnectionHandler connectionHandler;
    private bool activeBool = true;

    public override void OnStartClient()
    {
        if (!base.IsOwner)
        {
            this.enabled = false;
        }
    }

    // Start is called before the first frame update
    //[Client(RequireOwnership = true)]
    private void Start()
        {
            Debug.Log("<color=red>setting hardwarerig to networkrig spawn location</color>");
            hardwareRig = GameObject.FindWithTag("Player").GetComponent<HardwareRig>();
            hardwareRig.transform.SetPositionAndRotation(transform.position, transform.rotation);
            hardwareRig.rightHand.transform.SetPositionAndRotation(rightNetworkHand.transform.position, rightNetworkHand.transform.rotation);
            hardwareRig.leftHand.transform.SetPositionAndRotation(leftNetworkHand.transform.position, rightNetworkHand.transform.rotation);
    }

    // Update is called once per frame
    private void LateUpdate()
    {   
        /*
        if(hardwareRig != null)
        {
            transform.SetPositionAndRotation(hardwareRig.transform.position, hardwareRig.transform.rotation);
            leftNetworkHand.transform.SetPositionAndRotation(hardwareRig.leftHandPosition, hardwareRig.leftHandRotation);
            rightNetworkHand.transform.SetPositionAndRotation(hardwareRig.rightHandPosition, hardwareRig.rightHandRotation);
            networkHead.transform.SetPositionAndRotation(hardwareRig.headsetPosition, hardwareRig.headsetRotation);
        }
        */

    }
}
