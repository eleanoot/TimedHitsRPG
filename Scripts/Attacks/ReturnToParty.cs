using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToParty : BattleMoveNode
{
    private float speed = 10.0f;
    private BattleUnit actor;
    public override void Fire(FireArguments args)
    { 
        base.Fire(args);
        actor = args.actor;
        StartCoroutine(MoveFromTo(actor.transform, actor.transform.position, actor.startingPos));
    }

    IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b)
    {
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = b;
        //if (actor.tag == "PlayerUnit")
        //    actor.gameObject.GetComponent<PlayerUnitAction>().FinishTiming();
        //else
        //    actor.gameObject.GetComponent<EnemyUnitAction>().FinishTiming();
        SignalFinished();
    }
    
}
