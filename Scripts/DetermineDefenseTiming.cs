using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetermineDefenseTiming : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private void OnEnable()
    {
        gameObject.GetComponent<BattleUnit>().timedMultiplier = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<BattleUnit>().currentMove.GetComponent<BattleMoveNode>().GetTarget().GetComponent<Animator>().Play("Defend");
            float p = gameObject.GetComponent<BattleUnit>().currentMove.GetComponent<BattleMoveNode>().GetPerc();
            if (p == -1f)
            {
                Debug.Log("using normalised");
                float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                if (normalizedTime >= 0.6f && normalizedTime < 0.7f)
                    gameObject.GetComponent<BattleUnit>().timedMultiplier = 0.5f; // critical
                if (normalizedTime >= 0.45f && normalizedTime < 0.6f || normalizedTime >= 0.7f && normalizedTime < 0.85f)
                    gameObject.GetComponent<BattleUnit>().timedMultiplier = 0.75f; // good
            }
            else if (p < 0.5f)
            {
                gameObject.GetComponent<BattleUnit>().timedMultiplier = 1f;
            }
            else if (p >= 0.5f && p < 0.8f)
            {
                gameObject.GetComponent<BattleUnit>().timedMultiplier = 0.75f; // good
            }
            else if (p >= 0.8f)
                gameObject.GetComponent<BattleUnit>().timedMultiplier = 0.5f; // critical

         //   Debug.Log(gameObject.GetComponent<BattleUnit>().timedMultiplier);

        }
    }

    public void OnDisable()
    {
        gameObject.GetComponent<BattleUnit>().currentMove.GetComponent<BattleMoveNode>().ApplyDamage();
    }
}
