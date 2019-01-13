using UnityEngine;

public class ArmyBase : AttackableEntity {

    public Soldier soldier;
    public GameObject spawn;
    public GameObject soldierContainer;
    public ArmyBase enemyBase;
    public Material soldierMaterial;

    protected override void Init()
    {
        FindObjectOfType<LifebarManager>().AddEntity(this);
    }

    protected override void SpecificUpdate()
    {
    }

    public void SendSoldier()
    {
        Soldier newSoldier = Instantiate(soldier);
        newSoldier.gameObject.SetActive(true);
        newSoldier.faction = faction;
        newSoldier.transform.position = spawn.transform.position;
        newSoldier.transform.SetParent(soldierContainer.transform);
        //newSoldier.transform.localScale = Vector3.one;
        newSoldier.GetComponent<MeshRenderer>().material = soldierMaterial;
        if (enemyBase != null)
        {
            newSoldier.SetGoal(enemyBase.transform.position);
        }
        FindObjectOfType<LifebarManager>().AddEntity(newSoldier);
    }
}
