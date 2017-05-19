using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TOCK_ODD : AkTriggerBase
{
    public void Tick()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}