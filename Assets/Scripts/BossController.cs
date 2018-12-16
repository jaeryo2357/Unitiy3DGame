using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    BossStatus status;
    BossAnimation bossAnimation;
    CharacterMove characterMove;
    Transform attackTarget;



    // 대기 시간은 2초로 설정한다.
    public float waitBaseTime = 2.0f;
    // 남은 대기 시간.
    float waitTime;
    // 이동 범위 5미터.
    public float walkRange = 5.0f;
    // 초기 위치를 저장해 둘 변수.
    public Vector3 basePosition;
    // 복수의 아이템을 저장할 수 있는 배열로 한다.
    public GameObject[] dropItemPrefab;

    // 스테이트 종류.
    enum State
    {
        Walking,	// 탐색.
        Chasing,	// 추적.
        Attacking,	// 공격.
        Died,// 사망.
        Attacking5,
    };

    State state = State.Walking;        // 현재 스테이트.
    State nextState = State.Walking;    // 다음 스테이트.


    // Use this for initialization
    void Start()
    {
        status = GetComponent<BossStatus>();
        bossAnimation = GetComponent<BossAnimation>();
    
        characterMove = GetComponent<CharacterMove>();
        // 초기 위치를 저장한다.
        basePosition = transform.position;
        // 대기 시간.
        waitTime = waitBaseTime;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.Walking:
                Walking();
                break;
            case State.Chasing:
                Chasing();
                break;
            case State.Attacking:
                Attacking();
                break;
            case State.Attacking5:
                Attack5Start();
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
                case State.Chasing:
                    ChaseStart();
                    break;
                case State.Attacking:
                    AttackStart();
                    break;
                case State.Died:
                    Died();
                    break;
                case State.Attacking5:
                    Attacking5();
                    break;
            }
        }
    }


    // 스테이트를 변경한다.
    void ChangeState(State nextState)
    {
        this.nextState = nextState;
    }

    void WalkStart()
    {
        StateStartCommon();
    }

    void Walking()
    {
       
        // 대기 시간이 아직 남았다면.
        if (waitTime > 0.0f)
        {
            // 대기 시간을 줄인다.
            waitTime -= Time.deltaTime;
            // 대기 시간이 없어지면.
            if (waitTime <= 0.0f)
            {
                // 범위 내의 어딘가.
                Vector2 randomValue = Random.insideUnitCircle * walkRange;
                // 이동할 곳을 설정한다.
                Vector3 destinationPosition = basePosition + new Vector3(randomValue.x, 0.0f, randomValue.y);
                // 목적지를 지정한다.
                SendMessage("SetDestination", destinationPosition);
            }
        }
        else
        {
            // 목적지에 도착한다.
            if (characterMove.Arrived())
            {
                // 대기 상태로 전환한다.
                waitTime = Random.Range(waitBaseTime, waitBaseTime * 1.0f);
            }
            // 타겟을 발견하면 추적한다.
            if (attackTarget)
            {
                
                ChangeState(State.Chasing);
            }
        }
    }
    void Attack5Start()
    {
        StateStartCommon();
        status.Attack5 = true;
        characterMove.walkSpeed = 20.0f;
         //SendMessage("SetDestination", attackTarget.position);
         // 적이 있는 방향으로 돌아본다.
         
        if (bossAnimation.IsAttacked5())
        {
            ChangeState(State.Walking);
            characterMove.walkSpeed = 2.6f;
            waitTime = Random.Range(waitBaseTime, waitBaseTime * 5.5f);
        }
        }
    // 추적 시작.
    void ChaseStart()
    {
        StateStartCommon();
    }

    void Attacking5()
    {

        if (bossAnimation.IsAttacked5())
        {
            ChangeState(State.Walking);
            characterMove.walkSpeed = 3.0f;
        }
   
    }
    // 추적 중.
    void Chasing()
    {
        // 이동할 곳을 플레이어에 설정한다.
        SendMessage("SetDestination", attackTarget.position);
        // 2미터 이내로 접근하면 공격한다.
        if (waitTime < 0.0f) { 
        if (Vector3.Distance(attackTarget.position, transform.position) > 22.0f&&!status.Attack5) //플레이어가 도망쳤을 때
        {
                if (Random.Range(0, 2) < 1)
                {
                    Vector3 targetDirection = (attackTarget.position - transform.position).normalized;
                    SendMessage("SetDirection", targetDirection);
                    ChangeState(State.Attacking5);
                }
          
        }
    }
        
        if (Vector3.Distance(attackTarget.position, transform.position) <= 2.0f)
        {
            ChangeState(State.Attacking);
        }
    }

    // 공격 스테이트가 시작되기 전에 호출된다.
    void AttackStart()
    {
        StateStartCommon();
        status.Attack1 = true;

        // 적이 있는 방향으로 돌아본다.
        Vector3 targetDirection = (attackTarget.position - transform.position).normalized;
        SendMessage("SetDirection", targetDirection);

        // 이동을 멈춘다.
        SendMessage("StopMove");
    }

    // 공격 중 처리.
    void Attacking()
    {
        
        if (bossAnimation.IsAttacked()) 
        {
            ChangeState(State.Walking);
        }
        // 대기 시간을 다시 설정한다.
        waitTime = Random.Range(waitBaseTime, waitBaseTime * 1.0f);
        // 타겟을 리셋한다.
        attackTarget = null;
    }

    void dropItem()
    {
        if (dropItemPrefab.Length == 0) { return; }
        GameObject dropItem = dropItemPrefab[Random.Range(0, dropItemPrefab.Length)];
        GameObject e = Instantiate(dropItem, transform.position, Quaternion.identity) as GameObject;
        e.GetComponent<Rigidbody>().AddForce(transform.up * 30.0f);
    }

    void Died()
    {
        status.died = true;
        dropItem();
        Destroy(gameObject);
    }

    void Damage(AttackArea.AttackInfo attackInfo)
    {
        status.HP -= attackInfo.attackPower;
        if (status.HP <= 0)
        {
            status.HP = 0;
            // 체력이 0이므로 사망 스테이트로 전환한다.
            ChangeState(State.Died);
        }
    }

    // 스테이트가 시작되기 전에 스테이터스를 초기화한다.
    void StateStartCommon()
    {
        status.Attack1 = false;
        status.died = false;
        status.Attack5 = false;
    }
    // 공격 대상을 설정한다.
    public void SetAttackTarget(Transform target)
    {
        attackTarget = target;
    }
}

