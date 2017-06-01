using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MONSTER_TOO_FAR : AkTriggerBase
{
    public void Bye()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}