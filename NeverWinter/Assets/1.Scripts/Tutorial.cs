using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private delegate void TutorialEvent();

    private readonly List<(TutorialEvent, TutorialNext)> tutorialEvents = new List<(TutorialEvent, TutorialNext)>();

    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private Transform pointArrow;
    [SerializeField] private GameObject[] buttons;

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

        //카메라 이동
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

        //카메라 줌 인/아웃
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

        //타워 소환
        Button button;  //제어할 버튼
        bool isClick = false;
        tutorialEvents.Add((
            delegate
            {
                button = buttons[0].GetComponent<Button>();
                button.onClick.AddListener(() => isClick = true);

                backgroundPanel.transform.SetAsLastSibling();
                backgroundPanel.SetActive(true);
            
                buttons[0].transform.SetAsLastSibling();
                buttons[0].SetActive(true);
            },
        new TutorialNext(
            next: delegate
            {
                return isClick;
            },
            end: delegate
            {
                backgroundPanel.SetActive(false);
            })
        ));

        //타워 이동
        Transform summonedTower = transform;
        bool isMoved = false;
        tutorialEvents.Add((
            delegate
            {
                pointArrow.gameObject.SetActive(true);
                GridTower.MovedEvent += () => isMoved = true;

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
                return isMoved;
            },
            wait: delegate
            {
                float w = Screen.width / 8f;
                float h = Screen.height / 8f;

                Vector3 summonedTowerPos = main.WorldToScreenPoint(summonedTower.position);

                Vector3 arrowPos = new Vector3(
                    Mathf.Clamp(summonedTowerPos.x, w, Screen.width - w),
                    Mathf.Clamp(summonedTowerPos.y, h, Screen.height - h),
                    0f);

                pointArrow.position = arrowPos;

                float x = summonedTowerPos.x - Screen.width / 2f;
                float y = summonedTowerPos.y - Screen.height / 2f;

                float degree = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

                pointArrow.eulerAngles = new Vector3(0f, 0f, degree);
            },
            end: delegate
            {
                pointArrow.gameObject.SetActive(false);
                GridTower.MovedEvent -= () => isMoved = false;
            },
            time: 1f)
        ));

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