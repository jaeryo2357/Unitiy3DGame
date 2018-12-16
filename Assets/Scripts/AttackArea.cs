using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {

    CharacterStatus status;
    BossStatus bossstatus;

    bool QAttack = false;
    bool WAttack = false;
    int QDamege = 0;
	// Use this for initialization
	void Start () {
        status = transform.root.GetComponent<CharacterStatus>();
        bossstatus = transform.root.GetComponent<BossStatus>();

    }
	public class AttackInfo
    {
        public int attackPower;
        public bool isSkillQ;
        public bool isSkillW;
        public Transform attacker;
    }

    AttackInfo GetAttackInfo()
    {
        AttackInfo attackInfo = new AttackInfo();

        if (!QAttack&&!WAttack)
        {
            if (status != null)
                attackInfo.attackPower = status.Power;
            else
                attackInfo.attackPower = bossstatus.Power;
            attackInfo.attacker = transform.root;
            attackInfo.isSkillQ = false;
            attackInfo.isSkillW = false;
        }
        else if(QAttack)
        {
            attackInfo.attackPower = QDamege;
            attackInfo.attacker = transform.root;
            attackInfo.isSkillQ = true;
            attackInfo.isSkillW = false;
            QAttack = false;
            QDamege = 0;
        }
        else
        {
            attackInfo.attackPower = status.Power+5;
            attackInfo.attacker = transform.root;
            attackInfo.isSkillQ = false;
            attackInfo.isSkillW = true;
            WAttack = false;
            QDamege = 0;

        }
        return attackInfo;
    }

   
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");
            other.SendMessage("Damage", GetAttackInfo());
        if (status != null)
            status.lastAttackTarget = other.transform.root.gameObject;
      else
            bossstatus.lastAttackTarget = other.transform.root.gameObject;
        if(other.transform.root.gameObject.tag !="Player")
        GameObject.Find("GameManager").GetComponent<GameManager>().lastTarget(other.transform.root.gameObject);

    }
    public void SendQgage(float gage)
    {
        QAttack = true;
        QDamege = (int)gage;
    }
    public void QEnd()
    {
        QAttack = false;
    }
    public void SendW(bool Wpower)
    {
        WAttack = Wpower;
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
