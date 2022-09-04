using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private List<Transform> checkpoint = new List<Transform>();

    [SerializeField] private List<UnityEvent> checkpointAction = new List<UnityEvent>();

    public Vector3 RecoverCheckpointPosition(int index)
    {
        checkpointAction[index].Invoke();
        return checkpoint[index].position;
    }

    public void RegisterCheckpoint(Transform check)
    {
        int index = checkpoint.IndexOf(check);
        GameManager.Instance.gameData.checkpoint = index;
        GameManager.Instance.gameData.SaveGameData();
    }
}
