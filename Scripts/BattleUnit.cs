using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour, IComparable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject damageTextPrefab;
    [SerializeField]
    private Vector2 damageTextPosition;

    public float health;
    public float mana;
    public float attack;
    public float magic;
    public float defense;
    public float speed;

    public float timedMultiplier;

    public int nextActTurn;

    private bool dead = false;

    public GameObject currentMove;

    public Vector2 startingPos;

    public void Awake()
    {
        startingPos = gameObject.transform.position;
    }

    public void calculateNextActTurn(int currentTurn)
    {
        this.nextActTurn = currentTurn + (int)Mathf.Ceil(100.0f / this.speed);
    }

    public int CompareTo(object otherStats)
    {
        return nextActTurn.CompareTo(((BattleUnit)otherStats).nextActTurn);
    }

    public bool isDead()
    {
        return this.dead;
    }

    public void FireFinished()
    {
        currentMove.GetComponent<BattleMoveNode>().SignalFinished();
    }

    public void receiveDamage(float damage, float multi)
    {
        this.health -= damage;
        // animator.Play("Hit");

        GameObject HUDCanvas = GameObject.Find("HUD");
        GameObject damageText = Instantiate(this.damageTextPrefab, HUDCanvas.transform) as GameObject;
        damageText.GetComponent<Text>().text = "" + damage;
        damageText.transform.localPosition = this.damageTextPosition;
        damageText.transform.localScale = new Vector2(1.0f, 1.0f);

        //  if (timedMultiplier > 1f)
        //{
            //GameObject multiText = Instantiate(this.damageTextPrefab, HUDCanvas.transform) as GameObject;
            switch (multi)
            {
                case 1.5f: // attack
                case 0.75f: // defend
                    //multiText.GetComponent<Text>().text = "Good!";
                    damageText.GetComponent<Text>().text += "\nGood!";
                    break;
                case 2f: //attack
                case 0.5f: // defend
                   // multiText.GetComponent<Text>().text = "Critical!!";
                    damageText.GetComponent<Text>().text += "\nCritical!!";
                    break;
                default:
                   // multiText.GetComponent<Text>().text = "Okay";
                    damageText.GetComponent<Text>().text += "\nOkay";
                    break;

            }

            //multiText.transform.localPosition = this.damageTextPosition + new Vector2(0, 20);
            //multiText.transform.localScale = new Vector2(1.0f, 1.0f);

            //}

            // show damage text based on the timed multiplier for critical/good



            if (this.health <= 0)
        {
            this.dead = true;
            this.gameObject.tag = "DeadUnit";
            Destroy(this.gameObject);
        }
    }

}
