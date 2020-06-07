using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider2D : MonoBehaviour
{
	[Header("Slider2D vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	[Header("floats")]
	[Range(0f, 1f)]
	public float value;
	//[Header("ints")]
	//[Header("bools")]
	[Header("GO, components")]
	public SpriteRenderer sr;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	private float baseScale;
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions
	public void SetValue()
	{
		sr.transform.localScale = new Vector3(value * baseScale, sr.transform.localScale.y, sr.transform.localScale.z);
	}
	#endregion

	#region PrivateFunctions
	private void Start()
	{
		baseScale = sr.transform.localScale.x;
	}

	private void Update()
	{
		SetValue();
	}
	#endregion
}
