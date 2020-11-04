using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedWeapon : MonoBehaviour
{
    [SerializeField] Camera myCamera = null;
    [SerializeField] Ammo ammo = null;
    [SerializeField] GameObject startGun = null;
    [SerializeField] Gun[] gunsInBag = new Gun[0];

    GameObject currentGun;

    // Start is called before the first frame update
    void Start()
    {
        EquipWeapon(startGun);
    }

    private void Update()
    {
        SwitchWeapon();
    }

    private void SwitchWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Destroy(currentGun);
            EquipWeapon(gunsInBag[0].gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Destroy(currentGun);
            EquipWeapon(gunsInBag[1].gameObject);
        }
    }

    private void EquipWeapon(GameObject gunToEquip)
    {
        currentGun = Instantiate(gunToEquip, transform.position, transform.rotation, transform);
        currentGun.GetComponent<Gun>().SetMyCamera(myCamera);
        currentGun.GetComponent<Gun>().SetAmmo(ammo);
    }

}
