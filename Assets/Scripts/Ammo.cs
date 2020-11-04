using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int currentAmmo;
    int maxAmmo = 25;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    public bool CheckForAmmo()
    {
        if (currentAmmo > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UseAmmo()
    {
        currentAmmo--;
    }
}
