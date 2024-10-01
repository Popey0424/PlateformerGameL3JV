using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllersInfo : MonoBehaviour
{
    [SerializeField] private GameObject controllerMenu;
    [SerializeField] private Image raycastImage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            ClickControllerMenu();
        } 
    }
    public void ClickControllerMenu()
    {
        raycastImage.gameObject.SetActive(true);
        controllerMenu.SetActive(true);

    }


    public void ClickBackControllerMenu()
    {
        raycastImage.gameObject.SetActive(false);
        controllerMenu.SetActive(false);
    }


}
