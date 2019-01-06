using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Soldier : MonoBehaviour {

    public enum Faction { Fac1, Fac2 };
    private static Color Fac1Color = Color.red;
    private static Color Fac2Color = Color.blue;

    public Stack<Vector3> goals;
    public float maxDistanceDelta = 0.1f;
    public int maxHP = 100;
    public int currentHP = 100;
    public List<Soldier> targets;
    public List<Soldier> nearbyAllies;
    public Faction faction;

    private Lifebar lifebar;

	// Use this for initialization
	void Start () {
        goals = new Stack<Vector3>();
        goals.Push(new Vector3(0.0f, 0.0f, transform.position.z));
        targets = new List<Soldier>();
        lifebar = GetComponentInChildren<Lifebar>();
	}
	
	// Update is called once per frame
	void Update () {
        MoveTowardsGoal();
        UpdateLifeBar();
        UpdateSoldierColor();
	}

    private void MoveTowardsGoal()
    {
        if (goals != null && goals.Count > 0 && targets.Count == 0)
        {
            Vector3 goal = goals.Peek();
            if (!IsAtGoal(goal))
            {
                if (IsAllyAhead(goal))
                {
                    Debug.Log("Ally ahead");
                }
                Vector3 nextPosition = FindNextPosition(goal);
                Debug.DrawLine(transform.position, nextPosition);
                transform.position = new Vector3(nextPosition.x, nextPosition.y, transform.position.z);
            }
            else
            {
                Debug.Log("Goal reached, popping");
                goals.Pop();
            }
        }
    }

    private Vector3 FindNextPosition(Vector3 goal, int depth=0)
    {
        Debug.Log("Finding next position - Depth " + depth.ToString());
        if(depth >= 10)
        {
            Debug.Log("Could not find next position");
            return transform.position;
        }
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, goal, maxDistanceDelta);
        if(CanGoToPosition(nextPosition))
        {
            return nextPosition;
        }

       /*Vector3 move = nextPosition - transform.position;
        Vector3 leftPos = Quaternion.Euler(0.0f, 0.0f, -45.0f) * move + transform.position; 
       //Vector3 rightPos = Quaternion.Euler(0.0f, 0.0f, 45.0f) * move;
        if(CanGoToPosition(leftPos))
        {
            return leftPos;
        }
        /*if (CanGoToPosition(rightPos))
        {
            return rightPos;
        }
        return FindNextPosition(leftPos, depth+1);*/
        return transform.position;
    }

    private bool CanGoToPosition(Vector3 pos)
    {
        bool canGoToNextPosition = true;
        foreach (Soldier s in nearbyAllies)
        {
            if (Vector3.Distance(s.transform.position, pos) <= 10)
            {
                canGoToNextPosition = false;
            }
        }
        return canGoToNextPosition;
    }

    private bool IsAtGoal(Vector3 goal)
    {
        return Vector3.Distance(goal, transform.position) < Mathf.Epsilon;
    }

    private bool IsAllyAhead(Vector3 goal)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.Normalize(goal - transform.position), 3.0f);
        if(hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            GameObject obj = hit.collider.gameObject;
            Soldier otherSoldier = obj.GetComponent<Soldier>();
            if(otherSoldier != null && otherSoldier.faction == faction)
            {
                return true;
            }
        }
        return false;
    }

    private void FindIntermediateGoal()
    {

    }


    public void Damage(int dmg)
    {
        if (currentHP - dmg <= 0)
        {
            Die();
        }
        else
        {
            currentHP -= dmg;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void UpdateLifeBar()
    {
        if (lifebar != null)
        {
            lifebar.CurrentHealth = currentHP;
        }
    }

    private void UpdateSoldierColor()
    {
        GetComponent<Image>().color = faction == Faction.Fac1 ? Fac1Color : Fac2Color;
    }


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
