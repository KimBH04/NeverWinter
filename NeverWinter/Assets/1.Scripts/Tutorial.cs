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
         * 1. 카메라 이동
         * 1.5. 카메라 줌 인/아웃
         * 2. 타워 소환
         * 3. 웨이브 버튼
         * 3.5. 증강체
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
            time: 1f)
        ));
        #endregion

        #region 카메라 줌 인/아웃
        tutorialEvents.Add((
            delegate
            {
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
            time: 1f)
        ));
        #endregion

        #region 타워 소환
        tutorialEvents.Add((
            delegate
            {
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
            })
        ));
        #endregion

        #region 타워 이동
        Transform summonedTower = transform;
        tutorialEvents.Add((
            delegate
            {
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

                Vector3 summonedTowerPos = main.WorldToScreenPoint(summonedTower.position);

                point.gameObject.SetActive(true);
                point.position = new Vector3(w, summonedTowerPos.y, 0f);

                if (summonedTowerPos.x < w)
                {
                    point.eulerAngles = Vector3.zero;
                }
                else if (summonedTowerPos.x > Screen.width - w)
                {
                    point.eulerAngles = new Vector3(0f, 0f, 180f);
                }
                else
                {
                    point.gameObject.SetActive(false);
                }
            },
            end: delegate
            {
                point.gameObject.SetActive(false);
                GridTower.MovedEvent -= () => @event = false;
            },
            time: 1f)
        ));
        #endregion

        #region 웨이브
        tutorialEvents.Add((
            delegate
            {
                buttons[1].onClick.AddListener(() => backgroundPanel.SetActive(false));

                @event = false;
                EnemySpawnPoint.WaveFinished += () => @event = true;

                backgroundPanel.transform.SetAsLastSibling();
                backgroundPanel.SetActive(true);

                buttons[1].transform.SetAsLastSibling();
                buttons[1].gameObject.SetActive(true);
            },
        new TutorialNext(
            next: delegate
            {
                return @event && GameManager.count <= 0;
            },
            end: delegate
            {
                buttons[1].onClick.RemoveListener(() => backgroundPanel.SetActive(false));
                EnemySpawnPoint.WaveFinished -= () => @event = true;

                backgroundPanel.SetActive(false);
            })
        ));
        #endregion

        #region 증강체
        tutorialEvents.Add((
            delegate
            {
                tutorialTxt.text = "증강체에 대한 설명...";
            },
        new TutorialNext(
            next: delegate
            {
                return Input.GetMouseButtonDown(0);
            },
            end: delegate
            {
                tutorialTxt.text = string.Empty;
            })
        ));

        tutorialEvents.Add((
            delegate
            {
                @event = false;

                buttons[2].onClick.AddListener(() => @event = true);
                buttons[3].onClick.AddListener(() => @event = true);
                buttons[4].onClick.AddListener(() => @event = true);

                tutorialTxt.text = "아무 능력을 선택해보세요";
            },
        new TutorialNext(
            next: delegate
            {
                return @event;
            },
            end: delegate
            {
                buttons[2].onClick.RemoveListener(() => @event = true);
                buttons[3].onClick.RemoveListener(() => @event = true);
                buttons[4].onClick.RemoveListener(() => @event = true);

                tutorialTxt.text = string.Empty;
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