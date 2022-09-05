using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string speakerName;
        [TextArea(3, 10)]
        public string[] sentences;
    }

    [Header("References")]
    [SerializeField] private GameObject boxTalkIcon;
    [SerializeField] private GameObject spaceInstruction;

    [Header("Dialogue")]
    public Dialogue dialogue;
    public UnityEvent dialogFinished;

    private Transform playerTransform = null;

    public void TriggerDialog(Action call)
    {
        GameManager.Instance.dialogueManager.StartDialogue(dialogue, call);       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.gameObject.transform;
            spaceInstruction.SetActive(true);
            collision.gameObject.GetComponent<NormalInput>().OnPressSpace.AddListener(StartDialogue);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spaceInstruction.SetActive(false);
            collision.gameObject.GetComponent<NormalInput>().OnPressSpace.RemoveListener(StartDialogue);
        }
    }
    public void StartDialogue()
    {
        boxTalkIcon.SetActive(true);
        spaceInstruction.SetActive(false);
        
        if (playerTransform)
            GameManager.Instance.cameraController.EnableDialogueCamera(true, Vector3.Lerp(playerTransform.position, transform.position, 0.5f));

        GameManager.Instance.playerController.gameObject.GetComponent<NormalInput>().OnPressSpace.RemoveListener(StartDialogue);
        TriggerDialog(DialogueFinished);
    }

    public void DialogueFinished()
    {
        boxTalkIcon.SetActive(false);
        dialogFinished.Invoke();

        if (!ShopManager.shopIsOpen)
        {
            spaceInstruction.SetActive(true);
            GameManager.Instance.playerController.gameObject.GetComponent<NormalInput>().OnPressSpace.AddListener(StartDialogue);
        }
    }
}
