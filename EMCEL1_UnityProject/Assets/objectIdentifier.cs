using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectIdentifier : MonoBehaviour
{
    public GameObject identifiedObj;

    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            identifiedObj = other.gameObject;
        }    
    }
}
