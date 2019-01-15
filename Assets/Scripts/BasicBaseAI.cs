using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArmyBase))]
public class BasicBaseAI : MonoBehaviour {

    private ArmyBase _base;
    private float _nextTime;
    public float spawnDelay = 2.0f;
    public ArmyBase enemyBase;

	// Use this for initialization
	void Start () {
        _base = GetComponent<ArmyBase>();
	}
	
	// Update is called once per frame
	void Update () {
		if(CanSpawnSoldier())
        {
            _base.SendSoldier();
            _nextTime = Time.time + spawnDelay;
        }
    }

    private bool CanSpawnSoldier()
    {
        return Time.time > _nextTime && enemyBase != null;
    }
}
