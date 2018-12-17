using UnityEngine;
using System.Collections;

public class HitArea : MonoBehaviour {

    public GameObject particle;
    void Damage(AttackArea.AttackInfo attackInfo)
    {
        if (attackInfo.isSkillQ)
            Instantiate(particle, transform.position,
                transform.rotation);
        else if(attackInfo.isSkillW)
        {
            transform.root.GetComponent<Animator>().SetBool("Kockback", true);
            transform.root.GetComponent<CharaAnimation>().isKockdown = true;
        }
        else
        {
            transform.root.GetComponent<Animator>().SetBool("Damage", true);
        }
        transform.root.SendMessage("Damage", attackInfo);
    }
}
