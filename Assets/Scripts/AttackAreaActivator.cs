using UnityEngine;
using System.Collections;

public class AttackAreaActivator : MonoBehaviour {

    Collider[] attackAreaColiders;
	// Use this for initialization
	void Start () {
        AttackArea[] attackAreas = GetComponentsInChildren<AttackArea>();
        attackAreaColiders = new Collider[attackAreas.Length];

        for(int attackAreaCnt=0;attackAreaCnt<attackAreas.Length;attackAreaCnt++)
        {
           attackAreaColiders[attackAreaCnt] = attackAreas[attackAreaCnt].GetComponent<Collider>();

            attackAreaColiders[attackAreaCnt].enabled = false;
        }
	}

    void StartAttackHit()
    {
        foreach (Collider attackAreaCollider in attackAreaColiders)
            attackAreaCollider.enabled = true;
    }
    void EndAttackHit()
    {
        foreach (Collider attackAreaCollider in attackAreaColiders)
            attackAreaCollider.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
