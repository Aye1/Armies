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

    /*public void OnTriggerEnter2D(Collider2D collision)
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
    }*/

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger 3D enter");
        GameObject otherObject = other.gameObject;
        AttackableEntity otherEntity = otherObject.GetComponent<AttackableEntity>();
        if (otherEntity != null)
        {
            // Ally or enemy?
            bool isAlly = otherEntity.faction == soldier.faction;
            List<AttackableEntity> list = isAlly ? soldier.nearbyAllies : soldier.targets;
            if (!list.Contains(otherEntity))
            {
                list.Add(otherEntity);
            }
            if(!isAlly)
            {
                otherEntity.attackers.Add(soldier);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        GameObject otherObject = other.gameObject;
        AttackableEntity otherEntity = otherObject.GetComponent<AttackableEntity>();
        if (otherEntity != null)
        {
            bool isAlly = otherEntity.faction == soldier.faction;
            List<AttackableEntity> list = !isAlly ? soldier.nearbyAllies : soldier.targets;
            if (list.Contains(otherEntity))
            {
                list.Remove(otherEntity);
            }
            if (!isAlly)
            {
                otherEntity.attackers.Remove(soldier);
            }
        }
    }
}
