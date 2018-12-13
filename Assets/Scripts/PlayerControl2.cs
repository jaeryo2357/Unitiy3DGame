using UnityEngine;
using System.Collections;

public class PlayerControl2 : MonoBehaviour {


    const float RayCastMaxDistance = 100.0f;
    CharacterStatus status;
    CharaAnimation charAnimation;
    Transform attackTarget;
    InputManager inputManager;

    Vector3 Wposition;
    GameObject gameManager;

    public float attackRange = 1.5f;

    public float Qgage = 0.0f;
    enum State
    {
        Walking,
        Attacking,
        SkillQ,
        Died,
        SkillW
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
            case State.SkillW:
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
                case State.SkillW:
                    SkillWStart();
                    break;
                case State.Died:
                    Died();
                    break;
            }
        }
    }

    //SendMessage("SetDestination", hitInfo.point);

    void SkillWing()
    {
        ChangeState(State.Walking);
        //move.walkSpeed = 6.0f;
        status.SkillW = false;
        inputManager.setW(false);
    }

    void SkillWStart()
    {
        StateStartCommon();
        status.SkillW = true;
        //move.walkSpeed = 130.0f;
        //  transform.FindChild("Hips").GetComponent<Rigidbody>().AddForce(Wposition*1000.0f);
        Wposition = Vector3.zero;
    }
    void Walking()
    {

        if (inputManager.isW())
        {
            ChangeState(State.SkillW);
            attackTarget = null;
        }
        if (inputManager.isQ())
        {

            ChangeState(State.SkillQ);
            attackTarget = null;
        }

        else if (inputManager.Clicked())
        {

            Ray ray = Camera.main.ScreenPointToRay(inputManager.GetCursorPosition());
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, RayCastMaxDistance, (1 << LayerMask.NameToLayer("Ground")) |
                (1 << LayerMask.NameToLayer("EnemyHit"))))
            {

            
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("EnemyHit"))
                {
                    Vector3 hitpoint = hitInfo.point;
                    hitpoint.y = transform.position.y;
                    float distance = Vector3.Distance(hitpoint, transform.position);
                    if (distance < attackRange)
                    {

                        //공격
                        attackTarget = hitInfo.collider.transform;
                        ChangeState(State.Attacking);
                    }
                  
                }

            }
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
        StateStartCommon();
        status.attacking = true;

        Vector3 targetDirection = (attackTarget.position -
            transform.position).normalized;
        SendMessage("SetDirection", targetDirection);
        SendMessage("StopMove");
    }
    void SkillQing()
    {

        if (!inputManager.isQ())
        {
            status.SkillQ = false;

            GameObject.FindGameObjectWithTag("sword").SendMessage("SendQgage", Qgage);
            ChangeState(State.Walking);
        }
        else
        {
            if (Qgage < 100)
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

        if (inputManager.Clicked())
        {

            Ray ray = Camera.main.ScreenPointToRay(inputManager.GetCursorPosition());
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, RayCastMaxDistance, (1 << LayerMask.NameToLayer("Ground"))))
            {

                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    SendMessage("SetDestination", hitInfo.point);
                    ChangeState(State.Walking);
                    attackTarget = null;
                }

            }
        }
    }

    void Died()
    {
        status.died = true;
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


