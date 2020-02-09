using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEnemy : MonoBehaviour
{
    private GameObject[] currentEnemies;
    private int currentIndex = 0;
    void OnEnable()
    {
        currentEnemies = GameObject.FindGameObjectsWithTag("EnemyUnit");
        gameObject.transform.position = currentEnemies[currentIndex].transform.position + new Vector3(0, 1.5f, 0);
    }

    public void SelectEnemyTarget(GameObject target)
    {
        GameObject partyData = GameObject.Find("PlayerParty");
        partyData.GetComponent<SelectUnit>().attackEnemyTarget(target);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentIndex > 0)
                currentIndex--;
            else
                currentIndex = currentEnemies.Length - 1;

            gameObject.transform.position = currentEnemies[currentIndex].transform.position + new Vector3(0, 1.5f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentIndex < currentEnemies.Length - 1)
                currentIndex++;
            else
                currentIndex = 0;

            gameObject.transform.position = currentEnemies[currentIndex].transform.position + new Vector3(0, 1.5f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectEnemyTarget(currentEnemies[currentIndex]);
        }
    }
}
