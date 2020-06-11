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
	public BasicGun gun;
	public Button btn;
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions
	public void ApplyGun(Sprite sp, BasicGun gun, BP bp)
	{
		iconRenderer.sprite = sp;
		this.gun = gun;

		btn.onClick.AddListener(() =>
		{
			bp.GetGunAt(index);
		});
	}
	#endregion

	#region PrivateFunctions
	#endregion
}
