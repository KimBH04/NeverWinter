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
                            title.text = string.Format("{0} Lv. {1}", "대포 강화", level);
                        if (explain)
                            explain.text = "대포의 공격력이 소폭 증가합니다.";
                    }
                    break;
                case UpgradeItemType.Coin:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/SELL");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "코인 획득량 증가", level);
                        if (explain)
                            explain.text = "코인 획득량이 증가합니다.";
                    }
                    break;
                case UpgradeItemType.Spell:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/x1");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "스펠 쿨 감소", level);
                        if (explain)
                            explain.text = "스펠 쿨타임이 5초 감소합니다.";
                    }
                    break;
                case UpgradeItemType.HP:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/x1.5");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "체력 증가", level);
                        if (explain)
                            explain.text = "성 체력이 3만큼 증가합니다.";
                    }
                    break;
                case UpgradeItemType.Reload:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/x2");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "공격속도 증가", level);
                        if (explain)
                            explain.text = "타워 공격속도가 증가합니다.";
                    }
                    break;
                case UpgradeItemType.DeBuff:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/LEAVE");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "적 이동속도 감소", level);
                        if (explain)
                            explain.text = "적 이동속도가 10만큼 감소합니다.";
                    }
                    break;
            }
        }
    }
}