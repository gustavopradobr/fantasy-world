using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChangeInspector : MonoBehaviour
{
    public bool changeSkin = false;

    private void OnValidate()
    {
        GetComponent<SkinChanger>().UpdateSkin(changeSkin ? 0 : 1);
    }
}
