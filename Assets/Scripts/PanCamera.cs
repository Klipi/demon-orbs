using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PanCamera : MonoBehaviour {

    public float touchSensitivity = 0.01f;
    public float minY = 0;
    public float maxY = 20;
    public float snapMultiplier = 1.0f;

    private float m_hardMinY;
    private float m_hardMaxY;
    private Vector3 m_lastPosition;

    private bool m_cancelTween;

	// Use this for initialization
	void Start () {
        DOTween.Init();

        m_hardMinY = minY - 1;
        m_hardMaxY = maxY + 1;

        m_cancelTween = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0))
        {
            m_lastPosition = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - m_lastPosition;
           
            Debug.Log("Delta: " + delta);
            if(delta.y > 0)
            {
                Debug.Log("Moving down");
                if(transform.position.y >= maxY)
                {
                    m_cancelTween = true;
                }
            }
            if(delta.y < 0)
            {
                Debug.Log("Moving up");
                if(transform.position.y <= minY)
                {
                    m_cancelTween = true;
                }
            }

            if(DOTween.IsTweening(transform) && m_cancelTween)
            {
                DOTween.Kill(transform);
                m_cancelTween = true;
            }

            transform.Translate(0, -delta.y * touchSensitivity, 0);
            m_lastPosition = Input.mousePosition;

            Vector3 clampedPosition = transform.position;
            clampedPosition.y = Mathf.Clamp(transform.position.y, m_hardMinY, m_hardMaxY);
            transform.position = clampedPosition;
        }
        else
        {
            if (transform.position.y < minY)
            {
                transform.DOMoveY(minY, Mathf.Abs(transform.position.y - minY) * snapMultiplier);
            }
            else if(transform.position.y > maxY)
            {
                transform.DOMoveY(maxY, transform.position.y - maxY * snapMultiplier);
            } 
        }
	}
}
