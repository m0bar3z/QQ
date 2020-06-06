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
	public GameObject bpItemPrefab;
	public Transform container;
	public List<BasicGun> guns = new List<BasicGun>();
	public List<BPItem> items = new List<BPItem>();
	#endregion

	#region PrivateVars
	private PlayerController pc;
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

		int index = guns.Count;
		guns.Add(gun);

		// make new gunItem
		BPItem item = Instantiate(bpItemPrefab, container).GetComponent<BPItem>();
		item.index = index;

		items.Add(item);

		// apply this gun to it
		item.ApplyGun(gun.spr.sprite, gun, this);

		// disable this gun's go
		gun.gameObject.SetActive(false);

	}

	public void GetGunAt(int index)
	{
		BasicGun gn = guns[index];
		gn.gameObject.SetActive(true);

		if (pc.facingRight && gn.transform.localScale.x > 0 || !pc.facingRight && gn.transform.localScale.x < 0) 
			gn.transform.localScale = new Vector3(-gn.transform.localScale.x, gn.transform.localScale.y, gn.transform.localScale.z);

		DropGunAt(index);

		pc.PickUp(gn);
	}

	public void DropGunAt(int index)
	{
		// destroy the object and BPItem
		BPItem itm = items[index];
		items.RemoveAt(index);
		guns.RemoveAt(index);
		Destroy(itm.gameObject);

		// update indexes
		int i = 0;
		foreach(BPItem item in items)
		{
			item.index = i++;
		}

	}
	#endregion

	#region PrivateFunctions
	private void Start()
	{
		pc = FindObjectOfType<PlayerController>();
	}
	#endregion
}
