using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {

    CharacterStatus status;

    bool QAttack = false;
    int QDamege = 0;
	// Use this for initialization
	void Start () {
        status = transform.root.GetComponent<CharacterStatus>();
     

    }
	public class AttackInfo
    {
        public int attackPower;
        public bool isSkillQ;
        public Transform attacker;
    }

    AttackInfo GetAttackInfo()
    {
        AttackInfo attackInfo = new AttackInfo();

        if (!QAttack)
        {
            attackInfo.attackPower = status.Power;
            attackInfo.attacker = transform.root;
            attackInfo.isSkillQ = false;
        }
        else
        {
            attackInfo.attackPower = QDamege;
            attackInfo.attacker = transform.root;
            attackInfo.isSkillQ = true;
            QAttack = false;
            QDamege = 0;
        }
        return attackInfo;
    }

   
    void OnTriggerEnter(Collider other)
    {
            other.SendMessage("Damage", GetAttackInfo());
            status.lastAttackTarget = other.transform.root.gameObject;
            

    }
    public void SendQgage(float gage)
    {
        QAttack = true;
        QDamege = (int)gage;
    }

    void OnAttack()
    {
        this.GetComponent<Collider>().enabled = true;
    }
    void OnAttackTermination()
    {
        this.GetComponent<Collider>().enabled = false;
    }
    // Update is called once per frame
    void Update () {
	
	}
}
