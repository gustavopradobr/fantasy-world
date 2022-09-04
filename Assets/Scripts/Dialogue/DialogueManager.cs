using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private Queue<string> sentences;
    private Action dialogueCallback;
    private bool typing = false;
    private bool cancelTyping = false;
    WaitForSecondsRealtime waitType = new WaitForSecondsRealtime(0.01f);


    [Header("References")]
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private Text headerText;
    [SerializeField] private Text dialogueText;

    [Header("Properties")]
    [SerializeField] private float typeDelay = 0.01f;

    void Start()
    {
        sentences = new Queue<string>();
        waitType = new WaitForSecondsRealtime(typeDelay);
    }

    public void StartDialogue(DialogueTrigger.Dialogue dialogue, Action call)
    {
        dialogueCallback = call;
        GameManager.Instance.playerController.movementEnabled = false;
        dialogueCanvas.gameObject.SetActive(true);
        headerText.text = dialogue.speakerName;

        GameManager.Instance.playerController.GetComponent<NormalInput>().OnPressSpace.AddListener(DisplayNextSentence);

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (typing)
        {
            cancelTyping = true;
            return;
        }


        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        if (!typing)
            StartCoroutine(TypeSentence(sentence));
    }
    private IEnumerator TypeSentence(string sentence)
    {
        typing = true;
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            if (cancelTyping)
                break;

            dialogueText.text += letter;
            yield return waitType;

            if (cancelTyping)
                break;
        }
        dialogueText.text = sentence;
        typing = false;
        cancelTyping = false;
    }

    private void EndDialogue()
    {
        headerText.text = "";
        dialogueText.text = "";
        dialogueCanvas.gameObject.SetActive(false);
        GameManager.Instance.playerController.movementEnabled = true;
        GameManager.Instance.cameraController.EnableDialogueCamera(false, Vector3.zero);

        GameManager.Instance.playerController.GetComponent<NormalInput>().OnPressSpace.RemoveListener(DisplayNextSentence);

        dialogueCallback?.Invoke();
        dialogueCallback = null;
    }
}
