using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoller : MonoBehaviour
{
    [SerializeField] private GameObject characterObject = null;

    private void Start()
    {
        DisableRagdoll();
    }

    //enable the ragdoll
    public void EnableRagdoll()
    {
        //all rigidbodies
        Rigidbody[] rbs = characterObject.GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < rbs.Length; i++)
        {
            Rigidbody rb = rbs[i]; 
            
            //enable gravity
            rb.useGravity = true;

            //disable kinematic
            rb.isKinematic = false;
        }
    }

    //disable the ragdoll
    public void DisableRagdoll()
    {
        //all rigidbodies
        Rigidbody[] rbs = characterObject.GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < rbs.Length; i++)
        {
            Rigidbody rb = rbs[i];

            //disable gravity
            rb.useGravity = false;

            //enable kinematic
            rb.isKinematic = true;
        }
    }
}
