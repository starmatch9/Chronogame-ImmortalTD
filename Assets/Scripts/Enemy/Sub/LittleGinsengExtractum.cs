using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGinsengExtractum : Enemy
{
    //————小人参精————

    [HideInInspector]
    public bool attach = false;
    public override void OtherSpawn()
    {
        
    }

    public override void OtherReset()
    {
        if (!attach)
        {
            return;
        }

        if (GlobalData.globalEnemies.Contains(this))
        {
            GlobalData.globalEnemies.Remove(this);
        }

        Destroy(gameObject);
    }

}
