using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResetTriggerNya
{
    // Start is called before the first frame update
    public static void ResetAllTrigger(this Animator animator)
    {
        foreach (var trigger in animator.parameters)
        {
            if (trigger.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(trigger.name);
            }
        }
    }

}
