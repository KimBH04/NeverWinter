using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverWiter
{
    public class GameManager : MonoBehaviour
    {
        public Attack Bullet = null;
        public GameObject levelUpPanel = null;
        public UiUpgrade[] upgradeItems = new UiUpgrade[3];
        public int[] upgradeItemLevel = new int[(int)UpgradeItemType.max+1];
        public Tower2 Canon = null; 

        void Awake()
        {

        }

        void Start()
        {
            Application.targetFrameRate = 60;
            Bullet.AD = 10f;
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
                case UpgradeItemType.CanonAttack:
                    {
                        if (Bullet)
                        {
                            Bullet.AD += 5.0f; 
                        }

                    }
                    break;
                case UpgradeItemType.Coin:
                    {
                        Debug.Log("������ �����մϴ�");
                    }
                    break;
                case UpgradeItemType.Spell:
                    {
                        Debug.Log("���� ��Ÿ�� ����");
                    }
                    break;
                case UpgradeItemType.HP:
                    {
                        Debug.Log("�ؼ��� Hp ����");
                    }
                    break;
                case UpgradeItemType.Reload:
                    {
                        Canon.shootDelay -= 0.2f;
                    }
                    break;

            } }

            
    }
}