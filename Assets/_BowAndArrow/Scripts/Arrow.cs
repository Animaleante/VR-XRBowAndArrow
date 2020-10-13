using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
	[HideInInspector]
	public XRHandInteractor handInteractor { get; private set; } = null;

	[SerializeField] float speed = 2000.0f;
	[SerializeField] Transform tip = null;

	bool inAir = false;
	Vector3 lastPosition = Vector3.zero;

	Rigidbody rb = null;

	protected override void Awake()
	{
		base.Awake();
		rb = GetComponent<Rigidbody>();

		onSelectEnter.AddListener(OnHandSelectEnter);
		onSelectExit.AddListener(OnHandSelectExit);
	}

	private void FixedUpdate()
	{
		if (inAir)
		{
			CheckForCollision();
			lastPosition = tip.position;
		}
	}

	private void CheckForCollision()
	{
		RaycastHit hit;
		if (Physics.Linecast(lastPosition, tip.position, out hit))
		{
			Stop();
			Target t = hit.collider.GetComponent<Target>();
			if (t)
			{
				t.Hit(this);
			}
		}
	}

	private void Stop()
	{
		inAir = false;
		SetPhysics(false);

	}

	public void Release(float pullValue)
	{
		inAir = true;
		SetPhysics(true);

		MaskAndFire(pullValue);
		StartCoroutine(RotateWithVelocity());

		lastPosition = tip.position;
	}

	private void SetPhysics(bool usePhysics)
	{
		rb.isKinematic = !usePhysics;
		rb.useGravity = usePhysics;
	}

	private void MaskAndFire(float power)
	{
		colliders[0].enabled = false;
		interactionLayerMask = 1 << LayerMask.NameToLayer("Ignore");

		Vector3 force = transform.forward * (power * speed);
		rb.AddForce(force);
	}

	private IEnumerator RotateWithVelocity()
	{
		yield return new WaitForFixedUpdate();

		while(inAir)
		{
			Quaternion newRotation = Quaternion.LookRotation(rb.velocity, transform.up);
			transform.rotation = newRotation;
			yield return null;
		}
	}

	public new void OnSelectEnter(XRBaseInteractor interactor)
	{
		base.OnSelectEnter(interactor);
	}

	public new void OnSelectExit(XRBaseInteractor interactor)
	{
		base.OnSelectExit(interactor);
	}

	private void OnHandSelectEnter(XRBaseInteractor interactor)
	{
		if(interactor is XRHandInteractor handInteractor)
		{
			this.handInteractor = handInteractor;
		}
	}

	private void OnHandSelectExit(XRBaseInteractor interactor)
	{
		if (interactor is XRHandInteractor handInteractor)
		{
			this.handInteractor = null;
		}
	}
}
