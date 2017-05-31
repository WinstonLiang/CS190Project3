using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _OUTSIDE : AkTriggerBase
{
    public void TheBirds()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}