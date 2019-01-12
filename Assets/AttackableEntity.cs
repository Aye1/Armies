﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackableEntity : MonoBehaviour {

    public enum Faction { Fac1, Fac2 };
    public int maxHP = 100;
    public int _currentHP = 100;
    public Faction faction;
    public List<AttackableEntity> attackers;
    public List<AttackableEntity> targets;
    public bool canAttack;


    protected Lifebar _lifebar;

    protected int CurrentHP
    {
        get { return _currentHP; }
        set { 
            if (value != _currentHP)
            {
                _currentHP = value;
                UpdateLifeBar();
            }
        }
    }

    public abstract void Init();

    // Use this for initialization
    void Start () {
        targets = new List<AttackableEntity>();
        attackers = new List<AttackableEntity>();
        _lifebar = GetComponentInChildren<Lifebar>();
        UpdateLifeBar();
        Init();
    }

    public void Damage(int dmg)
    {
        if (CurrentHP - dmg <= 0)
        {
            Die();
        }
        else
        {
            CurrentHP -= dmg;
        }
    }

    public void Die()
    {
        foreach (AttackableEntity s in attackers)
        {
            s.targets.Remove(this);
        }
        foreach (AttackableEntity s in targets)
        {
            s.attackers.Remove(this);
        }
        Destroy(gameObject);
    }

    private void UpdateLifeBar()
    {
        if (_lifebar != null)
        {
            _lifebar.CurrentHealth = CurrentHP;
        }
    }
}