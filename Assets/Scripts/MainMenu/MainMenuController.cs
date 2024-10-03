using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("FadeImage")]
    [SerializeField] private Image imagefade;

    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject leaveWarning;

    [Header("Raycast")]
    [SerializeField] private GameObject rayCastLeaveImage;

    [Header("EscapeKey")]
    [SerializeField] private KeyCode leaveMainMenu;

    [Header("Bool Escape")]
    [SerializeField] private bool IsInMainMenu;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioClick;

    private void Start()
    {
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        rayCastLeaveImage.SetActive(false);
        IsInMainMenu = true;
    }

    private void Update()
    {
        if (settingsMenu.activeSelf && Input.GetKeyDown(leaveMainMenu))
        {
            //Back
            IsInMainMenu = true;
        }
        if (creditsMenu.activeSelf && Input.GetKeyDown(leaveMainMenu))
        {
            //back
            IsInMainMenu = true;
        }
        if(leaveWarning.activeSelf && Input.GetKeyDown(leaveMainMenu))
        {
            //back
            IsInMainMenu= true;
        }
    }



    #region StartGame
    public void OnClickPlaybutton()
    {
        audioClick.Play();
        imagefade.gameObject.SetActive(true);
        imagefade.DOFade(1, 2.9f).OnComplete(FadeStartComplete);
    }
    private void FadeStartComplete()
    {
        SceneManager.LoadScene(2);
    }
    #endregion

    #region CreditsMenu
    public void OnClickCreditsButton()
    {
        audioClick.Play();
        imagefade.gameObject.SetActive(true);
        imagefade.DOFade(1, 2.9f).OnComplete(FadeCreditsComplete);
    }
    private void FadeCreditsComplete()
    {
        creditsMenu.gameObject.SetActive(true);
        IsInMainMenu = false;
        imagefade.DOFade(0, 1).OnComplete(ResetFade);
    }

    public void OnClickBackCredits()
    {
        audioClick.Play();
        imagefade.gameObject.SetActive(true);
        imagefade.DOFade(1, 1f).OnComplete(FadeMenuComplete);
    }
    #endregion

    #region SettingsMenu
    public void OnClickSettingsButtons()
    {
        audioClick.Play();
        imagefade.gameObject.SetActive(true);
        imagefade.DOFade(1, 1f).OnComplete(FadeSettingsComplete);
    }

    private void FadeSettingsComplete()
    {
        settingsMenu.gameObject.SetActive(true);
        IsInMainMenu = false;
        imagefade.DOFade(0, 1).OnComplete(ResetFade);
    }

    public void OnClickBackSettings()
    {
        audioClick.Play();
        imagefade.gameObject.SetActive(true);
        imagefade.DOFade(1, 1f).OnComplete(FadeMenuComplete);
    }
    #endregion

    #region LeaveGame Version2
    public void OnClickLeaveGameButton()
    {
        audioClick.Play();
        Animator animator_LeaveGame = leaveWarning.GetComponent<Animator>();

        if (animator_LeaveGame != null)
        {
            bool IsOpen = animator_LeaveGame.GetBool("IsOpen");
            animator_LeaveGame.SetBool("IsOpen", true);
        }
        rayCastLeaveImage.SetActive(true);
    }
    #endregion

    #region LeaveGameYes

    public void OnClickYesLeaveGame()
    {
        audioClick.Play();
        Application.Quit();
        Debug.Log("JEu Quitter");
    }
    #endregion

    #region LeaveGameNo
    public void OnClickLeaveGameNo()
    {
        audioClick.Play();
        leaveWarning.SetActive(false);
        rayCastLeaveImage.SetActive(false);
    }
    #endregion

    #region FadeMenu
    private void FadeMenuComplete()
    {
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        IsInMainMenu = true;
        imagefade.DOFade(0, 1).OnComplete(ResetFade);
    }
    #endregion

    #region ResetFade
    public void ResetFade()
    {
        imagefade.gameObject.SetActive(false);
    }
    #endregion

    #region LeaveGame
    public void OnClickWarningLeave()
    {
        leaveWarning.SetActive(true);
        rayCastLeaveImage.SetActive(true);
    }
    #endregion

}
