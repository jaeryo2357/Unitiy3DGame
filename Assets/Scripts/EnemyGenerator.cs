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
            yield return new WaitForSeconds(5.0f);
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
                existEnemys[enemyCount].transform.position = new Vector3(transform.position.x+Random.Range(-10.0f,10.0f), transform.position.y, transform.position.z + Random.Range(-10.0f, 10.0f));
                return;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
