using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverWiter
{
    public class GameManager : MonoBehaviour
    { 
        private static int lives=100;
        private bool gameOver = false;

[SerializeField]
        private GameObject penel, gameoverUI, waveBtn;
     
        
        
        public Attack Bullet = null;
        public GameObject levelUpPanel = null;
        public UiUpgrade[] upgradeItems = new UiUpgrade[3];
        public int[] upgradeItemLevel = new int[(int)UpgradeItemType.max+1];
        public GameObject tower = null;
        

        void Awake()
        {

        }

        void Start()
        {
            Application.targetFrameRate = 60;
            Bullet.AD = 10f;
        }

        public int Lives
        {
            get { return lives; }
            set
            {
                lives = value;

                if (lives <= 0)
                {
                    lives = 0;
                    GameOver();
                }
            }
        }

        public void GameOver()
        {
            if (!gameOver)
            {
                gameOver = true;
                Time.timeScale = 0;
                /*
                 * penel.SetActive()
                 * gameoverUI.SetActive(true);
                 * waveBtn.SetActive(false);
                 */
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
                case UpgradeItemType.Potion:
                    
                        if (Bullet)
                        {
                            Bullet.AD += 5.0f; 
                        }

                    
                    break;
                case UpgradeItemType.Axe:
                    
                        Debug.Log("������ �����մϴ�");
                    
                    break;

                case UpgradeItemType.Book:
                    
                        Debug.Log("���� ��Ÿ�� ����");
                    
                    break;
                case UpgradeItemType.Xbow:
                    
                        Debug.Log("�ؼ��� Hp ����");
                    
                    break;
                case UpgradeItemType.Pub:

                    //Canon.shootDelay -= 0.2f;
                    
                    break;
                case UpgradeItemType.scout:
                    
                        Debug.Log("������ ");                      
                    
                    break;

                case UpgradeItemType.Knight:
                    
                        Debug.Log("�⸶��");
                    
                    break;

                case UpgradeItemType.Gold:
                    
                        Debug.Log("������ �����մϴ�");
                    
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
            } }

            
    }
}