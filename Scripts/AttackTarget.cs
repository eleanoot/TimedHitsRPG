using UnityEngine;
using System.Collections;

public class AttackTarget : MonoBehaviour
{

    public GameObject owner;

    [SerializeField]
    private string attackAnimation;
    [SerializeField]
    private string idleAnimation;

    [SerializeField]
    private bool magicAttack;

    [SerializeField]
    private float manaCost;

    public void hit(GameObject target)
    {
        this.owner.GetComponent<BattleUnit>().currentMove = this.gameObject;
        this.owner.GetComponent<Animator>().Play(this.attackAnimation);
    }

    public void PerformAttack(GameObject target)
    {
        BattleUnit ownerStats = this.owner.GetComponent<BattleUnit>();
        BattleUnit targetStats = target.GetComponent<BattleUnit>();

        float damage = ownerStats.attack * ownerStats.timedMultiplier;
        Debug.Log(damage);
        targetStats.receiveDamage(damage, ownerStats.timedMultiplier);
        if (owner.tag == "PlayerUnit" || owner.tag == "EnemyUnit" && ownerStats.timedMultiplier == 1f)
            target.GetComponent<Animator>().Play("Hit");
        this.owner.GetComponent<BattleUnit>().currentMove.GetComponent<BattleMoveNode>().SignalFinished();
        this.owner.GetComponent<Animator>().Play(this.idleAnimation);

    }
}
