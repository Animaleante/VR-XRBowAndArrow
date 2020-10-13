using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHandInteractor : XRDirectInteractor
{
	public new void OnSelectEnter(XRBaseInteractable interactable)
	{
		base.OnSelectEnter(interactable);
	}

	public new void OnSelectExit(XRBaseInteractable interactable)
	{
		base.OnSelectExit(interactable);
	}
}
