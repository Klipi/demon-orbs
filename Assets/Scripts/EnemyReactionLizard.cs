using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyReactionLizard : MonoBehaviour
{
    //TODO: KAIKKI :/

    private Transform m_head;
    private Transform m_body;

    private Vector3 m_startPosHead;
    private Vector3 m_startRotHead;

    void Start()
    {
        m_head = transform.GetChild(0);
        m_body = transform.GetChild(1);

        DOTween.Init();
        m_startPosHead = m_head.position;
        m_startRotHead = m_head.rotation.eulerAngles;

        Idle();
    }

    public void Idle()
    {
        
        Debug.Log("Idle!");
        DOTween.CompleteAll(); //needed?
        DOTween.KillAll(); //needed?
        Debug.Log("All prev tweens completed!");

        m_head.transform.position = m_startPosHead;
        m_head.transform.rotation = Quaternion.Euler(m_startRotHead);

        Sequence idleSeq = DOTween.Sequence();
        idleSeq.SetId("idle");
        idleSeq.Append(m_head.DOLocalRotate(new Vector3(0, 0, -6), 1.0f));
        idleSeq.Join(m_head.DOScaleY(0.90f, 1.0f));
        idleSeq.SetLoops(-1, LoopType.Yoyo);
        DOTween.Play(idleSeq);

        StartCoroutine(IdleBodySequence());



        /*
        transform.position = m_startPosition;
        transform.rotation = Quaternion.Euler(m_startRotation);
        transform.localScale = m_startScale;

        Sequence jumpSeq = DOTween.Sequence();
        jumpSeq.SetId("jump");

        jumpSeq.Append(transform.DOMove(m_endPositionUp, 0.5f));
        jumpSeq.Join(transform.DOScaleX(0.12f, 0.5f));

        jumpSeq.Append(transform.DOMove(m_endPositionDown, 0.5f));
        jumpSeq.Join(transform.DOScaleX(0.15f, 0.5f));
        jumpSeq.Join(transform.DOScaleY(0.14f, 0.5f));

        jumpSeq.Append(transform.DOMove(m_startPosition, 0.2f));
        jumpSeq.Join(transform.DOScaleY(0.15f, 0.2f));

        jumpSeq.SetLoops(-1, LoopType.Restart);
        DOTween.Play(jumpSeq);
        */
    }

    IEnumerator IdleBodySequence()
    {
        yield return new WaitForSeconds(1.0f);

        Sequence idleSeq2 = DOTween.Sequence();
        idleSeq2.SetId("idle2");
        idleSeq2.Append(m_body.DOScaleY(0.95f, 1.0f));
        idleSeq2.SetLoops(-1, LoopType.Yoyo);
        DOTween.Play(idleSeq2);
    }

    public void HitEnemy()
    {
        /*
        DOTween.Kill("jump");
        Sequence hitSeq = DOTween.Sequence();
        hitSeq.SetId("hit");
        hitSeq.Append(transform.DOShakePosition(1.0f, 0.7f, 10, 90));
        hitSeq.Join(transform.DOPunchScale(new Vector3(-0.1f, -0.1f, -0.1f), 1, 3, 0));
        hitSeq.AppendInterval(0.1f);
        hitSeq.OnComplete(Idle);
        DOTween.Play(hitSeq);
        */
    }

    public void HitsYou()
    {
        /*
        DOTween.Kill("jump");
        Sequence attackSeq = DOTween.Sequence();
        attackSeq.SetId("hitYou");
        attackSeq.Append(transform.DOScale(upScale, 0.5f));
        attackSeq.Append(transform.DOScale(m_startScale, 0.5f));
        attackSeq.AppendInterval(0.1f);
        attackSeq.OnComplete(Idle);
        DOTween.Play(attackSeq);
        */
    }
}
