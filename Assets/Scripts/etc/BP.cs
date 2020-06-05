using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP : MonoBehaviour
{
	[Header("BP vars")]

	#region StaticProperties
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	[Header("GO, Components")]
	public GameObject BPItemPrefab;
	public List<BasicGun> guns = new List<BasicGun>();
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions
	public void StoreGun(BasicGun gun)
	{
		foreach (BasicGun gn in guns)
		{
			if (gn.name == gun.name)
			{
				Destroy(gun.gameObject);
				return;
			}
		}

		guns.Add(gun);

		// make new gunItem

		// apply this gun to it

		// disable this gun's go

	}

	public void GetGunAt(int index, PlayerController pc, BasicGun curGun)
	{
		// disable cur gun
		// get the gun
		// store that last mo fo
		// enable the new mo fo
	}

	public void DropGunAt(int index)
	{
		// destroy the object and BPItem
		// update indexes
	}
	#endregion

	#region PrivateFunctions
	#endregion
}
