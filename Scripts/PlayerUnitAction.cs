using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitAction : MonoBehaviour
{
    [SerializeField]
    private GameObject physicalAttack;

    [SerializeField]
    private GameObject magicalAttack;

    private GameObject currentAttack;

    private void Awake()
    {
        this.physicalAttack = Instantiate(this.physicalAttack, this.transform) as GameObject;
        this.physicalAttack.GetComponent<BattleMoveNode>().CreateChildren(this.physicalAttack, this.gameObject);
        this.magicalAttack = Instantiate(this.magicalAttack, this.transform) as GameObject;
        this.magicalAttack.GetComponent<BattleMoveNode>().CreateChildren(this.magicalAttack, this.gameObject);

        //this.physicalAttack.SetActive(false);
        //this.magicalAttack.SetActive(false);

       // this.physicalAttack.GetComponent<AttackTarget>().owner = this.gameObject;
       // this.magicalAttack.GetComponent<AttackTarget>().owner = this.gameObject;

        this.currentAttack = this.physicalAttack;
    }

    public void selectAttack(bool physical)
    {
        this.currentAttack = (physical) ? this.physicalAttack : this.magicalAttack;
    }

    public void act(BattleUnit target)
    {
        this.currentAttack.GetComponent<BattleMoveNode>().Fire(new BattleMoveNode.FireArguments(this.gameObject.GetComponent<BattleUnit>(), target, new List<BattleMoveNode>()));
    }

    public void StartTiming()
    {
        gameObject.GetComponent<DetermineAttackTiming>().enabled = true;
    }

    public void FinishTiming()
    {
        gameObject.GetComponent<DetermineAttackTiming>().enabled = false;
    }

}
