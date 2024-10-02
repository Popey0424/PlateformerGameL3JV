using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampLight : MonoBehaviour
{
    [SerializeField] private Light2D _light;





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

    }


    public void OnLight()
    {
        

        _light.enabled = true;




    }

    public void OffLight()
    {

        _light.enabled = false;
    }
}
   
