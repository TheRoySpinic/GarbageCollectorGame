using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEffect : SingletonGen<ShowEffect>
{
    [SerializeField]
    private Animator blackout = null;

    public static void Blackout()
    {
        instance.blackout.SetTrigger("Blackout");
        instance.StartCoroutine(instance.CBlackout());
    }

    private IEnumerator CBlackout()
    {
        yield return new WaitForSeconds(0.2f);
        blackout.ResetTrigger("Blackout");
    }
}
