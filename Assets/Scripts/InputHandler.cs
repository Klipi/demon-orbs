using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool dragging = false;

	[SerializeField]
	private OrbSequenceController sequenceContoller;

    void Start()
    {	
	}

    private void Update()
    {
    	if (dragging)
    	{
    		// Debug.Log(Input.mousePosition);
    	}
    }

    public void OnPointerDown(PointerEventData data)
    {
    	Debug.Log("Pointer down");
    	dragging = true;

    	sequenceContoller.StartSequence();
    }

    public void OnPointerUp(PointerEventData data)
    {
    	Debug.Log("Pointer up");
    	dragging = false;

    	sequenceContoller.EndSequence();
    }

}
