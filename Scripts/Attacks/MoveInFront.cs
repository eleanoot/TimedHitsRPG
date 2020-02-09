using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInFront : BattleMoveNode
{
    private float speed = 10.0f;
    private BattleUnit target;
    private BattleUnit actor;
    public override void Fire(FireArguments args)
    {
        base.Fire(args);
        Debug.Log("inherit happened");
        target = args.target;
        actor = args.actor;
        Debug.Log("SpawnScript and object is active " + gameObject.activeInHierarchy);
        StartCoroutine(MoveFromTo(actor.transform, actor.transform.position, target.transform.position + new Vector3(3, 0 ,0)));
        //while (!isFinished)
        //    UpdateFrame();
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
        SignalFinished();
    }

    //void Update()
    //{
    //    // Move our position a step closer to the target.
    //    float step = speed * Time.deltaTime; // calculate distance to move
    //    actor.transform.position = Vector3.MoveTowards(actor.transform.position, target.transform.position, step);
    //    base.UpdateFrame();
    //}

    //public override void UpdateFrame()
    //{
    //    base.UpdateFrame();
    //    float step = speed * Time.deltaTime;
    //    Debug.Log(string.Format("step {0}", step));
    //    actor.transform.position = Vector2.MoveTowards(actor.transform.position, target.transform.position, step);
    //    Debug.Log(string.Format("moved to {0}, {1}", actor.transform.position.x, actor.transform.position.y));
    //}
}
