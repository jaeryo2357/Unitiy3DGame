using UnityEngine;
using System.Collections;

public class AppleScripts : MonoBehaviour {

    GameObject Manager;
    public int Hp = 10;
	// Use this for initialization
	void Start () {
        Manager = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Manager.GetComponent<GameManager>().playerHpUp(Hp);
        }
        Destroy(gameObject);
    }
}
