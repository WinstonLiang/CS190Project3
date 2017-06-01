using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MONSTER_CLOSER : AkTriggerBase
{
    public void Step()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}