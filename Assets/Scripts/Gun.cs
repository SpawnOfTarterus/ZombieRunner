﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Camera myCamera = null;
    [SerializeField] GameObject projectile = null;
    [SerializeField] Transform projectileSpawnPos = null;
    [SerializeField] ParticleSystem muzzleFlash = null;
    [SerializeField] Ammo ammo = null;

    float raycastEquivilentMultiplyer = 1000f;
    float noZoom = 60f;
    float ironSightsZoom = 50f;

    public void SetMyCamera(Camera camera)
    {
        myCamera = camera;
    }

    public void SetAmmo(Ammo thisAmmo)
    {
        ammo = thisAmmo;
    }

    // Start is called before the first frame update
    void Start()
    {
        myCamera.fieldOfView = noZoom;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        ToggleZoom();
    }

    private void ToggleZoom()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            if(myCamera.fieldOfView == noZoom)
            {
                myCamera.fieldOfView = ironSightsZoom;
            }
            else if(myCamera.fieldOfView == ironSightsZoom)
            {
                myCamera.fieldOfView = noZoom;
            }
        }
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(!ammo.CheckForAmmo()) { return; }
            RaycastHit hit;
            muzzleFlash.Play();
            if(Physics.Raycast(myCamera.transform.position, myCamera.transform.forward, out hit, Mathf.Infinity))
            {
                SpawnProjectile(hit.point);
            }
            else
            {
                Vector3 fauxHitPoint = myCamera.transform.position + myCamera.transform.forward * raycastEquivilentMultiplyer;
                SpawnProjectile(fauxHitPoint);
            }
        }
    }

    private void SpawnProjectile(Vector3 targetDirection)
    {
        ammo.UseAmmo();
        GameObject bulletInstance = Instantiate(projectile, projectileSpawnPos.position, transform.rotation);
        bulletInstance.GetComponent<Projectile>().SetTargetPoint(targetDirection);
        Destroy(bulletInstance, 3f);
    }
}
