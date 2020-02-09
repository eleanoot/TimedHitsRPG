using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponJab : BattleMoveNode
{
    private BattleUnit target;
    private BattleUnit actor;
    public override void Fire(FireArguments args)
    {
        base.Fire(args);
        target = args.target;
        this.GetComponent<AttackTarget>().hit(args.target.gameObject);
        //SignalFinished();
    }

    public override void ApplyDamage()
    {
        this.GetComponent<AttackTarget>().PerformAttack(target.gameObject);
    }
    
}
