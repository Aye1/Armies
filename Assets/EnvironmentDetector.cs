using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDetector : MonoBehaviour {

    private Soldier soldier;

	// Use this for initialization
	void Start () {
        soldier = GetComponentInParent<Soldier>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObject = collision.gameObject;
        Soldier otherSoldier = otherObject.GetComponent<Soldier>();
        if (otherSoldier != null)
        {
            // Ally or enemy?
            List<Soldier> list = otherSoldier.faction == soldier.faction ? soldier.nearbyAllies : soldier.targets;
            if (!list.Contains(otherSoldier))
            {
                list.Add(otherSoldier);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject otherObject = collision.gameObject;
        Soldier otherSoldier = otherObject.GetComponent<Soldier>();
        if (otherSoldier != null)
        {
            List<Soldier> list = otherSoldier.faction == soldier.faction ? soldier.nearbyAllies : soldier.targets;
            if (list.Contains(otherSoldier))
            {
                list.Remove(otherSoldier);
            }
        }
    }
}
