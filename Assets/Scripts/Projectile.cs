using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 4f;
    [SerializeField] int damage = 1;

    Vector3 targetPoint;
    Rigidbody rigidbody;
    bool hitSomething = false;

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
        Vector3 forceToApply = GetSimplestSlope(rawSlope, divisor);
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

    private Vector3 GetSimplestSlope(Vector3 rawSlope, float divisor)
    {
        float newX = rawSlope.x / divisor;
        float newY = rawSlope.y / divisor;
        float newZ = rawSlope.z / divisor;
        return new Vector3(newX, newY, newZ);
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitSomething = true;
        if(!collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth)) { return; }
        enemyHealth.DealDamage(damage);
    }
}
