using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
namespace BNG
{

    public class NetworkCharacterOffset : NetworkBehaviour

    {
        // Start is called before the first frame update
        void Start()
        {

        }

        public override void OnStartClient()
        {

        var connectionHandler = GameObject.Find("NetworkManager").GetComponent<ConnectionHandler>();
        if (!connectionHandler.serverBool)
        {
                this.enabled = false;
           }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            float yOffset = transform.parent.localPosition.y;
            transform.localPosition = new Vector3(transform.localPosition.x, -1 - yOffset, transform.localPosition.z);
        }
    }
}
