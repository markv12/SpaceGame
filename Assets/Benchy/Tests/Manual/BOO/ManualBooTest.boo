import UnityEngine

class ManualBooTest (MonoBehaviour): 

	
	def Start ():
		pass
	
	def Update ():
		TranslateViaBoo()

	def TranslateViaBoo():
		Benchy.Begin("")
		transform.Translate(0, 0, 2)
		CalculateBoo()
		Benchy.Begin("Static boo test")
		BooClass.BooStaticCalculation()
		Benchy.End("Static boo test")
		Benchy.End("")
		
	def CalculateBoo():
		x = 1234 * 4321
		CalculateMoreBoo(x)
		
	def CalculateMoreBoo(x as int):
		y = x * 2
		y = y + 1
		
static class BooClass:
	[NeverProfileMethod]
	def BooStaticCalculation():
		z = 5090 * 2222
		z = z + 1
