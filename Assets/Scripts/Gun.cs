using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Camera myCamera = null;
    [SerializeField] GameObject startGun = null;
    [SerializeField] GameObject projectile = null;
    [SerializeField] Transform projectileSpawnPos = null;
    [SerializeField] float projectileSpeed = 1f;

    float raycastEquivilentMultiplyer = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject currentGun = Instantiate(startGun, transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if(Physics.Raycast(myCamera.transform.position, myCamera.transform.forward, out hit, Mathf.Infinity))
            {
                GameObject bulletInstance = Instantiate(projectile, projectileSpawnPos.position, transform.rotation);
                bulletInstance.GetComponent<Projectile>().SetTargetPoint(hit.point);
                Destroy(bulletInstance, 3f);
            }
            else
            {
                GameObject bulletInstance = Instantiate(projectile, projectileSpawnPos.position, transform.rotation);
                Vector3 fauxHitPoint = myCamera.transform.position + myCamera.transform.forward * raycastEquivilentMultiplyer;
                bulletInstance.GetComponent<Projectile>().SetTargetPoint(fauxHitPoint);
                Destroy(bulletInstance, 3f);
            }
        }
    }
}
