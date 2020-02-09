using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnit : MonoBehaviour
{
    private GameObject currentUnit;

    [SerializeField]
    private GameObject actionsMenu, enemyUnitsMenu;
    
    public void selectCurrentUnit(GameObject unit)
    {
        Debug.Log(string.Format("selecting {0}", unit.name));
        this.currentUnit = unit;
        this.actionsMenu.SetActive(true);
        //this.currentUnit.GetComponent<PlayerUnitAction>().updateHUD();
    }

    public void selectAttack(bool physical)
    {
        this.currentUnit.GetComponent<PlayerUnitAction>().selectAttack(physical);
       // Debug.Log(string.Format("attack: physical = {0}", physical));
        this.actionsMenu.SetActive(false);
        this.enemyUnitsMenu.SetActive(true);
    }

    public void attackEnemyTarget(GameObject target)
    {
        this.actionsMenu.SetActive(false);
        this.enemyUnitsMenu.SetActive(false);
        this.currentUnit.GetComponent<PlayerUnitAction>().act(target.GetComponent<BattleUnit>());

    }
}
