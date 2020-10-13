using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float hitDisplayTime = 5.0f;

    [SerializeField] Material baseMaterial;
    [SerializeField] Material hitMaterial;

    bool isHit = true;
    MeshRenderer mr;
    float displayTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
		{
            displayTime += Time.deltaTime;

            if (displayTime >= hitDisplayTime)
			{
                HideHit();
			}
		}
    }

    public void Hit(Arrow arrow)
	{
        isHit = true;
        displayTime = 0.0f;
        ShowHit();
    }

    void ShowHit()
	{
        mr.material = hitMaterial;
	}

    void HideHit()
    {
        mr.material = baseMaterial;
    }
}
