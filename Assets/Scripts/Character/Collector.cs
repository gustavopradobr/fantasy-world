using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private LayerMask collectableLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if ((collectableLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            if (collision.gameObject.TryGetComponent(out Collectable collectable))
            {
                Collectable.CollectableType type = collectable.type;
                int value = collectable.Collect();
                GameManager.AddCoin(value);
            }
        }
    }
}
