using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OrbController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
	[SerializeField]
	private OrbSequenceController 	sequenceController;

	[SerializeField]
	private InputHandler			inputHandler;

	public OrbType Type;

	// Use this for initialization
	void Start () {
		Type = new OrbType();
		Type.Color = OrbColor.BLUE;
		Type.Symbol = OrbSymbol.SQUARE;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void HighlightOrb()
	{
		Debug.Log("Highlighted");
	}

	void TrySelect()
	{
		bool success = sequenceController.SelectOrb(this.Type);
		if (success)
		{
			this.HighlightOrb();
		}
	}

	public void OnPointerDown(PointerEventData data) 
	{
		// Pass the pointer event to InputHandler
		inputHandler.OnPointerDown(data);

		TrySelect();

	}

	public void OnPointerEnter(PointerEventData data)
	{
		TrySelect();
	}

	public void OnPointerUp(PointerEventData data)
	{
		// Pass the pointer event to InputHandler
		inputHandler.OnPointerUp(data);

	}
}
