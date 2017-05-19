using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TICK_EVEN : AkTriggerBase
{
    public void Tick()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}
