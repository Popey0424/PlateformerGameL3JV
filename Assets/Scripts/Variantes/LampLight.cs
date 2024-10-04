using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampLight : MonoBehaviour
{
    [SerializeField] private Light2D _light;





    private void Start()
    {
        OnLight();
    }

    private void Update()
    {
      

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
   
