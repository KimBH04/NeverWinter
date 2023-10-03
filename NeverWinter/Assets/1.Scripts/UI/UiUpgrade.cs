using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NeverWiter
{
    public enum UpgradeItemType
    {
        CanonAttack,
        Coin,
        Spell,
        HP,
        Reload,
        DeBuff,

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
                manager.SelectUpgrade(upradeItemType);
        }

        public void SetUpgradeInfo(UpgradeItemType uType, int level)
        {
            upradeItemType = uType;

            switch (upradeItemType)
            {
                case UpgradeItemType.CanonAttack:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/Screenshot_20230914-212152_Gallery-removebg-preview");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "���� ��ȭ", level);
                        if (explain)
                            explain.text = "������ ���ݷ��� ���� �����մϴ�.";
                    }
                    break;
                case UpgradeItemType.Coin:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/SELL");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "���� ȹ�淮 ����", level);
                        if (explain)
                            explain.text = "���� ȹ�淮�� �����մϴ�.";
                    }
                    break;
                case UpgradeItemType.Spell:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/x1");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "���� �� ����", level);
                        if (explain)
                            explain.text = "���� ��Ÿ���� 5�� �����մϴ�.";
                    }
                    break;
                case UpgradeItemType.HP:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/x1.5");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "ü�� ����", level);
                        if (explain)
                            explain.text = "�� ü���� 3��ŭ �����մϴ�.";
                    }
                    break;
                case UpgradeItemType.Reload:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/x2");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "���ݼӵ� ����", level);
                        if (explain)
                            explain.text = "Ÿ�� ���ݼӵ��� �����մϴ�.";
                    }
                    break;
                case UpgradeItemType.DeBuff:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/LEAVE");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "�� �̵��ӵ� ����", level);
                        if (explain)
                            explain.text = "�� �̵��ӵ��� 10��ŭ �����մϴ�.";
                    }
                    break;
            }
        }
    }
}