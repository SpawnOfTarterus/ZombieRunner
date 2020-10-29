using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 4f;
    [SerializeField] int damage = 1;
    [SerializeField] ParticleSystem hitEffect = null;
    [SerializeField] float hitRadius = 0f;

    Vector3 targetPoint;
    Rigidbody rigidbody;
    int enemyLayer = 1 << 10;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.Normalize(CalculateForceToApply(targetPoint)) * projectileSpeed);
    }

    public void SetTargetPoint(Vector3 newTargetPoint)
    {
        targetPoint = newTargetPoint;
    }

    private Vector3 CalculateForceToApply(Vector3 targetPos)
    {
        Vector3 rawSlope = targetPos - transform.position;
        float divisor = FindDivisor(rawSlope);
        Vector3 forceToApply = rawSlope / divisor;
        return forceToApply;
    }

    private float FindDivisor(Vector3 rawSlope)
    {
        if(Mathf.Abs(rawSlope.x) <= Mathf.Abs(rawSlope.y) && Mathf.Abs(rawSlope.x) <= Mathf.Abs(rawSlope.z))
        {
            return Mathf.Abs(rawSlope.x);
        }
        else if(Mathf.Abs(rawSlope.y) <= Mathf.Abs(rawSlope.z))
        {
            return Mathf.Abs(rawSlope.y);
        }
        else
        {
            return Mathf.Abs(rawSlope.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayHitEffect();
        if(collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth)) 
        { 
            enemyHealth.DealDamage(damage);
            Destroy(gameObject);
            return; 
        }
        CheckForDamageables();
    }

    private void PlayHitEffect()
    {
        GameObject hitEffectInstance = Instantiate(hitEffect.gameObject, transform.position, Quaternion.identity);
        Destroy(hitEffectInstance, hitEffect.main.startLifetime.constantMax);
    }

    private void CheckForDamageables()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, hitRadius, enemyLayer);
        foreach(Collider hitCollider in hitColliders)
        {
            float distanceFromExplosionCenter = Vector3.Distance(transform.position, hitCollider.transform.position);
            float explosionDamage = damage / (distanceFromExplosionCenter * distanceFromExplosionCenter);
            EnemyHealth enemy = hitCollider.gameObject.GetComponent<EnemyHealth>();
            enemy.DealDamage(Mathf.RoundToInt(explosionDamage));
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }
}
