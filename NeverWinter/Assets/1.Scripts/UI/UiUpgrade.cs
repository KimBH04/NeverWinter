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
                // case UpgradeItemType.Potion:
                //     {
                //         if (icon)
                //             icon.sprite = Resources.Load<Sprite>("Sprites/증강 물약");
                //
                //         if (title)
                //             title.text = string.Format("{0} Lv. {1}", "물약", level);
                //         if (explain)
                //             explain.text = "주문 스킬 쿨타임 감소";
                //     }
                //     break;
                case UpgradeItemType.Axe:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/증강 도끼");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "증강 도끼", level);
                        if (explain)
                            explain.text = "주문 스킬 공격력 증가";
                    }
                    break;
                case UpgradeItemType.Book:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/증강 고서");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "증강 고서", level);
                        if (explain)
                            explain.text = "마법 타워의 공격력과 공격속도가 상승";
                    }
                    break;
                case UpgradeItemType.Xbow:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/증강 석궁");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "증강 석궁", level);
                        if (explain)
                            explain.text = "석궁 타워의 공격력과 공격속도 상승";
                    }
                    break;
                case UpgradeItemType.Pub:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/증강 선술집");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "증강 선술집", level);
                        if (explain)
                            explain.text = "선술집의 골드 수급량 증가";
                    }
                    break;
                case UpgradeItemType.scout:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/증강 정찰대");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "증강 정찰대", level);
                        if (explain)
                            explain.text = "적 몬스터들의 방어력 감소";
                    }
                    break;
                case UpgradeItemType.Knight:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/증강 기마병");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "증강 기마병", level);
                        if (explain)
                            explain.text = "적 이동속도가 10만큼 감소합니다";
                    }
                    break;
                case UpgradeItemType.Gold:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/증강 골드");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "증강 골드", level);
                        if (explain)
                            explain.text = "적 몬스터 처치시 획득 골드량 증가";
                    }
                    break;
                case UpgradeItemType.Clover:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/증강 클로버");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "증강 클로버", level);
                        if (explain)
                            explain.text = "무작위 타워 1기 획득 (1~3성)";
                    }
                    break;
                case UpgradeItemType.Shield:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/증강 방패");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "증강 방패", level);
                        if (explain)
                            explain.text = "공성 내구도 수치 증가 (피 회복)";
                    }

                    break;


                case UpgradeItemType.Crown:
                    {
                        if (icon)
                            icon.sprite = Resources.Load<Sprite>("Sprites/증강 왕관");

                        if (title)
                            title.text = string.Format("{0} Lv. {1}", "증강 왕관", level);
                        if (explain)
                            explain.text = "적 이동속도가 10만큼 감소합니다";
                    }

                    break;
                    
            }
        }
    }
}