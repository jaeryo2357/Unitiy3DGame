using UnityEngine;
using System.Collections;

public class SearchArea : MonoBehaviour {
	EnemyCtrl enemyCtrl;
    BossController bossCtrl;
	void Start()
	{
		// EnemyCtrl을 미리 준비한다.
		enemyCtrl = transform.root.GetComponent<EnemyCtrl>();
        bossCtrl = transform.root.GetComponent<BossController>();
	}
	
	void OnTriggerStay( Collider other )
	{
        // Player태그를 타깃으로 한다.
        if (other.tag == "Player")
        {
            if (enemyCtrl != null)
                enemyCtrl.SetAttackTarget(other.transform);
            else
                bossCtrl.SetAttackTarget(other.transform);
        }
	}
}
