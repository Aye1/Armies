using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifebarManager : MonoBehaviour {

    public Lifebar lifebar;
    public Dictionary<AttackableEntity, Lifebar> lifebars;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(AttackableEntity entity in lifebars.Keys)
        {
            Lifebar currentLifebar;
            lifebars.TryGetValue(entity, out currentLifebar);
            if (currentLifebar != null)
            {
                MapValues(entity, currentLifebar);
                SetLifebarPosition(entity, currentLifebar);
            }
        }
    }

    public void AddEntity(AttackableEntity entity)
    {
        if(lifebars == null)
        {
            lifebars = new Dictionary<AttackableEntity, Lifebar>();
        }
        if(!lifebars.ContainsKey(entity)) 
        {
            Lifebar newLifebar = Instantiate(lifebar);
            MapValues(entity, newLifebar);
            SetLifebarPosition(entity, newLifebar);
            newLifebar.transform.SetParent(transform);
            lifebars.Add(entity, newLifebar);
        }
    }

    public void RemoveEntity(AttackableEntity entity)
    {
        if(lifebars != null && lifebars.ContainsKey(entity))
        {
            Lifebar currentLifeBar;
            lifebars.TryGetValue(entity, out currentLifeBar);
            if(currentLifeBar != null)
            {
                Destroy(currentLifeBar.gameObject);
            }
            lifebars.Remove(entity);
        }
    }

    private void MapValues(AttackableEntity entity, Lifebar lb)
    {
        lb.CurrentHealth = entity.CurrentHP;
        lb.MaxHealth = entity.maxHP;
    }

    private void SetLifebarPosition(AttackableEntity entity, Lifebar lb)
    {
        Vector3 offset = new Vector3(0.0f, 5.0f, 0.5f);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(entity.transform.position + offset);
        lb.transform.position = screenPos;
    }
}
