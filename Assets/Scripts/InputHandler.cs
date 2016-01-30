using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using DG.Tweening;

public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool dragging = false;

	[SerializeField]
	private OrbSequenceController 	sequenceContoller;

	[SerializeField]
	private Transform				mousePathRenderer;

	[SerializeField]
	private Material				mousePathMaterial;

	[SerializeField]
	private float					mouseTrailFadeOutTime 	= 0.5f;

	[SerializeField]
	private	float					dragFadeInTime			= 0.2f;

	private AudioSource				dragAudioSource;

    void Start()
    {
		dragAudioSource = AudioPlayer.Instance.GetAudioSource(SoundType.DRAG);
	}

    private void Update()
    {
    	if (dragging)
    	{
			Vector3 newpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			newpos.z = 0f;
    		mousePathRenderer.position = newpos;
    	}
    }

    public void OnPointerDown(PointerEventData data)
    {
    	Debug.Log("Pointer down");
    	dragging = true;

    	sequenceContoller.StartSequence();

    	mousePathRenderer.GetComponent<ParticleSystem>().Play();

    	dragAudioSource.volume = 0f;
    	dragAudioSource.DOFade(1f, dragFadeInTime);
    	dragAudioSource.loop = true;
    	dragAudioSource.Play();

    }

	private IEnumerator FadeParticles()
	{
		float startTime = Time.time;
		ParticleSystem particleSystem = mousePathRenderer.GetComponent<ParticleSystem>();

		while (Time.time < startTime + mouseTrailFadeOutTime)
		{
			//This sets up an array container to hold all of the particles
			ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];

			//get the particles
			particleSystem.GetParticles(particles);

			byte a = Convert.ToByte(255f*Mathf.Lerp(1f, 0f, (Time.time - startTime)/mouseTrailFadeOutTime));

			Color32 newColor = new Color32(255, 255, 255, a);

			//then iterate through the array changing the color 1 by 1
			for(int p = 0; p < particles.Length; p++)
			{
				particles[p].color = newColor;
			}

			//set the particles back to the system
			particleSystem.SetParticles(particles, particles.Length);

			yield return new WaitForEndOfFrame();
		}

		particleSystem.SetParticles(new ParticleSystem.Particle[0], 0);

	}

    public void OnPointerUp(PointerEventData data)
    {
    	Debug.Log("Pointer up");
    	dragging = false;

    	sequenceContoller.EndSequence();

		mousePathRenderer.GetComponent<ParticleSystem>().Stop();
		StartCoroutine(FadeParticles());
		dragAudioSource.DOFade(0f, dragFadeInTime)
			.OnComplete(() => dragAudioSource.Stop());

    }
}
