using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public static bool playerAttacking = false;
    public static int playerDamage = 10;

    [Range(0, 100)][SerializeField] private int quickAttackDamage = 10;
    [Range(0, 100)][SerializeField] private int strongAttackDamage = 15;
    public void AttackQuick()
    {
        playerAttacking = true;
        playerDamage = quickAttackDamage;
    }
    public void AttackStrong()
    {
        playerAttacking = true;
        playerDamage = strongAttackDamage;
    }
    public static void FinishAttack(){
        playerAttacking = false;
    }

}
