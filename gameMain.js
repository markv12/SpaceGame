//var levelFileName = "levelX.tmx";
//var levelFileName = "uncompressed.tmx";
//var levelFileName = "OpenPlane.tmx";
var levelFileName = "SquareLevel.tmx";

var playerSpriteName = "pShip.png";
//var playerSpriteName = "spaceship.png";

var viewportNarrow = 2;
var viewportWide = 0.35;


var tileMapName = "tiles_mapBW.png";       
var player, thruster;
var speedLabel;
var mainViewport;
var background;

var Q = Quintus({ audioSupported: ['mp3', 'ogg'] })
	.include("Sprites, Scenes, Input, 2D, Touch, UI, Audio, Anim")
	.setup({
		width: 960,
		height: 640,
		development: true
	}).controls().touch();            

Q.input.keyboardControls({
	88: "flip",
	65: "left",
	68: "right"
});﻿

//player
Q.Sprite.extend("Player",{
	accelX:0,
	accelY:0,
	angleVelocity:0, 
	thrust:20,
	thrusting:false,
	cyclesSinceLastBurst:100,
	everyOther:1,           
	init: function(p) {
		this._super(p, { asset: playerSpriteName, x: 160, y: 50, jumpSpeed: -380, gravity:0.2});
		this.add('2d');    
		this.p.flip=false;

		Q.input.on("flip",this,function(){
			this.flip();
			//this.p.angle = -this.p.angle;
		});
	},

	flip: function(){
		if(this.p.flip){
				this.p.flip = false;
			}
			else{
				this.p.flip = 'x';
			}
			thruster.flip();
	},

	step: function(dt) { 
		//console.log(dt);
		//console.log(this.p.angle);

		//This catches when the player goes too fast and escapes the bounds of the level
		if(this.p.x > background.p.w-75){
			this.p.x = background.p.w -150;
			this.p.vx = 0;
		}
		else if(this.p.x <70){
			this.p.x = 70;
			this.p.vx = 0;
		}    
		if(this.p.y > background.p.h){
			this.p.y = background.p.h -250;
			this.p.vy = 0;
		}
		else if(this.p.y <70){
			this.p.y = 100;
			this.p.vy = 0;
		}
			
		if(Q.inputs['left']) {
			this.angleVelocity=-2;
		} 
		else if(Q.inputs['right']) {
			this.angleVelocity=2;              
		}
		this.p.angle+=this.angleVelocity;
		if(this.p.angle <-90){
			this.flip();
			this.p.angle=90;
		}
		else if(this.p.angle >90){
			this.flip();
			this.p.angle=-90;
		} 
  

		if(Q.inputs['up']) {
			this.accelY = -2; 

		} 
		else if(Q.inputs['down']) {
			this.accelY = -6;                 
		}
		if(Q.inputs['fire']) {
			if(this.p.flip){
				this.accelX = this.thrust*Math.cos(this.p.angle*0.0174532925);
				this.accelY = this.thrust*Math.sin(this.p.angle*0.0174532925);
			}
			else{
				this.accelX = -this.thrust*Math.cos(this.p.angle*0.0174532925);
				this.accelY = -this.thrust*Math.sin(this.p.angle*0.0174532925);
			}
	
			/*this.p.x +=this.everyOther*Math.cos(this.p.angle*0.0174532925);
			if(this.everyOther<=0){
				this.everyOther = Math.random()*10;
			}
			else{
				this.everyOther = -this.everyOther;
			}
			this.p.y +=this.everyOther*Math.sin(this.p.angle*0.0174532925);
			this.p.angle +=this.everyOther/5;*/
		
			if(!this.thrusting){
				Q.audio.play('rocket.mp3', { loop: true });
				thruster.play("thrust");
				this.thrusting=true;
				if(this.cyclesSinceLastBurst>100){
					this.accelX*=40;
					this.accelY*=40;
					this.cyclesSinceLastBurst=0;
				}
				else{
					this.cyclesSinceLastBurst/=2;
				}
			}
		} 
		else{
			if(this.thrusting){
				Q.audio.stop('rocket.mp3');
				thruster.play("off");
				this.thrusting=false;
			}
		}


		//console.log("AccelX: " +this.accelX +" AccelY: " + this.accelY);
		//console.log("vX: " +this.p.vx +" vy: " + this.p.vy);
		this.p.vx += this.accelX;
		this.p.vy += this.accelY;
		this.accelX=0;
		this.accelY=0;
		this.p.vx *= 0.996;
		this.p.vy *= 0.996;
		this.angleVelocity *= 0.9;
	
		this.cyclesSinceLastBurst++;
	
		//Q.stageScene("HUD",1);
		var currentFactor = mainViewport.scale;
		var scaleFactor;
		if(this.p.vx != 0){   	  
			 scaleFactor = Math.abs(1/(Math.sqrt(this.p.vx*this.p.vx+this.p.vy*this.p.vy)/800));
		}
		else{ 	
			 scaleFactor = viewportNarrow;
		}
		if(scaleFactor > viewportNarrow){
			scaleFactor=viewportNarrow;
		}
		else if(scaleFactor < viewportWide){
			scaleFactor=viewportWide;
		}	
		if(Math.abs(scaleFactor-currentFactor)<0.01){
			mainViewport.scale=scaleFactor;
		}
		else{
			if(scaleFactor>currentFactor){
				mainViewport.scale+=0.01; 
			}
			else if(scaleFactor<currentFactor){
				mainViewport.scale-=0.01; 
			}
		}

	}                    
  });

