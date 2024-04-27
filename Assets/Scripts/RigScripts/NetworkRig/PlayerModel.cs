using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
using FishNet.Connection;

public class PlayerModel : NetworkBehaviour
{
    public GameObject sprite;
    public override void OnStartClient()
    {
        if (base.IsOwner)
        {
            sprite.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
