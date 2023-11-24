using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private delegate void TutorialEvents();

    private readonly List<(TutorialEvents, TutorialNext)> tutorialEvents = new List<(TutorialEvents, TutorialNext)>();

    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private Transform[] buttons;

    private void Start()
    {/* 템플릿
        tutorialEvents.Add((
            delegate
            {
                //튜토리얼 발생시킬 이벤트
            },
        new TutorialNext(
            next: delegate
            {
                //다음 튜토리얼 이벤트로 넘길 이벤트
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
                return true;
            },
            wait: delegate
            {
                //대기 중에 발생시킬 이벤트
                //없다면 wait 비워도 됨
            })
        ));

        StartCoroutine(EventOccurrence());
    }

    private IEnumerator EventOccurrence()
    {
        foreach (var (tuto, next) in tutorialEvents)
        {
            tuto();
            yield return next;
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
            return next();
        }

        public void Reset() { }
    }
}