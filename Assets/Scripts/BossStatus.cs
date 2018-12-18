using UnityEngine;
using System.Collections;

public class BossStatus : MonoBehaviour {




    public int HP = 300;
    public int MaxHP = 300;

    public int Power = 30;
    public GameObject lastAttackTarget = null;
    public string characterName = "Orc";

    public bool isKockback = false;
    public bool died = false;
    public bool Attack1 = false;
    public bool Attack2=    false;
    public bool Attack3 = false;
    public bool Attack4 = false;
    public bool Attack5 = false;

}


