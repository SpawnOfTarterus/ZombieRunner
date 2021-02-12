using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    Light light;
    float maxLightWidth = 100f;
    float minLightWidth = 20f;
    float maxLightDistance = 200f;
    float minLightDistance = 10f;
    float maxLightIntensity = 3f;
    float minLightIntensity = 1f;
    float maxBattery = 100f;
    float timer;
    [SerializeField] float battery;
    [SerializeField] HUDControl hud;
    [SerializeField] Material batteryMaterial;

    private void Start()
    {
        battery = maxBattery;
        light = GetComponent<Light>();
    }

    void Update()
    {
        ToggleLight();
        IntensityControl();
        DrainBattery();
    }

    public void GainBattery(float power)
    {
        battery += power;
        hud.UpdateBatteryBar(battery, maxBattery);
        if(battery > maxBattery)
        {
            battery = maxBattery;
        }
    }

    private void ToggleLight()
    {
        if(battery == 0) { return; }
        if(Input.GetKeyDown(KeyCode.F))
        {
            light.enabled = !light.enabled;
            hud.ToggleBatteryBarSetting();
        }
    }

    private void DrainBattery()
    {
        if(light.enabled && battery > 0)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                battery -= light.intensity * 2;
                hud.UpdateBatteryBar(battery, maxBattery);
                timer = 0;
            }
            if (battery < 0) { battery = 0; light.enabled = false; }
        }
    }

    private void IntensityControl()
    {
        if (!light.enabled) { return; }
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(light.range < maxLightDistance)
            {
                light.range += ((maxLightDistance - minLightDistance) / 100 * 5);
            }
            if(light.spotAngle > minLightWidth)
            {
                light.spotAngle -= ((maxLightWidth - minLightWidth) / 100 * 5);
            }
            if (light.intensity < maxLightIntensity)
            {
                light.intensity += ((maxLightIntensity - minLightIntensity) / 100 * 5);
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (light.range > minLightDistance)
            {
                light.range -= ((maxLightDistance - minLightDistance) / 100 * 5);
            }
            if (light.spotAngle < maxLightWidth)
            {
                light.spotAngle += ((maxLightWidth - minLightWidth) / 100 * 5);
            }
            if (light.intensity > minLightIntensity)
            {
                light.intensity -= ((maxLightIntensity - minLightIntensity) / 100 * 5);
            }
        }
    }
}
