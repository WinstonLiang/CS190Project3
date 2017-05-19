using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _WALK : AkTriggerBase
{
    public void Walk()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}
