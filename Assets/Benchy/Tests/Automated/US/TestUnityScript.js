#pragma strict
private var waitingForTest : boolean = true;
private var positions : Vector3[];

function Awake () {
    positions = new Vector3[100];
    for (var i = 0; i < 100; i++)
        positions[i] = Vector3.zero;
}

function Start () {
	TestCoroutine();
	yield WaitForSeconds(5.0);
	waitingForTest = false;
}

function TestCoroutine(){
	while(waitingForTest){
		yield StartCoroutine(TestingOneTwoThree());
		yield WaitForSeconds(1.0);
	}
}

function TestingOneTwoThree(){
	yield WaitForSeconds(1.0);
}

function Update () {
    var x = 2.3;
	x = x*222;
	BlankMethodsAreNeverProfiled();
}

function BlankMethodsAreNeverProfiled() {
}