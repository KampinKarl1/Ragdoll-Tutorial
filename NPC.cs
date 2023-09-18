using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Animator controller = null;
    [SerializeField] private string hitTriggerName = "hit";
    private void Awake()
    {
        if (controller == null && !TryGetComponent(out controller)) 
        {
            Debug.LogWarning(name + " doesn't have an animator controller");
        }
    }

    public void GetHit() 
    {
        controller.SetTrigger(hitTriggerName);
    }
}
