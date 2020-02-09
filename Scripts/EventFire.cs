// Stand in for the combat system until I properly put one together. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFire : MonoBehaviour
{
    public BattleMoveNode start;
    // Start is called before the first frame update
    void Start()
    {
        start.Fire(new BattleMoveNode.FireArguments(null,null,new List<BattleMoveNode>()));
    }
    
}
