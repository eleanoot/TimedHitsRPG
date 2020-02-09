using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    private List<BattleUnit> unitStats;

    private GameObject playerParty;

    public GameObject enemyEncounter;

    [SerializeField]
    private Text battleWon;
    [SerializeField]
    private Text battleLoss;

    // Start is called before the first frame update
    void Start()
    {
        this.playerParty = GameObject.Find("PlayerParty");

        unitStats = new List<BattleUnit>();
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");

        foreach (GameObject unit in playerUnits)
        {
            BattleUnit currentUnitStats = unit.GetComponent<BattleUnit>();
            currentUnitStats.calculateNextActTurn(0);
            unitStats.Add(currentUnitStats);
        }

        GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject unit in enemyUnits)
        {
            BattleUnit currentUnitStats = unit.GetComponent<BattleUnit>();
            currentUnitStats.calculateNextActTurn(0);
            unitStats.Add(currentUnitStats);
        }

        unitStats.Sort();

        this.NextTurn();
    }

    public void NextTurn()
    {
        Debug.Log("next turn");
        GameObject[] remainingEnemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
        if (remainingEnemyUnits.Length == 0)
        {
            battleWon.gameObject.SetActive(true);
            Debug.Log("Battle over, player won");
            return;
        }

        GameObject[] remainingPlayerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        if (remainingPlayerUnits.Length == 0)
        {
            battleLoss.gameObject.SetActive(true);
            Debug.Log("Battle over, player lost");
            return;
        }

        BattleUnit currentUnitStats = unitStats[0];
        unitStats.Remove(currentUnitStats);

        if (!currentUnitStats.isDead())
        {
            GameObject currentUnit = currentUnitStats.gameObject;
            Debug.Log(currentUnit);
            currentUnitStats.calculateNextActTurn(currentUnitStats.nextActTurn);
            unitStats.Add(currentUnitStats);
            unitStats.Sort();

            if (currentUnit.tag == "PlayerUnit")
            {
                this.playerParty.GetComponent<SelectUnit>().selectCurrentUnit(currentUnit.gameObject);
            }
            else
                currentUnit.GetComponent<EnemyUnitAction>().act();
        }
        else
            this.NextTurn();
    }
}
