using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDamageText : MonoBehaviour
{
    [SerializeField]
    private float destroyFloat;

    // Start is called before the first frame update
    void Start()
    {
       Destroy(this.gameObject, this.destroyFloat);
    }

    private void OnDestroy()
    {
        GameObject turnSystem = GameObject.Find("TurnSystem");
        turnSystem.GetComponent<TurnSystem>().NextTurn();
    }
}
