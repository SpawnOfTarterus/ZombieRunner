using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount;
    [SerializeField] float batteryAmount;

    private void Update()
    {
        transform.Rotate(Vector3.up, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(ammoAmount > 0)
        {
            var ammo = other.GetComponentInChildren<Ammo>();
            ammo.GainAmmo(ammoType, ammoAmount);
            Destroy(gameObject);
        }
        else if(batteryAmount > 0)
        {
            other.GetComponentInChildren<FlashLight>().GainBattery(batteryAmount);
            Destroy(gameObject);
        }
    }
}
