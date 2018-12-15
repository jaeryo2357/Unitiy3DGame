using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NotHit()
    {
        transform.FindChild("Hips").GetComponent<BoxCollider>().enabled = false;
    }
    public void NowHit()
    {
        transform.FindChild("Hips").GetComponent<BoxCollider>().enabled = true;
    }
}
