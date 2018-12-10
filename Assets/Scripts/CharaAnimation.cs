using UnityEngine;
using System.Collections;

public class CharaAnimation : MonoBehaviour {

    Animator animator;
    CharacterStatus status;
    Vector3 prePosition;
    bool isDown=false;
    bool attacked = false;
    bool attackedQ = false;
    bool SkillW = false;
    bool Attacking = false;

    public bool IsAttacking()
    {
        return Attacking;
    }
    public bool IsAttacked()
    {
        return attacked;
    }
    public bool IsSkillQ()
    {
        return attackedQ;
    }
    public bool IsSkillW()
    {
        return SkillW;
    }
    void StartAttackHit()
    {
        Attacking = true;
        Debug.Log("StartAttackHit");
    }
    void EndAttackHit()
    {
        Debug.Log("EndAttackHit");
        
    }

    void EndAttack()
    {
        attacked = true;
        Attacking = false;
    }
    void EndAttackQ()
    {
        attackedQ = true;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        status = GetComponent<CharacterStatus>();

        prePosition = transform.position;
    }
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {

        Vector3 delta_position = transform.position - prePosition;
        animator.SetFloat("speed", delta_position.magnitude / Time.deltaTime);

        if(attacked && !status.attacking)
        {
            attacked = false;
        }

        if (attackedQ && !status.SkillQ)
            attackedQ = false;

        if (SkillW && !status.SkillW)
            SkillW = false;

        animator.SetBool("Attacking", (!attacked && status.attacking));
        animator.SetBool("SkillQ", (!attackedQ && status.SkillQ));
        animator.SetBool("SkillW", (!SkillW && status.SkillW));
        if(!isDown&&status.died)
        {
            isDown = true;
            animator.SetTrigger("Down");
        }

        prePosition = transform.position;
	}
}
