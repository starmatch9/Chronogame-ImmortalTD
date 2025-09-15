using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGinsengExtractum : Enemy
{
    //————小人参精————

    public override void OtherSpawn()
    {
        
    }

    public override void OtherReset()
    {
        if (GlobalData.globalEnemies.Contains(this))
        {
            GlobalData.globalEnemies.Remove(this);
        }
        Destroy(gameObject);
    }

}
