using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    GameObject Hp;
    GameObject HpText;
    int playerHp;
	// Use this for initialization
	void Start () {
        Hp = GameObject.Find("Hpbar");
        HpText = GameObject.Find("HpbarText");
        playerHp = 100;
	}
	

    public void playerAttacked(int power)
    {
        Hp.GetComponent<Image>().fillAmount -= (float)power / 100;
        playerHp -= power;
        HpText.GetComponent<Text>().text = playerHp + "/100";
    }
	// Update is called once per frame
	void Update () {
	
	}
}
