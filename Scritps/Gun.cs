
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    [SerializeField] private float force = 250f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            FireGun();
        }
    }

    private void FireGun()
    {
        print("Bang!");



        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100f))
        {
            if (hit.transform.root.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);

                if (!health.IsAlive && hit.collider.TryGetComponent(out Rigidbody rb))
                    rb.AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
            }
        }

    }
}
