﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

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

	private OrbType					_type;

	[SerializeField]
	private List<OrbController>		neighbors;

	public OrbType 					Type
	{
		get
		{
			return _type;
		}
		set
		{
			_type = value;
			ChangeSprite();
		}
	}

	public OrbPosition 				Position;

	private bool 					isSelected = false;

	// Use this for initialization
	void Start () {
		sequenceController.OnSequenceEnd += SequenceController_OnSequenceEnd;
	}

	private void ChangeSprite()
	{
		UnHighlightOrb();
	}

	void SequenceController_OnSequenceEnd (object sender, System.EventArgs e)
	{
		this.UnHighlightOrb();
		isSelected = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private Color GetColorForType(OrbType type)
	{
		switch (type.Color)
		{
			case OrbColorEnum.BLACK:
				return Color.black;
			case OrbColorEnum.BLUE:
				return Color.blue;
			case OrbColorEnum.GREEN:
				return Color.green;
			case OrbColorEnum.RED:
				return Color.red;
			case OrbColorEnum.WHITE:
				return Color.white;
			case OrbColorEnum.YELLOW:
				return Color.yellow;
			default:
				Debug.LogWarning(string.Format("Color not found {0}", type.Color));
				return Color.white;
		}
	}

	void UnHighlightOrb()
	{
//		this.GetComponent<RawImage>().DOColor(unselectedColor, colorTweenDuration);
		Texture texture = (Texture)Resources.Load(Type.GetResourcePath(false));
		this.GetComponent<RawImage>().texture = texture;
		ParticleSystem ps = this.GetComponent<ParticleSystem>();
		ps.Stop();
		ps.Clear();
	}

	void HighlightOrb()
	{
		// this.GetComponent<RawImage>().DOColor(selectedColor, colorTweenDuration);

		Texture texture = (Texture)Resources.Load(Type.GetResourcePath(true));
		this.GetComponent<RawImage>().texture = texture;
		AudioPlayer.Instance.PlaySound(SoundType.SELECT);
		ParticleSystem ps = this.GetComponent<ParticleSystem>();
		ps.startColor = GetColorForType(Type);
		ps.startLifetime = 3f;
		ParticleSystem.EmissionModule emission = ps.emission;
		emission.rate = new ParticleSystem.MinMaxCurve(10f, AnimationCurve.Linear(0f, 1f, 3f, 0.5f));
		ps.Play();
		ps.Emit(5);
	}

	public List<OrbController> GetNeighbors()
	{
		return neighbors;
	}

	void TrySelect()
	{
		if (isSelected)
			return;

		bool success = sequenceController.SelectOrb(this);
		if (success)
		{
			isSelected = true;
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
