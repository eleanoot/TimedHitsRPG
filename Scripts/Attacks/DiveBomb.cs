using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveBomb : BattleMoveNode
{
    public float perc;
    private float speed = 10.0f;
    float count = 0.0f;
    private BattleUnit target;
    private BattleUnit actor;
    public override void Fire(FireArguments args)
    {
        base.Fire(args);
        
        target = args.target;
        actor = args.actor;
       
        StartCoroutine(FlyUp(actor.transform, actor.transform.position, actor.transform.position + new Vector3(0, 3, 0)));
       
        //this.GetComponent<AttackTarget>().hit(args.target.gameObject);
    }

    public override float GetPerc()
    {
        return perc;
    }

    IEnumerator FlyUp(Transform objectToMove, Vector3 a, Vector3 b)
    {
        Debug.Log("flying up");
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = b;
        this.GetComponent<AttackTarget>().hit(target.gameObject);
        yield return new WaitForSeconds(1);
        
        StartCoroutine(FlyDown(actor.transform, actor.transform.position, target.transform.position + new Vector3(-1,1,0)));

    }

    IEnumerator FlyDown(Transform objectToMove, Vector3 a, Vector3 b)
    {
        Debug.Log("flying down");
        //target.GetComponent<DetermineAttackTiming>().enabled
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        //while (t <= 1.0f)
        //{
        //    t += step; // Goes from 0 to 1, incrementing by step each time
        //    Vector3 m1 = Vector3.Lerp(a, b, count);
        //    Vector3 m2 = Vector3.Lerp(a, b, count);
        //    objectToMove.position = Vector3.Lerp(m1, m2, t); // Move objectToMove closer to b
        //    yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        //}
        //while (t <= 1.0f)
        //{
        //    Debug.Log("step");
        //    t += step; // Goes from 0 to 1, incrementing by step each time
        //    objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
        //    yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        //}
        //objectToMove.position = b;

        float currentLerpTime = 0f;
        float lerpTime = 0.25f;
        perc = 0;
        while (perc <= 1.0f)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
                break;
            
            perc = currentLerpTime / lerpTime;
           // Debug.Log(perc);
            objectToMove.position = Vector3.Lerp(a, b, perc);
            yield return new WaitForFixedUpdate();
        }

        actor.gameObject.GetComponent<EnemyUnitAction>().FinishTiming();
        SignalFinished();

        //if (count < 1.0f)
        //    {
        //        count += 1.0f * Time.deltaTime;

        //        Vector3 m1 = Vector3.Lerp(a, b, count);
        //        Vector3 m2 = Vector3.Lerp(a, b, count);
        //        objectToMove.position = Vector3.Lerp(m1, m2, count);
        //    yield return new WaitForFixedUpdate();
        //}
        //objectToMove.position = b;

    }

    public override void ApplyDamage()
    {
        this.GetComponent<AttackTarget>().PerformAttack(target.gameObject);
    }
}
