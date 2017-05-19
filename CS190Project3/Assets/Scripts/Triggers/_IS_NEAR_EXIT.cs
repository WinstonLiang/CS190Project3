using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _IS_NEAR_EXIT : AkTriggerBase
{
    public void TheLight()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}