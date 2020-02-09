using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyUnitAction : MonoBehaviour
{

    [SerializeField]
    private GameObject attack;

    [SerializeField]
    private string targetsTag;

    void Awake()
    {
        this.attack = Instantiate(this.attack, this.transform) as GameObject;
        this.attack.GetComponent<BattleMoveNode>().CreateChildren(this.attack, this.gameObject);
    }

    GameObject findRandomTarget()
    {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(targetsTag);

        if (possibleTargets.Length > 0)
        {
            int targetIndex = Random.Range(0, possibleTargets.Length);
            GameObject target = possibleTargets[targetIndex];

            return target;
        }

        return null;
    }

    public void act()
    {
        GameObject target = findRandomTarget();
        this.attack.GetComponent<BattleMoveNode>().Fire(new BattleMoveNode.FireArguments(this.gameObject.GetComponent<BattleUnit>(), target.GetComponent<BattleUnit>(), new List<BattleMoveNode>()));
    }

    public void StartTiming()
    {
        gameObject.GetComponent<DetermineDefenseTiming>().enabled = true;
    }

    public void FinishTiming()
    {
        gameObject.GetComponent<DetermineDefenseTiming>().enabled = false;
    }

}
