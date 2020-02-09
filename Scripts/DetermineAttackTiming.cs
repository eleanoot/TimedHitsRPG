using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetermineAttackTiming : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public void PrintTime()
    {
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    private void OnEnable()
    {
        gameObject.GetComponent<BattleUnit>().timedMultiplier = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (normalizedTime >= 0.6f && normalizedTime < 0.7f)
                gameObject.GetComponent<BattleUnit>().timedMultiplier = 2.0f; // critical
            if (normalizedTime >= 0.45f && normalizedTime < 0.6f || normalizedTime >= 0.7f && normalizedTime < 0.85f)
                gameObject.GetComponent<BattleUnit>().timedMultiplier = 1.5f; // good

        }
    }

    public void OnDisable()
    {
        gameObject.GetComponent<BattleUnit>().currentMove.GetComponent<BattleMoveNode>().ApplyDamage();
    }
}
