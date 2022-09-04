using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;

    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private SpriteRenderer blackBackground;

    [Header("Properties")]
    [Range(0f, 10f)][SerializeField] private float followDamp;
    [Range(0f, 10f)][SerializeField] private float ortoSizeDamp;
    [Range(1f, 20f)][SerializeField] private float originalOrtoSize;

    [Header("Shop Camera")]
    [SerializeField] private float shopOrtoSize;
    [SerializeField] private Vector3 shopCamOffset;

    [Header("Dialogue Camera")]
    [SerializeField] private float dialogueOrtoSize;
    [SerializeField] private Vector3 dialogueCamOffset;
    private Vector3 dialogueCamTarget;

    [HideInInspector] public bool enableShopCamera = false;
    [HideInInspector] public bool enableDialogueCamera = false;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position;
        desiredPosition.z = transform.position.z;
        float desiredOrtoSize = originalOrtoSize;

        if (enableShopCamera)
        {
            desiredPosition = target.position + shopCamOffset + new Vector3(mainCamera.aspect, 0, 0);
            desiredPosition.z = transform.position.z;
            desiredOrtoSize = shopOrtoSize;
        }
        else if (enableDialogueCamera)
        {
            desiredPosition = dialogueCamTarget + dialogueCamOffset + new Vector3(mainCamera.aspect, 0, 0);
            desiredPosition.z = transform.position.z;
            desiredOrtoSize = dialogueOrtoSize;
        }

        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, Time.deltaTime * followDamp);
        mainCamera.orthographicSize = Mathf.SmoothStep(mainCamera.orthographicSize, desiredOrtoSize, Time.deltaTime * ortoSizeDamp);
    }


    public void EnableShopCamera(bool enable)
    {
        blackBackground.enabled = enable;
        enableShopCamera = enable;
    }
    public void EnableDialogueCamera(bool enable, Vector3 targetPosition)
    {        
        dialogueCamTarget = enable ? targetPosition : transform.position;
        enableDialogueCamera = enable;
    }
}
