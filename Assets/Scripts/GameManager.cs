using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    GameObject Hp;
    GameObject HpText;
    GameObject enemyhp;
    GameObject SkillQ;
    GameObject SkillE;
    public GameObject enemyUI;
    public Camera firstPersonCamera;
    public Camera overheadCamera;
    public GameObject playerButton;
    public GameObject HpUi;
    public GameObject Wall1;
    public GameObject wall3;
    public GameObject wall2;
    public Sprite wolf;
    public Sprite heo;
    public Sprite dragon;
    public Sprite boss;
    int playerHp;
    public bool wallbegin = true;
	// Use this for initialization
	void Start () {
        enemyhp = GameObject.Find("Hp");
        Hp = GameObject.Find("Hpbar");
        HpText = GameObject.Find("HpbarText");
        SkillQ = GameObject.Find("SkillQ");
        SkillE = GameObject.Find("SkillE");
        SkillE.GetComponent<Image>().fillAmount = 0;
        SkillQ.GetComponent<Image>().fillAmount = 0;
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
        CharacterStatus statuse = enemy.GetComponent<CharacterStatus>();
        if (statuse != null)
        {
            if (statuse.characterName.Equals("Dragon"))
                enemyUI.GetComponent<Image>().sprite = dragon;
            else if(statuse.characterName.Equals("Warg"))
                enemyUI.GetComponent<Image>().sprite = wolf;
            else if (statuse.characterName.Equals("Skeleton"))
                enemyUI.GetComponent<Image>().sprite = heo;
           




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
        else
        {
            BossStatus statusb = enemy.GetComponent<BossStatus>();
            enemyUI.GetComponent<Image>().sprite = boss;


            if (statusb.HP > 0)
            {
                enemyUI.SetActive(true);
                enemyhp.GetComponent<Image>().fillAmount = (float)statusb.HP /statusb.MaxHP;
            }
            else
            {
                enemyUI.SetActive(false);
            }
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
        if (SkillQ.GetComponent<Image>().fillAmount > 0)
            SkillQ.GetComponent<Image>().fillAmount -= 0.02f;
        if (SkillE.GetComponent<Image>().fillAmount > 0)
            SkillE.GetComponent<Image>().fillAmount -= 0.03f;



    }
}
