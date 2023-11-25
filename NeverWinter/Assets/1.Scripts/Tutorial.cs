using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private delegate void TutorialEvents();

    private readonly List<(TutorialEvents, TutorialNext)> tutorialEvents = new List<(TutorialEvents, TutorialNext)>();

    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private Transform[] buttons;

    private Vector3 cameraPos;
    private float counter = 0f;

    private void Start()
    {
        Transform cam = Camera.main.transform;
        cameraPos = new Vector3(cam.position.x, 0f, cam.position.z);
            
        /* 템플릿
        tutorialEvents.Add((
            delegate
            {
                //튜토리얼 발생시킬 이벤트
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
            })
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
            })
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
            })
        ));

        StartCoroutine(EventOccurrence());
    }

    private IEnumerator EventOccurrence()
    {
        foreach (var (tuto, next) in tutorialEvents)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("start");

            tuto();

            yield return next;
            Debug.Log("done");
        }
    }

    private class TutorialNext : IEnumerator
    {
        public delegate void WaitEvent();
        public delegate bool NextEvent();

        private readonly WaitEvent wait;
        private readonly NextEvent next;

        public TutorialNext(NextEvent next, WaitEvent wait = null)
        {
            this.wait = wait;
            this.next = next!;
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