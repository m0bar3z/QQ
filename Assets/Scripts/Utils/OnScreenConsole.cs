using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenConsole : MonoBehaviour
{
	[Header("OnScreenConsole vars")]

	#region StaticProperties
	public static bool hasInstnace;

	private static OnScreenConsole instance;

	public static void Print(string txt)
	{
		if (hasInstnace)
		{
			instance.PrintOnConsole(txt);
		}
	}
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	[Header("GO, components")]
	public Text consoleTXT;
	#endregion

	#region PublicVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	private void Start()
	{
		instance = this;
		hasInstnace = true;
	}

	private void PrintOnConsole(string txt)
	{
		consoleTXT.text = txt + "\n" + consoleTXT.text;
	}
	#endregion
}
