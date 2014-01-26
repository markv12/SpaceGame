using UnityEngine;
using System.Collections;

public class EnumTest : MonoBehaviour {

	void Start () {
		Test(JustAnEnum.Red);
		Test(JustAnEnum.Yellow);
		Test(JustAnEnum.Orange);
		Test(JustAnEnum.Blue);
		Test(JustAnEnum.RedOrBlue);
		Test(JustAnEnum.AnythingButYellow);
	}
	
	
    void Test(JustAnEnum color) {
//		Debug.Log("Testing " + color);
//  		if ((color & JustAnEnum.RedOrBlue) == color) Debug.Log("It's Red or Blue"); 
//		if ((color & JustAnEnum.RedOrBlue) == JustAnEnum.None) Debug.Log("It's not Red or Blue");
//		if ((color & JustAnEnum.Yellow) == JustAnEnum.None) Debug.Log("It's not Yellow");
//		if ((color & JustAnEnum.All) == color) Debug.Log("It's definitely something");
//		Debug.Log("-----------------");
	}
}

public enum JustAnEnum {
  None = 0,
  Red = 1<<0,
  Blue = 1<<1,
  Orange = 1<<2,
  Yellow = 1<<3,
  All = ~0,
  RedOrBlue = Red | Blue,
  AnythingButYellow = All ^ Yellow
}