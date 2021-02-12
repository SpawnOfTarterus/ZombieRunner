using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;
    [SerializeField] HUDControl hud;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    private void Start()
    {
        //currentAmmo = maxAmmo;
    }

    public bool CheckForAmmo(AmmoType ammoType)
    {
        if (GetAmmoSlot(ammoType).ammoAmount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetAmmoAmount(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void UseAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
        hud.UpdateAmmoText(ammoType, GetAmmoSlot(ammoType).ammoAmount);
    }

    public void GainAmmo(AmmoType ammoType, int pickUpAmount)
    {
        GetAmmoSlot(ammoType).ammoAmount += pickUpAmount;
        if(FindObjectOfType<Gun>().GetAmmoType() != ammoType) { return; }
        hud.UpdateAmmoText(ammoType, GetAmmoSlot(ammoType).ammoAmount);
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach(AmmoSlot ammoSlot in ammoSlots)
        {
            if(ammoSlot.ammoType == ammoType)
            {
                return ammoSlot;
            }
        }
        return null;
    }
}
