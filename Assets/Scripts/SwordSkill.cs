using UnityEngine;
using System.Collections;

public class SwordSkill : MonoBehaviour {

    InputManager inputManager;
    CharacterStatus status;
    public GameObject particle;
    GameObject particled;

    // Use this for initialization
    void Start () {
        inputManager = FindObjectOfType<InputManager>();
        status = transform.root.GetComponent<CharacterStatus>();
      
    }
	
	// Update is called once per frame
	void Update () {

        if (inputManager.isQ() && !status.attacking)
        {
                //  GetComponent<ParticleSystem>().Play();
                particled = Instantiate(particle, transform.position,
                    transform.rotation) as GameObject;
            
        }
        else
        {
            // GetComponent<ParticleSystem>().Stop();
            Destroy(particled);
         
        }
    }
}
