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
    [SerializeField] private GameObject settignsMenu;
    [SerializeField] private GameObject leaveWarning;

    [SerializeField] private GameObject rayCastLeaveImage;

    public KeyCode leaveMainMenu;





    #region StartGame
    public void OnClickPlaybutton()
    {
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
        imagefade.gameObject.SetActive(true);
        imagefade.DOFade(1, 2.9f).OnComplete(FadeCreditsComplete);
    }
    private void FadeCreditsComplete()
    {
        creditsMenu.gameObject.SetActive(true);
    }
    #endregion

    #region SettingsMenu
    public void OnClickSettingsButtons()
    {
        imagefade.gameObject.SetActive(true);
        imagefade.DOFade(1, 1f).OnComplete(FadeSettingsComplete);
    }

    private void FadeSettingsComplete()
    {
        settignsMenu.gameObject.SetActive(true);
    }
    #endregion

    #region LaveGame
    public void OnClickLeaveGameButton()
    {
        Animator animator_LeaveGame = leaveWarning.GetComponent<Animator>();

        if (animator_LeaveGame != null)
        {
            bool IsOpen = animator_LeaveGame.GetBool("IsOpen");
            animator_LeaveGame.SetBool("IsOpen", true);
        }
        rayCastLeaveImage.SetActive(true);
    }
    #endregion









}
