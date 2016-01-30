using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyReaction : MonoBehaviour {

    private Vector3 upScale = new Vector3(0.2f, 0.2f, 0.2f);

    private Vector3 m_startPosition;
    private Vector3 m_endPosition;

    private Vector3 m_deathRotation = new Vector3(0, 0, 180.0f);
    private Vector3 m_jumpPosition;

    void Start()
    {
        DOTween.Init();
        m_startPosition = transform.position;

        m_endPosition = m_startPosition;
        m_endPosition.y = m_startPosition.y + 0.3f;

        m_jumpPosition = m_startPosition;
        m_jumpPosition.x = 5;

        Idle();
    }

    public void Idle()
    {
        DOTween.Kill(transform);
        transform.DOJump(m_startPosition, 0.15f, 1, 1).SetLoops(-1, LoopType.Yoyo);
        //transform.DOScaleY(0.12f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        //transform.DOScaleX(0.14f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void HitEnemy()
    {
        DOTween.Kill(transform);
        transform.DOShakePosition(1, 0.7f, 10, 90);
        transform.DOScale(0.12f, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(Idle);
    }

   
    public void YouWin()
    {
        DOTween.Kill(transform);
        transform.DORotate(m_deathRotation, 0.3f);
        transform.DOJump(m_jumpPosition, 1, 2, 1).OnComplete(Idle);
    }

    public void HitsYou()
    {
        DOTween.Kill(transform);
        transform.DOScale(upScale, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(Idle);
    }
}
