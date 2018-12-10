using UnityEngine;
using System.Collections;

public class HitArea : MonoBehaviour {

    public GameObject particle;
    void Damage(AttackArea.AttackInfo attackInfo)
    {
        if (attackInfo.isSkillQ)
            Instantiate(particle, transform.position,
                transform.rotation);
        transform.root.SendMessage("Damage", attackInfo);
    }
}
