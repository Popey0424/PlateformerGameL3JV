using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampLight : MonoBehaviour
{
    [SerializeField] private Light2D _light;

    

    [SerializeField] private float batteryLevel = 100f;
    [SerializeField] private float rechargeRate = 5f;
    [SerializeField] private const float lightConsumption = 20f;
    [SerializeField] private const float minBatteryToTurnOn = 25f;
 
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLight();
        }
        if (Input.GetMouseButtonDown(1))
        {
            OffLight();
        }
        if (!_light.enabled)
        {
            RechargeBattery();
        }
    }


    public void OnLight()
    {
        if (batteryLevel >= minBatteryToTurnOn)
        {
            _light.enabled = true;
            batteryLevel -= lightConsumption;
        }
        else
        {
            Debug.Log("Plus de Batterie");
        }
    }

    public void OffLight()
    {
        
        _light.enabled = false;
    }

    public void RechargeBattery()
    {
        if (batteryLevel < 100f)
        {
            batteryLevel += rechargeRate * Time.deltaTime;
            batteryLevel = Mathf.Clamp(batteryLevel, 0f, 100f); 
        }
    }
}
