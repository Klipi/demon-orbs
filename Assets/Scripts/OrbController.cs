using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class OrbController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
	[SerializeField]
	private Color					unselectedColor;

	[SerializeField]
	private Color					selectedColor;

	[SerializeField]
	private OrbSequenceController 	sequenceController;

	[SerializeField]
	private InputHandler			inputHandler;

	[SerializeField]
	private float					colorTweenDuration = 0.3f;

	public OrbType Type;

	// Use this for initialization
	void Start () {
		Type = new OrbType();
		Type.Color = OrbColor.BLUE;
		Type.Symbol = OrbSymbol.SQUARE;

		sequenceController.OnSequenceEnd += SequenceController_OnSequenceEnd;;
	}

	void SequenceController_OnSequenceEnd (object sender, System.EventArgs e)
	{
		this.UnHighlightOrb();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UnHighlightOrb()
	{
		this.GetComponent<RawImage>().DOColor(unselectedColor, colorTweenDuration);

	}

	void HighlightOrb()
	{
		this.GetComponent<RawImage>().DOColor(selectedColor, colorTweenDuration);
		AudioPlayer.Instance.PlaySound(SoundType.SELECT);
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
