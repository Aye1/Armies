using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyBase : MonoBehaviour {

    public Soldier soldier;
    public GameObject spawn;
    public GameObject soldierContainer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendSoldier()
    {
        Soldier newSoldier = Instantiate(soldier);
        newSoldier.faction = Soldier.Faction.Fac2;
        newSoldier.transform.position = spawn.transform.position;
        newSoldier.transform.SetParent(soldierContainer.transform);
        newSoldier.transform.localScale = Vector3.one;

    }
}
