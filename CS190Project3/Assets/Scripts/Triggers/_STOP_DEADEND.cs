using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _STOP_DEADEND : AkTriggerBase
{
    public void SetDeadend()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}
