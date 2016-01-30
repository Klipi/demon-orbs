using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LevelSelectCamera : MonoBehaviour {

    public float touchSensitivity = 0.01f;
    public float snapMultiplier = 1.0f;

    private float m_minY = 0f;
    private float m_maxY = 0f;

    private float m_hardMinY = 0f;
    private float m_hardMaxY = 0f;
    private Vector3 m_lastPosition;

    private bool m_cancelTween;
    private bool m_initialized;

	// Use this for initialization
	void Start () {
        DOTween.Init();
        m_cancelTween = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(!m_initialized)
        {
            return;
        }

	    if(Input.GetMouseButtonDown(0))
        {
            m_lastPosition = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - m_lastPosition;
           
            if(delta.y > 0)
            {
                if(transform.position.y >= m_maxY)
                {
                    m_cancelTween = true;
                }
            }
            if(delta.y < 0)
            {
                if(transform.position.y <= m_minY)
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
            if (transform.position.y < m_minY)
            {
                transform.DOMoveY(m_minY, Mathf.Abs(transform.position.y - m_minY) * snapMultiplier);
            }
            else if(transform.position.y > m_maxY)
            {
                transform.DOMoveY(m_maxY, transform.position.y - m_maxY * snapMultiplier);
            } 
        }
	}

    public void setMinMaxY(float min, float max)
    {
        m_minY = min;
        m_hardMinY = m_minY - 1;
        Debug.Log("Min y: " + m_minY + ", hardMinY: " + m_hardMinY);

        m_maxY = max;
        m_hardMaxY = m_maxY + 1;
        Debug.Log("Max y: " + m_maxY + ", hardMaxY: " + m_hardMaxY);

        m_initialized = true;
    }
}
