using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : StateMachineBehaviour
{
    [SerializeField] private bool playerFinishAttack = false;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerFinishAttack)
        {
            PlayerWeapon.FinishAttack();
        }
    }
}
