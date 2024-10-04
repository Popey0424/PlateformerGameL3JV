using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Vector2 lastCheckpointPostion;
    public GameObject Lampe;
    public GameObject Ghost;
    [SerializeField] private Image imageFade;

    //public GameObject objectToDesactive;

    //private void Start()
    //{
    //    objectToDesactive.SetActive(false);
    //}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveCheckpoint(Vector2 checkpointPosition)
    {
        lastCheckpointPostion = checkpointPosition;
    }

    public Vector2 GetLastCheckpointPosition()
    {
        return lastCheckpointPostion;
    }

    public void Lose()
    {
        Lampe.SetActive(false);
        Ghost.SetActive(false);

        StartCoroutine(GonnaDie());
;
    }

    IEnumerator GonnaDie()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("FinDuTempsImaprtie");
        imageFade.gameObject.SetActive(true);
        imageFade.DOFade(1, 2f).OnComplete(FadeDeathComplete);
        Debug.Log("MOOOOOOOOOOOORRRRRRRRRRRRRTTTTTTTTTTTTTTTT");
    }

    private void FadeDeathComplete()
    {
        Debug.Log("Changement de Scene");
        //SceneManager.LoadScene(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
