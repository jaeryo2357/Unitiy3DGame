using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class potabScript : MonoBehaviour {

    CharacterStatus staus;
    public GameObject Hp;
    public GameObject canvas;
    public Camera main;
    public int num;
	// Use this for initialization
	void Start () {
        staus = GetComponent<CharacterStatus>();
        
    }

	
	// Update is called once per frame
	void Update () {
        if(main.isActiveAndEnabled)
        canvas.transform.LookAt(main.transform);

       if(staus.HP<=0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().ClearStage(num);
            Destroy(gameObject);
        }


    }
    public void onHit(int power)
    {
        staus.HP -= power;
        Hp.GetComponent<Image>().fillAmount -= (float)power/staus.MaxHP;
    }
}
