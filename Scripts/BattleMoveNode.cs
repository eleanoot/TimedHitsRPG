using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMoveNode : MonoBehaviour
{
    public int frameDuration = 10;
    protected int framesProgress = 0;

    // Nodes that should fire as soon as this node is reached i.e. instantly
    public List<GameObject> activateInstant = new List<GameObject>();

    // Nodes that should fire when this node's action has completed. 
    public List<GameObject> activateFinished = new List<GameObject>();

    [HideInInspector]
    public bool isFinished;

    public struct FireArguments
    {
        public BattleUnit actor;
        public BattleUnit target;
        public List<BattleMoveNode> activeNodes; // mainly used by the root to track all active nodes.

        public FireArguments(BattleUnit actor, BattleUnit target, List<BattleMoveNode> activeNodes)
        {
            this.actor = actor;
            this.target = target;
            this.activeNodes = activeNodes;
        }
    }

    protected FireArguments combatData;
    
    public BattleUnit GetTarget()
    {
        return combatData.target;
    }

    public virtual void CreateChildren(GameObject parent, GameObject owner)
    {
        for (int i = 0; i < activateInstant.Count; i++)
        {
            activateInstant[i] = Instantiate(activateInstant[i], activateInstant[i].transform.position, Quaternion.identity, parent.transform);
            if (activateInstant[i].GetComponent<AttackTarget>() != null)
            {
                activateInstant[i].GetComponent<AttackTarget>().owner = owner;
            }
            activateInstant[i].GetComponent<BattleMoveNode>().CreateChildren(activateInstant[i], owner);
        }

        for (int i = 0; i < activateFinished.Count; i++)
        {
            activateFinished[i] = Instantiate(activateFinished[i], activateFinished[i].transform.position, Quaternion.identity, parent.transform);
            if (activateFinished[i].GetComponent<AttackTarget>() != null)
            {
                activateFinished[i].GetComponent<AttackTarget>().owner = owner;
            }
            activateFinished[i].GetComponent<BattleMoveNode>().CreateChildren(activateFinished[i], owner);
        }

    }

    public virtual void Fire(FireArguments args)
    {
        this.gameObject.SetActive(true);
        // Debug.Log(gameObject.name);
        isFinished = false;
        combatData = args;
        combatData.activeNodes.Add(this);
        framesProgress = 0;
        
        foreach (GameObject n in activateInstant)
            n.GetComponent<BattleMoveNode>().Fire(args);

    }

    public void SignalFinished()
    {
        foreach (GameObject n in activateFinished)
            n.GetComponent<BattleMoveNode>().Fire(combatData);
        isFinished = true;
        combatData.activeNodes.Remove(this);
    }

    public virtual void ApplyDamage()
    {
        return;
    }

    public virtual float GetPerc()
    {
        return -1;
    }

    IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b)
    {
        float step = (10.0f / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = b;
    }

}
