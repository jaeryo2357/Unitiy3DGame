using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    GameObject Hp;
    GameObject HpText;
    GameObject enemyhp;
    public GameObject enemyUI;
    public Camera firstPersonCamera;
    public Camera overheadCamera;
    public GameObject playerButton;
    public GameObject HpUi;
    public GameObject Wall1;
    public GameObject wall3;
    public GameObject wall2;
    int playerHp;
    public bool wallbegin = true;
	// Use this for initialization
	void Start () {
        enemyhp = GameObject.Find("Hp");
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
        enemyUI.SetActive(false);
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

    public void playerHpUp(int power)
    {
        Hp.GetComponent<Image>().fillAmount += (float)power / 100;
        playerHp += power;
        HpText.GetComponent<Text>().text = playerHp + "/100";
    }

    public void lastTarget(GameObject enemy)
    {
        if (enemy.GetComponent<CharacterStatus>().HP > 0)
        {
            enemyUI.SetActive(true);
            enemyhp.GetComponent<Image>().fillAmount = (float)enemy.GetComponent<CharacterStatus>().HP / enemy.GetComponent<CharacterStatus>().MaxHP;
        }
        else
        {
            enemyUI.SetActive(false);
        }
    }
	// Update is called once per frame
	void Update () {
        if (Wall1.transform.position.y < 0.5f&&wallbegin)
        {
            Wall1.transform.Translate(0, 0.02f, 0);
            wall2.transform.Translate(0, 0.02f, 0);
            wall3.transform.Translate(0, 0.02f, 0);
        }
        else
        {
            wallbegin = false;
        }
    }
}
