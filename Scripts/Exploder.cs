using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 15.0f;
    [SerializeField] private float explosionForce = 200f;
    [SerializeField] private float maxDamage = 100f;
    [SerializeField] private AnimationCurve damageDropoff = new AnimationCurve();

    public void Explode() 
    {
        StartCoroutine(DoExplosion());
    }

    private void DamageStuffNearby() 
    {
        var cols = Physics.OverlapSphere(transform.position, explosionRadius);

        List<Transform> rootsHit = new List<Transform>();

        for (int i = 0; i < cols.Length; i++)
        {
            if (!rootsHit.Contains(cols[i].transform.root) && cols[i].transform.root.TryGetComponent(out Health health)) 
            {
                rootsHit.Add(cols[i].transform.root);

                float distance = (transform.position - cols[i].transform.position).magnitude;
                distance /= explosionRadius;

                float damage = maxDamage * damageDropoff.Evaluate(distance);

                health.TakeDamage(damage);
            }
        }
    }

    private void ThrowRigidbodies() 
    {
        var cols = Physics.OverlapSphere(transform.position, explosionRadius);

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].TryGetComponent(out Rigidbody rb)) 
            {
                Vector3 dir = cols[i].transform.position - transform.position;
                float distance = dir.magnitude;
                distance /= explosionRadius;

                rb.AddForce(explosionForce * damageDropoff.Evaluate(distance) * dir, ForceMode.Impulse);
            }
        }
    }

    IEnumerator DoExplosion() 
    {
        DamageStuffNearby();

        yield return new WaitForEndOfFrame();

        ThrowRigidbodies();
    }
}
