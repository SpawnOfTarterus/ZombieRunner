using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDControl : MonoBehaviour
{
    [SerializeField] Text ammoText;
    [SerializeField] Image healthBar;
    [SerializeField] Image batteryBar;
    [SerializeField] Image damageImage;

    Color litBattery = new Color(.522f, 1f, .984f, 1f);
    Color unlitBattery = new Color(.522f, 1f, .984f, .392f);
    float timer;


    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer < 0) { timer = 0; }
            damageImage.color = new Color(damageImage.color.r, damageImage.color.g, damageImage.color.b, timer);
        }
    }

    public void UpdateBatteryBar(float battery, float maxBattery)
    {
        batteryBar.fillAmount = battery / maxBattery;
    }

    public void UpdateHealthBar(int health, int maxHealth)
    {
        healthBar.fillAmount = ((float)health / maxHealth); 
    }

    public void UpdateAmmoText(AmmoType ammoType, int ammoAmount)
    {
        ammoText.text = $"{ammoType} : {ammoAmount}";
    }

    public void ToggleBatteryBarSetting()
    {
        if(batteryBar.color == litBattery)
        {
            batteryBar.color = unlitBattery;
        }
        else
        {
            batteryBar.color = litBattery;
        }
    }

    public void DisplayDamageImage()
    {
        damageImage.color = new Color(damageImage.color.r, damageImage.color.g, damageImage.color.b, 1f);
        timer = 1f;
    }


}
