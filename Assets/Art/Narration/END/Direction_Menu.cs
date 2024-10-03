using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Direction_Menu : MonoBehaviour
{
    // Dur�e du d�lai avant de changer de sc�ne (en secondes)
    public float delay = 5f;

    // D�marre la coroutine lors du chargement de la sc�ne
    void Start()
    {
        StartCoroutine(GoToMainMenu());
    }

    // Coroutine pour attendre un d�lai avant de changer de sc�ne
    IEnumerator GoToMainMenu()
    {
        // Attend pendant "delay" secondes
        yield return new WaitForSeconds(delay);

        // Change de sc�ne pour aller au menu principal
        SceneManager.LoadScene("MainMenu");
    }
}
