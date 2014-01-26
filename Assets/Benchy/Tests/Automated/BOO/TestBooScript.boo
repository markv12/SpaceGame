import UnityEngine

class TestBooScript (MonoBehaviour): 

	def Start ():
		pass
	
	def Update():
		BooAutoTest()
		
	def BooAutoTest():
		transform.Rotate(0, (5 * Time.deltaTime), 0)
		BlankMethodsAreNeverProfiled()

	def BlankMethodsAreNeverProfiled():
		pass