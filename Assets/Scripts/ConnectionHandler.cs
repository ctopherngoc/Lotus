using FishNet;
using FishNet.Transporting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public enum ConnectionType
{
    Host,
    Client
}

public class ConnectionHandler : MonoBehaviour
{
    public GameObject XRORIGIN;
    public ConnectionType connectionType;
    public bool serverBool = false;

#if UNITY_EDITOR
    private void OnEnable()
    {
        InstanceFinder.ClientManager.OnClientConnectionState += OnClientConnectionState;
    }

    private void OnDisable()
    {
        InstanceFinder.ClientManager.OnClientConnectionState -= OnClientConnectionState;
    }

    private void OnClientConnectionState(ClientConnectionStateArgs args)
    {
        if(args.ConnectionState == LocalConnectionState.Stopping)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
#endif


    private void Start()
    {
        #if UNITY_EDITOR
        if (ParrelSync.ClonesManager.IsClone())
        {
            //InstanceFinder.ClientManager.StartConnection();
            //MainCamera.SetActive(false);
            return;
        }
        else
        {
            InstanceFinder.ClientManager.StartConnection();
            Debug.Log("<color=red>connectiotype.client</color>");
            return;
        }
        #endif
    }

}
