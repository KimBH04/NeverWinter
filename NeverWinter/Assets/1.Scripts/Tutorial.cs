using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private delegate void TutorialEvent();

    private readonly List<(TutorialEvent, TutorialNext)> tutorialEvents = new List<(TutorialEvent, TutorialNext)>();

    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private Transform point;

    [SerializeField] private Button[] buttons;

    [SerializeField] private TextMeshProUGUI tutorialTxt;
    [SerializeField] private GameObject tutorialPanel;

    [SerializeField] private GameObject levelUpInfomationPanel;

    private Vector3 cameraPos;
    private float counter = 0f;

    private void Start()
    {
        /* 템플릿
        tutorialEvents.Add((
            delegate
            {
                //튜토리얼 발생시킬 이벤트

                //Start() 함수와 비슷함
            },
        new TutorialNext(
            next: delegate
            {
                //다음 튜토리얼 이벤트로 넘길 조건 이벤트
                //ex) Input.GetMouseButtonDown(0)등의 리턴 값이 bool인 것
                //아니면 본인이 직접 만들어도 됨
                return true;
            },
            wait: delegate
            {
                //대기 중에 발생시킬 이벤트
                //생략 가능

                //Update() 함수와 비슷함
            },
            end: delegate
            {
                //끝났을 때 이벤트
                //생략 가능
            },
            time: 0f)   //조건이 충족 되고 대기할 시간
                        //생략 시 기본 0초
        ));
        */

        /* 설명해야 할 것
         * 1. 카메라 이동v
         * 1.5. 카메라 줌 인/아웃v
         * 2. 타워 소환v
         * 3. 웨이브 버튼v
         * 3.5. 증강체v
         * 4. 타워 병합
         * 5. 마법
         * 6. 속도
         * 
         * 순서 미정
         * -웨이브 깃발
         * -체력바
         */

        Camera main = Camera.main;
        Transform cam = main.transform;
        cameraPos = new Vector3(cam.position.x, 0f, cam.position.z);

        bool @event = false;

        #region 카메라 이동
        tutorialEvents.Add((
            delegate
            {
                tutorialPanel.SetActive(true);
                tutorialTxt.gameObject.SetActive(true);
                tutorialTxt.text = "마우스 휠을 눌러 카메라를 움직여보세요";
                counter = 0f;
            },
        new TutorialNext(
            next: delegate
            {
                
                return counter >= 15f;
            },
            wait: delegate
            {
                if (Mathf.Abs(Input.GetAxisRaw("Mouse ScrollWheel")) == 0f)
                    counter += (new Vector3(cam.position.x, 0f, cam.position.z) - cameraPos).magnitude;

                cameraPos = new Vector3(cam.position.x, 0f, cam.position.z);
            },
            end: delegate
            {
                tutorialPanel.SetActive(false);
                tutorialTxt.gameObject.SetActive(false);
            },
            time: 1f)
        ));
        #endregion

        #region 카메라 줌 인/아웃
        tutorialEvents.Add((
            delegate
            {
                tutorialPanel.SetActive(true);
                tutorialTxt.gameObject.SetActive(true);
                tutorialTxt.text = "마우스 휠를 스크롤해서\n카메라를 줌 인/아웃 해보세요";
                counter = 0f;
            },
        new TutorialNext(
            next: delegate
            {
                return counter >= 2f;
            },
            wait: delegate
            {
                counter += Mathf.Abs(Input.GetAxisRaw("Mouse ScrollWheel"));
            },
            end: delegate
            {
                tutorialPanel.SetActive(false);
                tutorialTxt.gameObject.SetActive(false);
            },
            time: 1f)
        ));
        #endregion

        #region 타워 소환
        tutorialEvents.Add((
            delegate
            {
                tutorialTxt.gameObject.SetActive(true);
                tutorialTxt.text = "타워를 소환해보세요";

                @event = false;
                buttons[0].onClick.AddListener(() => @event = true);

                backgroundPanel.transform.SetAsLastSibling();
                backgroundPanel.SetActive(true);
            
                buttons[0].transform.SetAsLastSibling();
                buttons[0].gameObject.SetActive(true);
            },
        new TutorialNext(
            next: delegate
            {
                return @event;
            },
            end: delegate
            {
                buttons[0].onClick.RemoveListener(() => @event = true);
                backgroundPanel.SetActive(false);
                tutorialTxt.gameObject.SetActive(false);
            })
        ));
        #endregion

        #region 타워 이동
        Transform summonedTower = transform;
        tutorialEvents.Add((
            delegate
            {
                tutorialPanel.SetActive(true);
                tutorialTxt.gameObject.SetActive(true);
                tutorialTxt.text = "타워를 드래그해 움직여 보세요";

                @event = false;
                point.gameObject.SetActive(true);
                GridTower.MovedEvent += () => @event = true;

                GridField[] fields = FindObjectsOfType<GridField>();

                foreach (GridField field in fields)
                {
                    if (field.havingTower != null)
                    {
                        summonedTower = field.havingTowerParent;
                        break;
                    }
                }
            },
        new TutorialNext(
            next: delegate
            {
                return @event;
            },
            wait: delegate
            {
                float w = Screen.width / 8f;
                float h = Screen.height / 8f;

                Vector3 summonedTowerPos = main.WorldToScreenPoint(summonedTower.position);

                point.position = new Vector3(
                    Mathf.Clamp(summonedTowerPos.x, w, Screen.width - w),
                    Mathf.Clamp(summonedTowerPos.y, h, Screen.width - h),
                    0f);

                if (summonedTowerPos.x < w)
                {
                    point.eulerAngles = Vector3.zero;
                }
                else if (summonedTowerPos.x > Screen.width - w)
                {
                    point.eulerAngles = new Vector3(0f, 0f, 180f);
                }
                else if (summonedTowerPos.y < h)
                {
                    point.eulerAngles = new Vector3(0f, 0f, 90f);
                }
                else if (summonedTowerPos.y > Screen.height - h)
                {
                    point.eulerAngles = new Vector3(0f, 0f, -90f);
                }
            },
            end: delegate
            {
                point.gameObject.SetActive(false);
                GridTower.MovedEvent -= () => @event = false;

                tutorialPanel.SetActive(false);
                tutorialTxt.gameObject.SetActive(false);
            })
        ));
        #endregion

        #region 웨이브
        tutorialEvents.Add((
            delegate
            {
                tutorialTxt.gameObject.SetActive(true);
                tutorialTxt.text = "준비를 마치면 웨이브 버튼을 눌러 전투를 시작하세요!\n 웨이브가 시작되면 타워를 소환/이동할 수 없습니다!";
                buttons[1].onClick.AddListener(() => backgroundPanel.SetActive(false));
                buttons[1].onClick.AddListener(() => tutorialTxt.gameObject.SetActive(false));

                @event = false;
                GameManager.WaveEndEvent += () => @event = true;

                backgroundPanel.transform.SetAsLastSibling();
                backgroundPanel.SetActive(true);

                buttons[1].transform.SetAsLastSibling();
                buttons[1].gameObject.SetActive(true);
            },
        new TutorialNext(
            next: delegate
            {
                return @event;
            },
            end: delegate
            {
                buttons[1].onClick.RemoveListener(() => backgroundPanel.SetActive(false));
                buttons[1].onClick.RemoveListener(() => tutorialTxt.gameObject.SetActive(false));
                GameManager.WaveEndEvent -= () => @event = true;

                backgroundPanel.SetActive(false);
            })
        ));
        #endregion

        #region 증강체
        tutorialEvents.Add((
            delegate
            {
                levelUpInfomationPanel.SetActive(true);
                tutorialTxt.gameObject.SetActive(true);
                tutorialTxt.text = "웨이브가 끝나면 랜덤한 3개의 증강 중\n하나를 고를 수 있습니다";
            },
        new TutorialNext(
            next: delegate
            {
                return Input.GetMouseButtonDown(0);
            },
            end: delegate
            {
                levelUpInfomationPanel.SetActive(false);
                tutorialTxt.gameObject.SetActive(false);
            })
        ));

        tutorialEvents.Add((
            delegate
            {
                @event = false;

                buttons[4].onClick.AddListener(() => @event = true);
                buttons[5].onClick.AddListener(() => @event = true);
                buttons[6].onClick.AddListener(() => @event = true);
            },
        new TutorialNext(
            next: delegate
            {
                return @event;
            },
            end: delegate
            {
                buttons[4].onClick.RemoveListener(() => @event = true);
                buttons[5].onClick.RemoveListener(() => @event = true);
                buttons[6].onClick.RemoveListener(() => @event = true);
            })
        ));

        tutorialEvents.Add((
            delegate
            {
                backgroundPanel.transform.SetAsLastSibling();
                backgroundPanel.SetActive(true);
                tutorialTxt.gameObject.SetActive(true);
                tutorialTxt.text = "적용 된 증강체는 체력 바 밑에 표시됩니다.";
            },
        new TutorialNext(
            next: delegate
            {
                return Input.GetMouseButtonDown(0);
            },
            end: delegate
            {
                backgroundPanel.SetActive(false);
                tutorialTxt.gameObject.SetActive(false);
            })
        ));
        #endregion

        StartCoroutine(EventOccurrence());
    }

    private IEnumerator EventOccurrence()
    {
        foreach (var (tuto, next) in tutorialEvents)
        {
            Debug.Log("start");
            tuto();

            yield return next;
            next.end?.Invoke();
            Debug.Log("done");
            yield return new WaitForSeconds(next.time);
        }
    }

    private class TutorialNext : IEnumerator
    {
        public delegate bool NextEvent();

        private readonly NextEvent next;
        private readonly TutorialEvent wait;
        public readonly TutorialEvent end;
        public readonly float time;

        public TutorialNext(NextEvent next, TutorialEvent wait = null, TutorialEvent end = null, float time = 0f)
        {
            this.next = next!;
            this.wait = wait;
            this.end = end;
            this.time = time;
        }

        public object Current
        {
            get
            {
                wait?.Invoke();
                return null;
            }
        }

        public bool MoveNext()
        {
            return !next();
        }

        public void Reset() { }
    }
}