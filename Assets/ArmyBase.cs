using UnityEngine;

public class ArmyBase : AttackableEntity {

    public Soldier soldier;
    public GameObject spawn;
    public GameObject soldierContainer;
    public ArmyBase enemyBase;

    public override void Init()
    {
    }

    public void SendSoldier()
    {
        Soldier newSoldier = Instantiate(soldier);
        newSoldier.gameObject.SetActive(true);
        newSoldier.faction = faction;
        newSoldier.transform.position = spawn.transform.position;
        newSoldier.transform.SetParent(soldierContainer.transform);
        newSoldier.transform.localScale = Vector3.one;
        newSoldier.SetGoal(enemyBase.transform.position);
    }
}
