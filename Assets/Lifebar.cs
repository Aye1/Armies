using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour {

    private int _maxHealth = 100;
    private int _currentHealth;
    public Image currentHealthImage;

    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            if (_currentHealth != value && value >= 0 && value <= _maxHealth)
            {
                _currentHealth = value;
                ComputeBarSize();
            }
        }
    }


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ComputeBarSize()
    {
        float ratio = _currentHealth / (float)_maxHealth;
        currentHealthImage.transform.localScale = new Vector3(ratio, 1.0f, 1.0f);
    }
}
