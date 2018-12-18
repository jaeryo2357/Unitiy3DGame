using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerControl2 : MonoBehaviour {


    const float RayCastMaxDistance = 100.0f;
    CharacterStatus status;
    CharaAnimation charAnimation;
    public GameObject sword;
    InputManager inputManager;


    GameObject gameManager;

    public float attackRange = 1.5f;

    public float Qgage = 0.0f;
    enum State
    {
        Walking,
        Attacking,
        SkillQ,
        Died,
        Kick
    };
    State state = State.Walking;

    State nextState = State.Walking;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        status = GetComponent<CharacterStatus>();
        charAnimation = GetComponent<CharaAnimation>();

        inputManager = FindObjectOfType<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!status.died)
        {
            switch (state)
            {
                case State.Walking:
                    Walking();
                    break;
                case State.Attacking:
                    Attacking();
                    break;
                case State.SkillQ:
                    SkillQing();
                    break;
                case State.Kick:
                    SkillWing();
                    break;
            }

            if (state != nextState)
            {
                state = nextState;
                switch (state)
                {
                    case State.Walking:
                        WalkStart();
                        break;
                    case State.Attacking:
                        AttackStart();
                        break;
                    case State.SkillQ:
                        SkillQStart();
                        break;
                    case State.Kick:
                        SkillWStart();
                        break;
                    case State.Died:
                        Died();
                        break;
                }
            }
        }
    }

    //SendMessage("SetDestination", hitInfo.point);

    void SkillWing()
    {
        
        if (charAnimation.IsSkillW())
        { 
            ChangeState(State.Walking);
            inputManager.setW(false);
            status.SkillW = false;
            sword.SendMessage("SendW", false);
        }


    }

    void SkillWStart()
    {
        StateStartCommon();
        status.SkillW = true;
        sword.SendMessage("SendW", true);
        //move.walkSpeed = 130.0f;
        //  transform.FindChild("Hips").GetComponent<Rigidbody>().AddForce(Wposition*1000.0f);

    }
    void Walking()
    {

        
        if (inputManager.isQ())
        {

            ChangeState(State.SkillQ);
     
        }
        if(inputManager.isW())
        {
            ChangeState(State.Kick);
        }

        if (inputManager.Clicked())
        { 
            ChangeState(State.Attacking);
        
        }
    }

    void SkillQStart()
    {
        Qgage = 0.0f;
        StateStartCommon();
        status.SkillQ = true;
    }


    void AttackStart()
    {
        GetComponent<AudioSource>().PlayDelayed(0.2f);
        sword.SendMessage("QEnd");
        StateStartCommon();
        status.attacking = true;

   
    }
    void SkillQing()
    {

        if (!inputManager.isQ())
        {
            status.SkillQ = false;

            sword.SendMessage("SendQgage", Qgage);
            ChangeState(State.Walking);
        }
        else
        {
            if (Qgage < 40)
                Qgage += 3.1f;
        }
    }

    void ChangeState(State nextState)
    {
        this.nextState = nextState;
    }
    void Attacking()
    {
        if (charAnimation.IsAttacked())
            ChangeState(State.Walking);

    }

    void Died()
    {
        status.died = true;
        SceneManager.LoadScene("Game");
    }
    void Damage(AttackArea.AttackInfo attackInfo)
    {
        status.HP -= attackInfo.attackPower;
        if (status.HP <= 0)
        {
            status.HP = 0;
            ChangeState(State.Died);
         
        }
        gameManager.GetComponent<GameManager>().playerAttacked(attackInfo.attackPower);
    }
    void WalkStart()
    {
        StateStartCommon();
    }

    void StateStartCommon()
    {
        status.attacking = false;
        status.died = false;
    }
}


