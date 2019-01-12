using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Soldier : AttackableEntity {

    private static Color Fac1Color = Color.red;
    private static Color Fac2Color = Color.blue;

    public Stack<Vector3> goals;
    public float maxDistanceDelta = 0.1f;
    public float attackDelay = 1.0f;
    public List<AttackableEntity> nearbyAllies;

    private float _nextTimeAttack;


    public override void Init()
    {

    }

    // Update is called once per frame
    void Update () {
        //MoveTowardsGoal();
        //UpdateLifeBar();
        //UpdateSoldierColor();
        ManageMove();
        ManageFight();
	}

    public void SetGoal(Vector3 goal)
    {
        if(goals == null)
        {
            goals = new Stack<Vector3>();
        }
        goals.Push(goal);
    }

    private void ManageMove()
    { 
        GetComponentInChildren<MoveWithNavMesh>().CanMove = targets.Count == 0;
        if(goals.Count > 0)
        {
            GetComponentInChildren<MoveWithNavMesh>().destination = goals.Peek();
        }
    }

    private void ManageFight()
    {
        if(CanAttack())
        {
            Attack();
        }
    }

    private bool CanAttack()
    {
        return targets.Count > 0 && Time.time > _nextTimeAttack;
    }

    private void Attack()
    {
        targets.ToArray()[0].Damage(10);
        _nextTimeAttack = Time.time + attackDelay;
    }

    /*private void UpdateSoldierColor()
    {
        GetComponent<Image>().color = faction == Faction.Fac1 ? Fac1Color : Fac2Color;
    }*/


#if UNITY_EDITOR
    #region Debug methods
    [MenuItem("Debug/Damage all soldiers (10)")]
    public static void DamageAllSoldiers()
    {
        foreach(Soldier s in FindObjectsOfType<Soldier>())
        {
            s.Damage(10);
        }
    }
    #endregion
#endif

}
