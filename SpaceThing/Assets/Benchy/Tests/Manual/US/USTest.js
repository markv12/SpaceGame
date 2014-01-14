#pragma strict
private var waitingForTest : boolean = true;

function Start () {
	TestCoroutine();
	yield WaitForSeconds(5.0);
	waitingForTest = false;
}

function TestCoroutine(){
	while(waitingForTest){
		yield StartCoroutine(HelloWorld());
	}
	return;
}

@NeverProfileMethod
function HelloWorld(){
	Benchy.Begin("");
	Benchy.Begin("HelloWord");
	Benchy.End("HelloWord");
	Benchy.End("");
	yield WaitForSeconds(1.0);
}

function Update () {
	DoSomeWork();
}

// None of the below actually does anything (the other code unlrelated to benchy). It's there to give the method some weight so we register some activity
function DoSomeWork() {
	 Benchy.Begin("");
	 Benchy.Begin("Rotation test");
	 transform.Rotate(0, 5 * Time.deltaTime, 0);
	 Benchy.End("Rotation test");
	 Benchy.End("");
	 transform.Translate(0, 0, 2);
}