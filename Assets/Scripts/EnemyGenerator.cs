using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {

    public GameObject enemyPrefab;

    GameObject[] existEnemys;
    public int maxEnemy = 2;
	// Use this for initialization
	void Start () {

        existEnemys = new GameObject[maxEnemy];
        StartCoroutine(Exec());
	}

    IEnumerator Exec()
    {
        while(true)
        {
            Generate();
            yield return new WaitForSeconds(3.0f);
        }
    }

    void Generate()
    {
        for(int enemyCount=0;enemyCount<existEnemys.Length;++enemyCount)
        {
            if(existEnemys[enemyCount]==null)
            {
                existEnemys[enemyCount] = Instantiate(enemyPrefab, transform.position,
                    transform.rotation) as GameObject;
                return;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
