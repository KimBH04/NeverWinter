using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public delegate void TutorialEvents();

    private readonly List<(TutorialEvents, TutorialNext)> tutorialEvents = new List<(TutorialEvents, TutorialNext)>();

    public class TutorialNext : IEnumerator
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

    private void Start()
    {
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
}