//thruster  
Q.animations('fire', {
	thrust: { frames: [6,5,4,3,2,1,0], rate: 1/15},
	off: { frames: [7]}
});  
Q.Sprite.extend("thruster", {
	init: function(p) {
		this._super(p,{
			sprite: "fire",
			sheet: "fire"
		});
		this.add("animation, 2d");
	}, 
	flip: function(){ 
		 this.p.x = -this.p.x;
		 this.p.angle = -this.p.angle;
	}              
}); 

Q.Sprite.extend("VerticalEnemy", {
	init: function(p) {
		this._super(p, {vy: -100, rangeY: 200, gravity: 0 });
		this.add("2d");

	this.p.initialY = this.p.y;

	this.on("bump.left,bump.right,bump.bottom",function(collision) {
		 if(collision.obj.isA("Player")) { 
			Q.stageScene("endGame",1, { label: "Game Over" }); 
				collision.obj.destroy();
			}
	});
	this.on("bump.top",function(collision) {
		if(collision.obj.isA("Player")) { 
			collision.obj.p.vy = -100;
			this.destroy();
		}		
	});

	},
	step: function(dt) {                
		if(this.p.y - this.p.initialY >= this.p.rangeY && this.p.vy > 0) {
			this.p.vy = -this.p.vy;
		} 
		else if(-this.p.y + this.p.initialY >= this.p.rangeY && this.p.vy < 0) {
		  this.p.vy = -this.p.vy;
		} 
	}
});   
	   

Q.scene("levelX",function(stage) {

	background = new Q.TileLayer({ dataAsset: levelFileName, layerIndex: 0, sheet: 'tiles', tileW: 70, tileH: 70, type: Q.SPRITE_NONE });
	stage.insert(background);
	stage.sort=true;

	stage.collisionLayer(new Q.TileLayer({ dataAsset: levelFileName, layerIndex:1,  sheet: 'tiles', tileW: 70, tileH: 70 }));
	thruster = new Q.thruster({x:420, y:-20, z:2, scale:2, angle:90, type:0, gravity:0});
	player = new Q.Player({x:560, y:400, z:1, scale:0.15});
	thruster.play("off"); 
	stage.insert(thruster,player); 
	stage.insert(player);

	//var enemy = stage.insert(new Q.VerticalEnemy({x: 800, y: 550, scale:0.2, rangeY: 170, asset: "spaceship.png" }));
	stage.add("viewport").follow(player,{x: true, y: true},{/*minX: 0, maxX: background.p.w, minY: 0, maxY: background.p.h*/});
	stage.viewport.offsetX = 480;
	stage.viewport.scale = 0.8;
	mainViewport = stage.viewport;
});
Q.scene("HUD",function(stage) {
	speedLabel = new Q.UI.Text({x:140, y: 110, label:"XSpeed: " + Math.floor(player.p.vx) + " YSpeed: " + Math.floor(player.p.vy)});
	stage.insert(speedLabel);
});
Q.scene('endGame',function(stage) {
	var box = stage.insert(new Q.UI.Container({
		x: Q.width/2, y: Q.height/2, fill: "rgba(0,0,0,0.5)"
	}));

	var button = box.insert(new Q.UI.Button({ x: 0, y: 0, fill: "#CCCCCC",
								   label: "Play Again" }))         
	var label = box.insert(new Q.UI.Text({x:10, y: -10 - button.p.h, 
								label: stage.options.label }));
	button.on("click",function() {
		Q.clearStages();
		Q.stageScene('levelX');
	});
	box.fit(20);
});


//load assets
Q.enableSound();
Q.load(tileMapName+ ", " + playerSpriteName + ", rocket.mp3, fire.png, fire.json, "+levelFileName, function() {
  Q.compileSheets("fire.png", "fire.json");
  Q.sheet("tiles",tileMapName, { tilew: 70, tileh: 70});          
  Q.stageScene("levelX");
  //Q.stageScene("HUD",1);

  //pre-load rocket audio
  Q.audio.play('rocket.mp3');
  Q.audio.stop('rocket.mp3');
});

