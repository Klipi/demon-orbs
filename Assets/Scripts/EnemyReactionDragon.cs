﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyReactionDragon : MonoBehaviour {

    private Vector3 upScale = new Vector3(0.2f, 0.2f, 0.2f);

    private Vector3 m_startPosition;
    private Vector3 m_startRotation;
    private Vector3 m_startScale;

    private Vector3 m_endPositionUp;
    private Vector3 m_endPositionDown;

    //private Vector3 m_deathRotation = new Vector3(0, 0, 180.0f);
    private Vector3 m_jumpPosition;

    void Start()
    {
        DOTween.Init();
        m_startPosition = transform.position;
        m_startRotation = transform.rotation.eulerAngles;
        m_startScale = transform.localScale;

        m_endPositionUp = m_startPosition;
        m_endPositionUp.y = m_startPosition.y + 0.15f;

        m_endPositionDown = m_startPosition;
        m_endPositionDown.y = m_startPosition.y - 0.3f;

        m_jumpPosition = m_startPosition;
        m_jumpPosition.x = 5;

        Idle();
    }

    public void Idle()
    {
        Debug.Log("Idle!");
        DOTween.CompleteAll(); //needed?
        DOTween.KillAll(); //needed?
        Debug.Log("All prev tweens completed!");

        transform.position = m_startPosition;
        //transform.rotation = Quaternion.Euler(m_startRotation);
        transform.localScale = m_startScale;



        //jumpSeq.Append(transform.DOMove(m_endPositionUp, 1.0f));
        //jumpSeq.Append(transform.DOJump(m_endPositionUp, 0.5f, 1, 1.0f));
        //jumpSeq.Append(transform.DOScaleX(0.12f, 0.5f));

        //jumpSeq.Append(transform.DOScaleY(0.15f, 0.5f));


        //jumpSeq.Append(transform.DOMove(m_endPositionDown, 1.0f));
        //jumpSeq.Join(transform.DOScaleX(0.15f, 1.0f));
        //jumpSeq.Join(transform.DOScaleY(0.14f, 1.0f));

        //jumpSeq.Append(transform.DOMove(m_startPosition, 1.0f));

        //jumpSeq.Join(transform.DOScaleX(0.15f, 1.0f));

        Sequence jumpSeq = DOTween.Sequence();
        jumpSeq.SetId("jump");
        jumpSeq.Append(transform.DOScaleY(0.13f, 1.0f));
        jumpSeq.SetLoops(-1, LoopType.Yoyo);
        DOTween.Play(jumpSeq);
    }

    public void HitEnemy()
    {
        DOTween.Kill("jump");
        Sequence hitSeq = DOTween.Sequence();
        hitSeq.SetId("hit");
        hitSeq.Append(transform.DOShakePosition(1.0f, 0.7f, 10, 90));
        hitSeq.Join(transform.DOPunchScale(new Vector3(-0.1f, -0.1f,-0.1f), 1, 3, 0));
        hitSeq.AppendInterval(0.1f);
        hitSeq.OnComplete(Idle);
        DOTween.Play(hitSeq);
    }

    public void HitsYou()
    {
        DOTween.Kill("jump");
        Sequence attackSeq = DOTween.Sequence();
        attackSeq.SetId("hitYou");
        attackSeq.Append(transform.DOScale(upScale, 0.5f));
        attackSeq.Append(transform.DOScale(m_startScale, 0.5f));
        attackSeq.AppendInterval(0.1f);
        attackSeq.OnComplete(Idle);
        DOTween.Play(attackSeq);
    }
}
