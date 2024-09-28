using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Vector2 lastCheckpointPostion;


    private void Awake()
    {
        if(Instance == null)
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
}
