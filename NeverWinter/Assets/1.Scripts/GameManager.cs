using System;
using NeverWiter;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public delegate void GameManagerEvent();
    public static GameManagerEvent WaveEndEvent;

    public static GameManager instance;
    
    public static int lives = 100;
    public int Max_lives;
    public Slider Castle_Hpbar;
    public TextMeshProUGUI Castle_HpText;

    private bool gameOver = false;
    private bool gameWon = false;
    public Image waveFlag;

    public int count = 0;
    public int enemyCount = int.MaxValue;
    public Sprite []image;
    //public WaveContainer[] WaveContainer;
    public Button Wavebutton;
    public Button Sumonbutton;
    
    public int wavecount = 0;

    public GameObject[] RandomTower = new GameObject[4];
    public Image[] icon = new Image[5];
    public int cnt = 0;
    public GameObject []poison= new GameObject[3];
    public GameObject[] Xbow = new GameObject[3];
    public GameObject[] Cannon = new GameObject[3];
    public GameObject[] Magic = new GameObject[3];
    public GameObject[] Pub = new GameObject[3];

    //타워 리셋
    public GameObject[] Xbow1 = new GameObject[3];
    public GameObject[] Cannon1 = new GameObject[3];
    public GameObject[] Magic1 = new GameObject[3];
    public GameObject[] Pub1 = new GameObject[3];

    public GridTowerRandomSpawn Summon;
    private GridTowerRandomSpawn Coin;
    
    [SerializeField]
    private GameObject  gameoverUI, AnyBtn;

    [SerializeField] 
    private GameObject gamewonUI;
    
    public GameObject levelUpPanel = null;
    public UiUpgrade[] upgradeItems = new UiUpgrade[3];
    public int[] upgradeItemLevel = new int[(int)UpgradeItemType.max + 1];
    public GameObject tower = null;
    //public GameObject[] TowerAD = new GameObject[6];
    //public EnemyCtrl[] EnemySpeed = new EnemyCtrl[4];

    void Awake()
    {
        Time.timeScale = 1f;
        
        //if (instance == null)
        //{
            instance = this;
        //}
        //else if (instance != this)
        //{
        //    destroy(gameobject);
        //}
        //dontdestroyonload(gameobject);
    }

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Transform childTransform_1 = Magic[i].transform.Find("Magic");
            Transform childTransform_10 = Magic1[i].transform.Find("Magic");

            Transform childTransform_2 = Cannon[i].transform.Find("Cannon");
            Transform childTransform_20 = Cannon1[i].transform.Find("Cannon");

            Transform childTransform_3 = Xbow[i].transform.Find("Xbow");
            Transform childTransform_30 = Xbow1[i].transform.Find("Xbow");

            Transform childTransform_4 = Pub[i].transform.Find("intersection");
            Transform childTransform_40 = Pub1[i].transform.Find("intersection");

            Tower2 childScript_1 = childTransform_1.GetComponent<Tower2>();
            Tower2 childScript_10 = childTransform_10.GetComponent<Tower2>();
            childScript_10.AD = childScript_1.AD;


            Tower2 childScript_2 = childTransform_2.GetComponent<Tower2>();
            Tower2 childScript_20 = childTransform_20.GetComponent<Tower2>();

            childScript_2.AD = childScript_20.AD;


            Tower2 childScript_3 = childTransform_3.GetComponent<Tower2>();
            Tower2 childScript_30 = childTransform_30.GetComponent<Tower2>();

            childScript_30.AD = childScript_3.AD;

            Pub childScript_4 = childTransform_4.GetComponent<Pub>();
            Pub childScript_40 = childTransform_40.GetComponent<Pub>();

            childScript_40.attackBoost = childScript_4.attackBoost;

        }

        Castle_HpText.text = 100+" / 100";
        lives = 100;
        Max_lives = lives;
        Coin = GameObject.Find("Grids").GetComponent<GridTowerRandomSpawn>();
        Application.targetFrameRate = 60;
        Castle_Hpbar.value = Max_lives;
    }


    private void Update()
    {
        if (wavecount < 6)
        {
            if (count >= enemyCount)
            {
                Wavebutton.transform.DOLocalMoveY(-400, 1f);
                Sumonbutton.transform.DOLocalMoveY(-400, 1f);
                count = 0;

                WaveEndEvent?.Invoke();

                wavecount++;
                if (wavecount < 6)
                {
                    WAVEEvent();
                    GridTower.PlayClick = true;
                    waveFlag.sprite = image[wavecount];
                }
            }

            return;
        }

        GameSet();
        //Invoke(nameof(GameSet), 1f);
    }

    void GameSet()
    {
        AudioManager.instance.PlayBgm(false);
        GameVictory();
        // wavecount = 0;
        enabled = false;
    }

   
    public int Lives
    {
        get { return lives; }
        set
        {
            lives = value;

            
            Castle_Hpbar.value = lives;
            Castle_HpText.text = lives+ " / 100";
            //Debug.Log(Castle_Hpbar.value);

            if (lives <= 0)
            {

                lives = 0;
                Castle_Hpbar.gameObject.SetActive(false);
 
                GameOver();
            }
        }
    }

    public void GameVictory()
    {
        if (!gameWon)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Won);
            gameWon = true;

            gamewonUI.SetActive(true);
             AnyBtn.SetActive(false);
            
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            AudioManager.instance.PlayBgm(false);
            gameOver = true;
            Time.timeScale = 0;

            
             gameoverUI.SetActive(true);
             AnyBtn.SetActive(false);
            
        }
    }

    public void WAVEEvent()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Level);
        if (levelUpPanel)
            levelUpPanel.SetActive(true);


        List<UpgradeItemType> UpType = new List<UpgradeItemType>();

        while (UpType.Count < 3)
        {
            UpgradeItemType addType = (UpgradeItemType)Random.Range(0, (int)UpgradeItemType.max);
            if (UpType.Contains(addType) == false)
                UpType.Add(addType);
        }

        for (int i = 0; i < upgradeItems.Length; i++)
        {
            upgradeItems[i].thisButton.interactable = false;
            int index = (int)UpType[i];
            upgradeItems[i].SetUpgradeInfo(UpType[i], upgradeItemLevel[index] + 1);
        }

        StartCoroutine(ActiveButtons());
    }

    IEnumerator ActiveButtons()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        for (int i = 0; i < upgradeItems.Length; i++)
        {
            upgradeItems[i].thisButton.interactable = true;
        }
    }

    public void SelectUpgrade(UpgradeItemType uType)
    {
        //Time.timeScale = 1.0f;
        if (levelUpPanel)
            levelUpPanel.SetActive(false);

        upgradeItemLevel[(int)uType]++;

        int currentLevel = upgradeItemLevel[(int)uType];

        switch (uType)
        {
            case UpgradeItemType.Potion:
                for (int i = 0; i < SkillControl.instance.skiiTimes.Length; i++)
                {
                    SkillControl.instance.skiiTimes[i] *= 0.9f;
                }
                icon[cnt].gameObject.SetActive(true);
                icon[cnt].sprite = Resources.Load<Sprite>("Sprites/증강_스킬쿨타임감소(화약통)");
                cnt++;
                break;

            case UpgradeItemType.Axe:
                for (int i = 0; i < 3; i++)
                {

                    //Transform childTransform = poison[i].transform.Find("Poison1");                  
                    //if (childTransform != null)
                    //{
                    //    PoisonDamage childScript = childTransform.GetComponent<PoisonDamage>();
                    //    // 자식 객체에 있는 함수 호출
                    //    if (childScript != null)
                    //    {
                    //        childScript.damagePerSecond += 2f;
                    //        Debug.Log(childScript.damagePerSecond);
                    //    }
                    //}
                }
                icon[cnt].gameObject.SetActive(true);
                icon[cnt].sprite = Resources.Load<Sprite>("Sprites/증강_맹독");
                cnt++;

                break;

            case UpgradeItemType.Book:

                for (int i = 0; i < 3; i++)
                {
                    float[] PlusADp = new float[3];
                    PlusADp[i] = 3.2f;
                    //PoisonDamage poisonDamageScript = poison[i].GetComponent<PoisonDamage>();
                    Transform childTransform = Magic[i].transform.Find("Magic");
                    if (childTransform != null)
                    {
                        Tower2 childScript = childTransform.GetComponent<Tower2>();
                        if (childScript != null)
                        {
                            childScript.PlusAD1(PlusADp[i]);
                        }
                    }
                }

                icon[cnt].gameObject.SetActive(true);
                icon[cnt].sprite = Resources.Load<Sprite>("Sprites/증강_마법");
                cnt++;


                break;

            case UpgradeItemType.Xbow:

                for (int i = 0; i < 3; i++)
                {
                    float PlusADp = 0.7f;
                    //PoisonDamage poisonDamageScript = poison[i].GetComponent<PoisonDamage>();
                    Transform childTransform = Xbow[i].transform.Find("Xbow");
                    if (childTransform != null)
                    {
                        Tower2 childScript = childTransform.GetComponent<Tower2>();
                        if (childScript != null)
                        {
                            childScript.PlusAD1(PlusADp);
                        }
                    }
                }

                icon[cnt].gameObject.SetActive(true);
                icon[cnt].sprite = Resources.Load<Sprite>("Sprites/증강_석궁");
                cnt++;
                break;

            case UpgradeItemType.Pub:
                for (int i = 0; i < 3; i++)
                {
                    float[] PlusADp = new float[3];
                    PlusADp[i] = 0.5f;
                    Transform childTransform = Pub[i].transform.Find("intersection");
                    if (childTransform != null)
                    {
                        Pub childScript = childTransform.GetComponent<Pub>();
                        // 자식 객체에 있는 함수 호출
                        if (childScript != null)
                        {
                            childScript.plisad(PlusADp[i]);
                        }
                    }
                }
                icon[cnt].gameObject.SetActive(true);
                icon[cnt].sprite = Resources.Load<Sprite>("Sprites/증강_선술집");
                cnt++;
                break;

            case UpgradeItemType.scout:
                icon[cnt].gameObject.SetActive(true);
                icon[cnt].sprite = Resources.Load<Sprite>("Sprites/증강_바리게이트");
                cnt++;
                break;

            case UpgradeItemType.Knight:
                for (int i = 0; i < 3; i++)
                {
                    float[] PlusADp = new float[3];
                    PlusADp[i] = 1.5f;
                    //PoisonDamage poisonDamageScript = poison[i].GetComponent<PoisonDamage>();
                    Transform childTransform = Cannon[i].transform.Find("Cannon");
                    if (childTransform != null)
                    {
                        Tower2 childScript = childTransform.GetComponent<Tower2>();
                        if (childScript != null)
                        {
                            childScript.PlusAD1(PlusADp[i]);
                        }
                    }

                }
                icon[cnt].gameObject.SetActive(true);
                icon[cnt].sprite = Resources.Load<Sprite>("Sprites/증강_대포");
                cnt++;

                break;

            case UpgradeItemType.Gold:
                Coin.LevelCost -= 10;
                icon[cnt].gameObject.SetActive(true);
                icon[cnt].sprite = Resources.Load<Sprite>("Sprites/증강_골드");
                cnt++;

                break;

            case UpgradeItemType.Clover:


                break;

            case UpgradeItemType.Shield:
                Castle_HpText.text = (lives + 20).ToString() + " / " + (lives + 20).ToString();
                lives += 20;
                Max_lives += 20;
                Castle_Hpbar.value += 20;
                icon[cnt].gameObject.SetActive(true);
                icon[cnt].sprite = Resources.Load<Sprite>("Sprites/증강_체력");
                cnt++;


                break;

            case UpgradeItemType.Crown:
                //Vector3 spawnPosition = new Vector3(-8.04f, 0.1499987f, 7.98f);
                int result = Random.Range(0, 4);
                //GameObject newUnit = Instantiate(RandomTower[result], spawnPosition, Quaternion.identity);
                icon[cnt].gameObject.SetActive(true);
                icon[cnt].sprite = Resources.Load<Sprite>("Sprites/증강_타워소환");
                cnt++;
                Summon.SpawningTowerToRandomPosition(false);


                break;
        }
    }
}