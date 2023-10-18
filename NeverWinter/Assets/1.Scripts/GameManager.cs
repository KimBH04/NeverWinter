using System;
using NeverWiter;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private static int lives = 10;
    public int Max_lives;
    public Slider Castle_Hpbar;

    private bool gameOver = false;
    private bool gameWon = false;


    

    [SerializeField]
    private GameObject  gameoverUI, AnyBtn;

    [SerializeField] 
    private GameObject gamewonUI;
    
    public GameObject levelUpPanel = null;
    public UiUpgrade[] upgradeItems = new UiUpgrade[3];
    public int[] upgradeItemLevel = new int[(int)UpgradeItemType.max + 1];
    public GameObject tower = null;
    public GameObject[] TowerAD = new GameObject[6];
    public EnemyCtrl[] EnemySpeed = new EnemyCtrl[4];

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

        Max_lives = lives;
        
        Application.targetFrameRate = 60;
        Castle_Hpbar.value = Max_lives;

    }

   
    

    public int Lives
    {
        get { return lives; }
        set
        {
            lives = value;

            
            Castle_Hpbar.value = lives;
            Debug.Log(Castle_Hpbar.value);

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
            gameWon = true;
            Time.timeScale = 0;
            
            /*
             * gamewon.SetActive(true);
             *  AnyBtn.SetActive(false);
             */
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            Time.timeScale = 0;

            
             gameoverUI.SetActive(true);
             AnyBtn.SetActive(false);
            
        }
    }




    public void WAVEEvent()
    {
        Time.timeScale = 0.0f;
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
        Time.timeScale = 1.0f;
        if (levelUpPanel)
            levelUpPanel.SetActive(false);

        upgradeItemLevel[(int)uType]++;

        int currentLevel = upgradeItemLevel[(int)uType];

        switch (uType)
        {
            case UpgradeItemType.Axe:

                Tower2.ad += 5.0f;
                Debug.Log("포션");

                break;
            case UpgradeItemType.Potion:

                Debug.Log("도끼");

                break;

            case UpgradeItemType.Book:

                Debug.Log("책");

                break;
            case UpgradeItemType.Xbow:

                Tower2.shootdelay *= 0.9f;

                break;
            case UpgradeItemType.Pub:

                //Canon.shootDelay -= 0.2f;

                break;
            case UpgradeItemType.scout:

                Debug.Log("아아아 ");

                break;

            case UpgradeItemType.Knight:
/*
                for (int i = 0; i < EnemySpeed.Length; i++)
                {
                    EnemySpeed[i].agent.speed *= 0.5f;
                }
*/
                break;

            case UpgradeItemType.Gold:

                Debug.Log("아아아아");

                break;

            case UpgradeItemType.Clover:

                Debug.Log("Ŭ�ι�");
                Vector3 a = new Vector3(0.05f, 0.2f, -1.75f);
                Instantiate(tower, a, Quaternion.identity);

                break;

            case UpgradeItemType.Shield:

                Debug.Log("����");

                break;

            case UpgradeItemType.Crown:

                Debug.Log("����");

                break;
        }
    }


}