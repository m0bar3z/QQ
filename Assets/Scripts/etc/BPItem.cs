using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BPItem : MonoBehaviour
{
	[Header("BPItem vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	[Header("ints")]
	public int index;
	//[Header("bools")]
	[Header("GO, Components")]
	public Image iconRenderer;	
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions
	public void ApplyGun(Sprite sp)
	{
		iconRenderer.sprite = sp;
	}
	#endregion

	#region PrivateFunctions
	#endregion
}
