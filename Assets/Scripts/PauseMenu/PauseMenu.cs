using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Image raycast;
    [SerializeField] private Image imageFade;




    private void Start()
    {
        pauseMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickMenuPause();
        }
    }

    #region BackToMainMenu
    private void OnClickMenuPause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        raycast.gameObject.SetActive(true);
        
    }

    public void OnClickMainMenu()
    {
        Time.timeScale = 1;
        imageFade.gameObject.SetActive(true);
        imageFade.DOFade(1, 2.9f).OnComplete(FadeCompleteBackToMainMenu);
    }

    private void FadeCompleteBackToMainMenu()
    {

        SceneManager.LoadScene("MainMenu");
        ResetFade();
    }
    #endregion

    #region OnClickBackToGame
    public void OnClickBackToGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        raycast.gameObject.SetActive(false);
        
    }
    #endregion

    #region QuitGame
    public void QuitGame()
    {
        imageFade.gameObject.SetActive(true);
        imageFade.DOFade(1, 1f).OnComplete(FadeCompleteLeaveTheGame);
    }

    private void FadeCompleteLeaveTheGame()
    {
        Application.Quit();
    }
    #endregion 

    #region ResetFade
    private void ResetFade()
    {
        imageFade.gameObject.SetActive(false);
    }
    #endregion
}
