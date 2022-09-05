using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLookAtPlayer : MonoBehaviour
{
    private Transform target;
    private Vector3 initalScale;
    [SerializeField] private Transform character;
    [SerializeField] [Range(-1, 1)]private int originalX = -1;
    private void Start()
    {
        target = GameManager.Instance.playerController.transform;
        initalScale = character.transform.localScale;
        initalScale.x = character.transform.localScale.x * originalX;
    }

    private void Update()
    {
        if(character)
            UpdateCharacterDirection();
    }

    private void UpdateCharacterDirection()
    {
        if (transform.position.x > target.transform.position.x)
            character.localScale = new Vector3(initalScale.x, initalScale.y, initalScale.z);
        else if (transform.position.x < target.transform.position.x)
            character.localScale = new Vector3(initalScale.x * -1, initalScale.y, initalScale.z);
    }
}
