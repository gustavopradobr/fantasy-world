using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RegisterCheckpoint();
        }
    }

    public void RegisterCheckpoint()
    {
        GameManager.Instance.checkpointManager.RegisterCheckpoint(transform);
    }
}
