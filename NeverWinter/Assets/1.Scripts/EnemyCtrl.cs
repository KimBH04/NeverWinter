using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;


// 메모장

public class EnemyCtrl : MonoBehaviour
{
    public string EnemyName;
    public WayContainer container;
    private WayContainer Babyway;
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
    
    //능력 #미완성#
    public string Enemy_Spell;

    // 현상금
    public int Reward;
    
    //적 사망여부 
    public bool isEnemyDie = false;
    public bool isEnd = false;
    private Coroutine damageCoroutine = null;
    //보스 스킬
    public float animationInterval = 15f;
    private float timer = 0f;
    public float distance = 7.0f;
    public bool skillcool = true;
    public GameObject unitPrefab;
    public GameObject Skilleffect;

    private Transform target;
    private Gate gate1;
    Color originalColor;
    //public NavMeshAgent agent;

    [HideInInspector] public Animator animator;
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashSkill = Animator.StringToHash("Skill");

    void Start()
    {
        Babyway = GameObject.Find("Left_WayContainer").GetComponent<WayContainer>();
        animator = GetComponent<Animator>();
        //container = GameObject.Find("WayContainer").GetComponent<WayContainer>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Enemy_HP = Max_Hp;
        originalColor = render.material.color;
    }

    private void Update()
    {
        MoveWay();
        if (gameObject.CompareTag("Boss"))
        {
            timer += Time.deltaTime;
            if (timer >= animationInterval)
            {
                animator.SetBool(hashSkill, true);
                skillcool = false;
                Skilleffect.SetActive(true);
                timer = 0f;
            }
        }
    }

    public void Skill()
    {
        
        Collider[] colliderList = Physics.OverlapSphere(transform.position, distance, LayerMask.GetMask("TOWER"));

        for (int i = 0; i < colliderList.Length; i++)
        {
            Debug.Log(i);
            GridTower searchTarget = colliderList[i].GetComponent<GridTower>();
            if (searchTarget) //&& searchTarget.isDie == false)
            {
                searchTarget.tower.Stun();
            }
        }

        skillcool = true;
        Vector3 spawnPosition = new Vector3(-24.10f, 0.16f, 0f);
        for (int i =0; i<3; i++)
        {
            GameObject newUnit = Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
            newUnit.GetComponent<EnemyCtrl>().container = Babyway;
            spawnPosition.x -= 1f;
            manager.count--;
        }

        Skilleffect.SetActive(false);
        animator.SetBool(hashSkill, false);
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
            //animator.SetBool(hashAttack, true);
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
            if (gate1.hp<= 0)
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
        
        // 적 사망 사운드
        switch (EnemyName)
        {
            case "BabyGoblin":
                AudioManager.instance.PlaySfx(AudioManager.Sfx.BabyGoblin);
                break;
            case "ArcherGoblin":
                AudioManager.instance.PlaySfx(AudioManager.Sfx.ArcherGoblin);
                break;
            case "BugBear":
                AudioManager.instance.PlaySfx(AudioManager.Sfx.BugBear);
                break;
            case "MagicGoblin":
                AudioManager.instance.PlaySfx(AudioManager.Sfx.MagicGoblin);
                break;
            case "VikingGoblin":
                AudioManager.instance.PlaySfx(AudioManager.Sfx.VikingGoblin);
                break;
            case "BabyDragonborn":
                AudioManager.instance.PlaySfx(AudioManager.Sfx.BabyDragonborn);
                break;
            case "BabyLizardman":
                AudioManager.instance.PlaySfx(AudioManager.Sfx.BabyLizardman);
                break;
            case "Bear":
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Bear);
                break;
            // case "Boss":
            //     AudioManager.instance.PlaySfx(AudioManager.Sfx.Boss);
            //     break;
            // 보스는 아직 사운드가 없음
            case "Lizardman":
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Lizardman);
                break;
            case "Werewolf":
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Werewolf);
                break;
            case "BasicGoblin":
                AudioManager.instance.PlaySfx(AudioManager.Sfx.BasicGoblin);
                break;
            default:
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
                break;
        }


        //agent.enabled = false;

        GetComponent<Collider>().enabled = false;

        Cost.Coin += Reward;
        //Destroy(gameObject, 1.0f);
        //Debug.Log("주금   ");
        animator.SetTrigger(hashDie);

        manager.count++;
        
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
        //if (!isEnd && idx < container.WayPoints.Count)
        //{
        //    if (gameObject.CompareTag("Boss")&& skillcool == false)
        //    {
        //        return;
        //    }

        //    Transform tr = container.WayPoints[idx];
        //    transform.LookAt(tr, transform.up);

        //    transform.position = Vector3.MoveTowards(transform.position, tr.position, Enemy_move_Speed * Time.deltaTime);

        //    if ((transform.position - tr.position).sqrMagnitude < 0.05f)
        //    {
        //        idx++;
        //        if (idx >= container.WayPoints.Count)
        //        {
        //            isEnd = true;
        //        }
        //    }
        //}
        if (container != null && !isEnd && idx < container.WayPoints.Count)
        {
            if (gameObject.CompareTag("Boss") && skillcool == false)
            {
                return;
            }

            Transform tr = container.WayPoints[idx];
            transform.LookAt(tr, transform.up);

            transform.position = Vector3.MoveTowards(transform.position, tr.position, Enemy_move_Speed * Time.deltaTime);

            if ((transform.position - tr.position).sqrMagnitude < 0.05f)
            {
                idx++;
                if (idx >= container.WayPoints.Count)
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
