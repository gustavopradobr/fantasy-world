using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject boxTalkIcon;
    [SerializeField] private GameObject spaceInstruction;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
        spaceInstruction.SetActive(false);
        GameManager.Instance.playerController.gameObject.GetComponent<NormalInput>().OnPressSpace.RemoveListener(StartDialogue);
        GetComponent<DialogueTrigger>().TriggerDialog(OpenShop);
    }

    public void OpenShop()
    {
        GameManager.Instance.OpenShop(true);
    }
}
