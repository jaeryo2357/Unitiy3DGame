using UnityEngine;
using System.Collections;

public class HitArea : MonoBehaviour {

    public GameObject particle;
    void Damage(AttackArea.AttackInfo attackInfo)
    {
        if (attackInfo.isSkillQ)
            Instantiate(particle, transform.position,
                transform.rotation);
        else if (attackInfo.isSkillW)
        {
            transform.root.GetComponent<Animator>().SetBool("Kockback", true);
            if (transform.root.GetComponent<CharaAnimation>() != null)
                transform.root.GetComponent<CharaAnimation>().isKockdown = true;
            else
            {
                transform.root.GetComponent<BossAnimation>().isKockdown = true;
            }
        }
        else
        {
            if (GetComponent<AudioSource>() != null)
                GetComponent<AudioSource>().Play();
            if (transform.root.GetComponent<BossStatus>() != null)
            {
                if(!transform.root.GetComponent<BossStatus>().Attack1&&
                    !transform.root.GetComponent<BossStatus>().Attack2&&
                    !transform.root.GetComponent<BossStatus>().Attack3&&
                    !transform.root.GetComponent<BossStatus>().Attack4&&
                    !transform.root.GetComponent<BossStatus>().Attack5)
                transform.root.GetComponent<Animator>().SetBool("Damage", true);
            }
        }
        transform.root.SendMessage("Damage", attackInfo);
    }
}
