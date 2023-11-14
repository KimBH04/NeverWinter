using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


// 메모장

public class EnemyCtrl : MonoBehaviour
{
    public string EnemyName;
    private WayContainer container;
    private int idx;
    public Renderer render = null;
    public bool structure = false;
    
    public Transform[] movePoints;
    private GameManager manager;
    // 체력  #미완성#
   
    public float Enemy_HP;
    public float Max_Hp;
    public int atk;
    // 이동속도
    public float Enemy_move_Speed;
    public float Enemy_Speed_Save;
    
    //능력 #미완성#
    public string Enemy_Spell;

    // 현상금
    public int Reward;
    
    //적 사망여부 
    public bool isEnemyDie = false;
    public bool isEnd = false;
    private Coroutine damageCoroutine = null;

    private Transform target;
    private Gate gate1;
    Color originalColor;
    //public NavMeshAgent agent;

    [HideInInspector] public Animator animator;
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashDie = Animator.StringToHash("Die");
    
    void Start()
    {
        animator = GetComponent<Animator>();
        Enemy_Speed_Save = Enemy_move_Speed;
        container = GameObject.Find("WayContainer").GetComponent<WayContainer>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Enemy_HP = Max_Hp;
        originalColor = render.material.color;
    }

    private void Update()
    {
        MoveWay();
    }

    // 적이 데미지 받았을 때 쓰는 함수
    public void TakeDamage(float damage)
    {
        if (isEnemyDie)
            return;
        Enemy_HP -= damage;
        if (damageCoroutine != null)
            StopCoroutine(damageCoroutine);

        damageCoroutine = StartCoroutine(DamageEvent());
        
        if (Enemy_HP <= 0)
        {
            EnemyDie();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Gate")) //
        {
            structure = true;
            //Enemy_move_Speed = 0.0f;
            gate1 = other.GetComponent<Gate>();
            animator.SetBool(hashAttack, true);
            isEnd = true;
        }
    }

    public void Attack()
    {
        if (structure == false)
        GameManager.instance.Lives -= atk;

        if (structure == true)
        {
            gate1.hp -= atk;
            if (gate1.hp - atk <= 0)
            {             
                animator.SetBool(hashAttack, false);
                isEnd = false;
                structure = false;
            }
            
        }
        //Debug.Log("아야");
    }

    // 적이 죽었을 때 쓰는 함수  # 미완성 #
    // 애니메이션 추가 예정
    private void EnemyDie()
    {
        isEnd = true;
        isEnemyDie = true;
        if(EnemyName == "BabyGoblin") AudioManager.instance.PlaySfx(AudioManager.Sfx.BabyGoblin);
        else if(EnemyName == "BasicGoblin") AudioManager.instance.PlaySfx(AudioManager.Sfx.BasicGoblin);
        else if (EnemyName == "BugBear") AudioManager.instance.PlaySfx(AudioManager.Sfx.BugBear);
        else if (EnemyName == "MagicGoblin") AudioManager.instance.PlaySfx(AudioManager.Sfx.MagicGoblin);
        else if (EnemyName == "VikingGoblin") AudioManager.instance.PlaySfx(AudioManager.Sfx.VikingGoblin);
        else if (EnemyName == "ArcherGoblin") AudioManager.instance.PlaySfx(AudioManager.Sfx.ArcherGoblin);
        else AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);


        //agent.enabled = false;

        GetComponent<Collider>().enabled = false;

        Cost.Coin += Reward;
        //Destroy(gameObject, 1.0f);
        //Debug.Log("주금   ");
        animator.SetTrigger(hashDie);

        manager.count += 1;
        
    }

    //public void OnCollisionEnter(Collision other)
    //{
    //    return;
    //}


    IEnumerator Castle()
    {
        yield return new WaitForSeconds(1f); 
}


    public void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator DamageEvent()
    {
       
        if (render)
            render.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        if (render)
            render.material.color = originalColor;
    }

    void MoveWay()
    {
        if (!isEnd)
        {
            Transform tr = container.WayPoints[idx];
            transform.LookAt(tr, transform.up);

            transform.position = Vector3.MoveTowards(transform.position, tr.position, Enemy_move_Speed * Time.deltaTime);

            if ((transform.position - tr.position).sqrMagnitude < 0.05f)
            {
                idx++;
                if (idx >= container.WayPoints.Length)
                {
                    isEnd = true;
                }
            }
        }
        else
        {
            animator.SetBool(hashAttack, true);
        }
        
    }
}
