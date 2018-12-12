using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    GameObject Hp;
    GameObject HpText;
    public Camera firstPersonCamera;
    public Camera overheadCamera;
    public GameObject playerButton;
    public GameObject HpUi;
    int playerHp;
	// Use this for initialization
	void Start () {
        Hp = GameObject.Find("Hpbar");
        HpText = GameObject.Find("HpbarText");
        playerHp = 100;
        ShowOverheadView();
      
	}
    public void ShowOverheadView()
    {
        firstPersonCamera.enabled = false;
        overheadCamera.enabled = true;
        playerButton.SetActive(true);
        HpUi.SetActive(false);
    }

    public void ShowFirstPersonView()
    {
        firstPersonCamera.enabled = true;
        overheadCamera.enabled = false;
        playerButton.SetActive(false);
        HpUi.SetActive(true);
    }

    public void GamePlay()
    {
        ShowFirstPersonView();
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
