﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

	Vector2 slideStartPosition;
	Vector2 prevPosition;
	Vector2 delta=Vector2.zero;
	bool moved=false;
    bool Q = false;
    bool W = false;
	// Use this for initialization
	void Start () {
	
	}
    public bool isQ()
    {
        return Q;
    }

    public bool isW()
    {
        return W;
    }
    public void setW(bool result)
    {
        W = result;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1"))
			slideStartPosition = GetCursorPosition ();

		if (Input.GetButton ("Fire1")) {
            if (Vector2.Distance(slideStartPosition, GetCursorPosition()) >= (Screen.width * 0.1f))
                moved = true;
		}

        if (Input.GetKeyDown(KeyCode.E)&&GameObject.Find("SkillE").GetComponent<Image>().fillAmount<=0)
        {
            W = true;
            GameObject.Find("SkillE").GetComponent<Image>().fillAmount = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Q)&& GameObject.Find("SkillQ").GetComponent<Image>().fillAmount <= 0)
        {
            Q = true;
            GameObject.Find("SkillQ").GetComponent<Image>().fillAmount = 1;
        }
        

        

        if(Input.GetKeyUp(KeyCode.Q))
        {
            Q = false;
        }

		if (!Input.GetButtonUp ("Fire1")&&!Input.GetButton("Fire1")) {		
				moved = false;
		}

		if (moved)
			delta = GetCursorPosition () - prevPosition;
		else
			delta = Vector2.zero;

        prevPosition = GetCursorPosition();

	}

  

	public bool Clicked(){

		if (!moved && Input.GetButtonUp("Fire1"))
			return true;
		else
			return false;
	}

	public Vector2 GetDeltaPosition(){

		return delta;
	}


	public bool Moved(){

		return moved;
	}



	public Vector2 GetCursorPosition()
	{

		return Input.mousePosition;
	}
}
