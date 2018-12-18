using UnityEngine;
using System.Collections;

public class BossAnimation : MonoBehaviour
{

    Animator animator;
    BossStatus status;
    Vector3 prePosition;

    bool isDown = false;
    bool attacked = false;
    bool Attacking5 = false;
    bool attacked5=false;
    bool Attacking = false;
    public bool isKockdown=false;

    public bool IsAttacking()
    {
        return Attacking;
    }
    public bool IsAttacked()
    {
        return attacked;
    }

    public bool IsAttacking5()
    {
        return Attacking5;
    }
    public bool IsAttacked5()
    {
        return attacked5;
    }
    void StartAttackHit()
    {
        Attacking = true;
        Attacking5 = true;
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
        attacked5 = true;
        Attacking5 = false;
    }

    void EndHit()
    {
        isKockdown = false;
    }
  

    void Start()
    {
        animator = GetComponent<Animator>();
        status = GetComponent<BossStatus>();
   
        prePosition = transform.position;
    }
    // Use this for initialization


    // Update is called once per frame
    void Update()
    {

        Vector3 delta_position = transform.position - prePosition;
        if(!status.Attack5)
        animator.SetFloat("speed", delta_position.magnitude / Time.deltaTime);
        else
            animator.SetFloat("speed", 0.0f);
        if (attacked && !status.Attack1)
        {
            attacked = false;
        }

        if (attacked5 && !status.Attack5)
        {
            attacked5 = false;
        }


        if(Random.Range(0,3)<1)
        animator.SetBool("Attack1", (!attacked && status.Attack1));
        else
            animator.SetBool("Attack3", (!attacked && status.Attack1));
        animator.SetBool("Attack5", (!attacked5 && status.Attack5));
        if (!isDown && status.died)
        {
            isDown = true;
            animator.SetTrigger("Down");
        }

        prePosition = transform.position;
    }
}
