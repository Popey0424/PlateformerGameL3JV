using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Direction_Menu : MonoBehaviour
{
    // Durée du délai avant de changer de scène (en secondes)
    public float delay = 5f;

    // Démarre la coroutine lors du chargement de la scène
    void Start()
    {
        StartCoroutine(GoToMainMenu());
    }

    // Coroutine pour attendre un délai avant de changer de scène
    IEnumerator GoToMainMenu()
    {
        // Attend pendant "delay" secondes
        yield return new WaitForSeconds(delay);

        // Change de scène pour aller au menu principal
        SceneManager.LoadScene("MainMenu");
    }
}
