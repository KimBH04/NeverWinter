using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NeverWiter
{
    public enum UpgradeItemType
    {
        Potion,
        Axe,
        Book,
        Xbow,
        Pub,
        scout,
        Knight,
        Gold,
        Clover,
        Shield,
        Crown,

        max
    }
    public class UiUpgrade : MonoBehaviour
    {
        public Button thisButton = null;
        public GameManager manager = null;
        public UpgradeItemType upradeItemType = UpgradeItemType.max;
        public Image icon = null;
        public Text title = null, explain = null;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void UpgradeItemAction()
        {
            if (manager)
            {
                manager.SelectUpgrade(upradeItemType);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
            }


        }

        public void SetUpgradeInfo(UpgradeItemType uType, int level)
        {
            
            upradeItemType = uType;

            switch (upradeItemType)
            {
                case UpgradeItemType.Potion:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/���� ����");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "����", level);
                        if (explain)
                            explain.text = "�ֹ� ��ų ��Ÿ�� ����";
                    }
                    break;
                case UpgradeItemType.Axe:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/���� ����");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "����", level);
                        if (explain)
                            explain.text = "�ֹ� ��ų ���ݷ� ����";
                    }
                    break;
                case UpgradeItemType.Book:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/���� ����");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "���� ����", level);
                        if (explain)
                            explain.text = "���� Ÿ���� ���ݷ°� ���ݼӵ��� ���.";
                    }
                    break;
                case UpgradeItemType.Xbow:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/���� ����");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "���� ����", level);
                        if (explain)
                            explain.text = "���� Ÿ���� ���ݷ°� ���ݼӵ� ���";
                    }
                    break;
                case UpgradeItemType.Pub:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/���� ������");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "���� ������", level);
                        if (explain)
                            explain.text = "�������� ��� ���޷� ����";
                    }
                    break;
                case UpgradeItemType.scout:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/���� ������");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "���� ������", level);
                        if (explain)
                            explain.text = "�� ���͵��� ���� ����";
                    }
                    break;
                case UpgradeItemType.Knight:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/���� �⸶��");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "�� �̵��ӵ� ����", level);
                        if (explain)
                            explain.text = "�� �̵��ӵ��� 10��ŭ �����մϴ�.";
                    }
                    break;
                case UpgradeItemType.Gold:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/���� ���");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "���� ���", level);
                        if (explain)
                            explain.text = "�� ���� óġ�� ȹ�� ��差 ����";
                    }
                    break;
                case UpgradeItemType.Clover:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/���� Ŭ�ι�");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "���� Ŭ�ι�", level);
                        if (explain)
                            explain.text = "������ Ÿ�� 1�� ȹ�� (1~3��)";
                    }
                    break;
                case UpgradeItemType.Shield:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/���� ����");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "���� ����", level);
                        if (explain)
                            explain.text = "���� ������ ��ġ ���� (�� ȸ��)";
                    }

                    break;


                case UpgradeItemType.Crown:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/���� �հ�");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "���� �հ�", level);
                        if (explain)
                            explain.text = "�� �̵��ӵ��� 10��ŭ �����մϴ�.";
                    }

                    break;
                    
            }
        }
    }
}