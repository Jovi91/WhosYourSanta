var canvas = document.getElementById("canvas");
var ctx = canvas.getContext("2d");

var grd,
	keys_down = [],
	letters = [];
//list
//var symbols = [{ k: 81, s: "HTML", x: 5 }, { k: 87, s: "JS", x: 15 }, { k: 69, s: "CSS", x: 25 }, { k: 82, s: "SCSS", x: 35 }, { k: 84, s: "ASP.NET", x: 45 }, { k: 89, s: "C#", x: 55 }, { k: 85, s: "HAML", x: 65 }, { k: 73, s: "JADE", x: 75 }, { k: 79, s: "JQUERY", x: 85 }, { k: 80, s: "PHP", x: 95 }, { k: 65, s: "JAVA", x: 10 }, { k: 83, s: "JSON", x: 20 }, { k: 68, s: "XML", x: 30 }, { k: 70, s: "PYTHON", x: 40 }, { k: 71, s: "SQL", x: 50 }, { k: 72, s: "POSTGRESQL", x: 60 }, { k: 74, s: "MYSQL", x: 70 }, { k: 75, s: "LESS", x: 80 }, { k: 76, s: "CSS3", x: 90 }, { k: 90, s: "z", x: 20 }, { k: 88, s: "x", x: 30 }, { k: 67, s: "c", x: 40 }, { k: 86, s: "v", x: 50 }, { k: 66, s: "b", x: 60 }, { k: 78, s: "د", x: 70 }, { k: 77, s: "m", x: 80 }, { k: 48, s: "0", x: 90 }, { k: 49, s: "م", x: 0 }, { k: 50, s: "2", x: 10 }, { k: 51, s: "3", x: 20 }, { k: 52, s: "4", x: 30 }, { k: 53, s: "5", x: 40 }, { k: 54, s: "6", x: 50 }, { k: 55, s: "7", x: 60 }, { k: 56, s: "8", x: 70 }, { k: 57, s: "9", x: 80 }];
var symbols = [];
var drawnName = drawnSanta;
//var drawnWordDisplayed = false;
var IWon = false;
var countDrawnNameDisplayed = 0;
	//var myWords = ['kasia', 'MarilaTheBoss', 'Wojtex', 'Jovi', 'Antek', 'Makrela', 'Wiki']
nicksTable.forEach(AddWordsToObject);	


function AddWordsToObject(element, index) {
	var space = 80 / nicksTable.length;
	var xPosition;
	if (element == drawnName)
		xPosition = 40;
	else
		xPosition = Math.min(space, 13) + space * index;		

	var obj = { k: index, s: element, x: xPosition};
	symbols.push(obj);
}
function Letter(key) {

	var sym = findS(key);
	var rotateValue;
	var size;
	var transparency;
	//values for the name which was drawn (there can be onley one)
	if (sym == drawnName && countDrawnNameDisplayed < 1) {
		rotateValue = 0;
		size = 46;
		transparency = 1;
		IWon = true;
		countDrawnNameDisplayed++;
	}
	else {
		rotateValue = Math.floor((Math.random() * Math.PI) + 1);
		size = Math.floor((Math.random() * 40) + 12);
		transparency = Math.random();
		IWon = false;
    }


	this.x = findX(key);
	this.symbol = sym;
	this.color = "rgba(255, 255, 255, " + transparency + ")";
	this.size = size;
	this.path = getRandomPath(this.x);
	//this.rotate = Math.floor((Math.random() * Math.PI) + 1);
	this.rotate = rotateValue;
	this.percent = 0;
	this.Winner = IWon;
}

Letter.prototype.draw = function () {
	var percent = this.percent / 100;

	//for the winner the max heigth is fixed
	if (this.Winner == true) {
		this.path[1].y = -210;
		this.path[2].x = this.path[0].x +20;
    }
	
	var xy = getQuadraticBezierXYatPercent(this.path[0], this.path[1], this.path[2], percent, this);
	ctx.save();

	ctx.translate(xy.x, xy.y);

	ctx.rotate(this.rotate);
	//ctx.rotate(this.rotate- percent*2);
	ctx.font = this.size + "px Arial";
	ctx.fillStyle = this.color;
	ctx.fillText(this.symbol, -15, -15);
	ctx.restore();
};

//
//Letter.prototype.drawPath = function () {
//	ctx.lineWidth = 1;
//	ctx.beginPath();
//	ctx.moveTo(this.path[0].x, this.path[0].y);
//	ctx.quadraticCurveTo(this.path[1].x, this.path[1].y, this.path[2].x, this.path[2].y);
//	ctx.stroke();
//}

function findX(key) {
	for (var i = 0; i < symbols.length; i++) {
		if (symbols[i].k == key) {
			return (symbols[i].x * canvas.width / 100);
		}
	};
	return false;
}

function findS(key) {
	for (var i = 0; i < symbols.length; i++) {
		if (symbols[i].k == key) {
			return symbols[i].s;
		}
	};
	return false;
}

function getRandomPath(x) {
	var x_start = x;
	var x_end = x_start + Math.floor((Math.random() * 400) - 199);
	return [{
		x: x_start,
		y: canvas.height
	}, {
		x: (x_start + x_end) / 2,
		y: Math.floor((Math.random() * canvas.height) - canvas.height)
	}, {
		x: x_end,
		y: canvas.height
	}];
}

function drawBackground() {
	ctx.fillStyle = grd;
	ctx.fillRect(0, 0, canvas.width, canvas.height);
}


//
//function getLineXYatPercent(startPt, endPt, percent) {
//	var dx = endPt.x - startPt.x;
//	var dy = endPt.y - startPt.y;
//	var X = startPt.x + dx * percent;
//	var Y = startPt.y + dy * percent;
//	return ({ x: X, y: Y });
//}


function getQuadraticBezierXYatPercent(startPt, controlPt, endPt, percent, word) {
	
if (word.Winner == true && percent > 0.8)
		percent = 0.8;

	var x = Math.pow(1 - percent, 2) * startPt.x + 2 * (1 - percent) * percent * controlPt.x + Math.pow(percent, 2) * endPt.x;
	var y = Math.pow(1 - percent, 2) * startPt.y + 2 * (1 - percent) * percent * controlPt.y + Math.pow(percent, 2) * endPt.y;
	return ({ x: x, y: y });
}

//function getQuadraticBezierXYatPercent(startPt, controlPt, endPt, percent, word) {
//	var x = startPt.x;
//	if (word.Winner == true && percent > 0.8)
//		percent = 0.8;
//	var y = Math.pow(1 - percent, 2) * startPt.y + 2 * (1 - percent) * percent * controlPt.y + Math.pow(percent, 2) * endPt.y;

//	return ({ x: x, y: y });
//}

//
//function getCubicBezierXYatPercent(startPt, controlPt1, controlPt2, endPt, percent) {
//	var x = CubicN(percent, startPt.x, controlPt1.x, controlPt2.x, endPt.x);
//	var y = CubicN(percent, startPt.y, controlPt1.y, controlPt2.y, endPt.y);
//	return ({ x: x, y: y });
//}
////
//function CubicN(pct, a, b, c, d) {
//	var t2 = pct * pct;
//	var t3 = t2 * pct;
//	return a + (-a * 3 + pct * (3 * a - a * pct)) * pct
//		+ (3 * b + pct * (-6 * b + b * 3 * pct)) * pct
//		+ (c * 3 - c * 3 * pct) * t2
//		+ d * t3;
//}

function resize() {
	var box = canvas.getBoundingClientRect();
	canvas.width = box.width;
	canvas.height = box.height;
	grd = ctx.createRadialGradient(canvas.width / 2, canvas.height / 2, 0, canvas.width / 2, canvas.height / 2, canvas.height);
	grd.addColorStop(0, 'rgba(37, 30, 130, 0.0)');
	grd.addColorStop(1, 'rgba(37, 39, 86, 0.0)');
	
}

function draw() {
	ctx.clearRect(0, 0, canvas.width, canvas.height);
	drawBackground();

	for (var i = 0; i < letters.length; i++) {
		letters[i].percent += 0.8;
		letters[i].draw();
		//letters[i].drawPath();
		///
		///
		///
		///
		///tu ma sie zatrzymac i nie obracac.. no i nie powtarzac
		if (letters[i].percent > 100 && letters[i].symbol == drawnName) {
			//letters[i].rotate = 0;
			clearTimeout(myInterval);
			//letters.splice(i, 1);
		}
	};

	for (var i = 0; i < keys_down.length; i++) {
		if (keys_down[i]) {
			letters.push(new Letter(i));
		}
	};
	requestAnimationFrame(draw);
}
var start_keys=[];
for (i = 0; i < nicksTable.length; i++) {
	start_keys.push(i);
}
//nicksTable.forEach(number => start_keys.push(number));
//var start_keys = Array.from(Array(nicksTable.length).keys());
var count=15;

function startAnimation() {
	setTimeout(function () {
		var key = start_keys.pop();
		keys_down[key] = true;
		setTimeout(function () {
			keys_down[key] = false;
		}, 50);
		if (start_keys.length > 0) {
			startAnimation();
		}
	}, 200);
}
resize();
draw();
//startAnimation();
//function for repeat animation
var myInterval = setInterval(function () {

	if (start_keys.length <= 0 && count >= 0) {
		for (i = 0; i < nicksTable.length; i++) {
			start_keys.push(i);
		}
		//start_keys = Array.from(Array(nicksTable.length).keys());//words to show
		startAnimation();

		count--;
	}
}, 5000, startAnimation());
window.onresize = resize;
/*
document.getElementById("input").onkeyup = function(event){
	keys_down[event.keyCode] = false;
}

document.getElementById("input").onkeydown = function(event){
  if(event.keyCode == 91 && event.keyCode == 224){
    keys_down = [];
  }
	else if(event.keyCode >= 65 && event.keyCode <= 90 || event.keyCode >= 48 && event.keyCode <= 57){
		keys_down[event.keyCode] = true;
	}
}

document.getElementById("input").focus();
*/
window.requestAnimationFrame = (function () {
	return window.requestAnimationFrame ||
		window.webkitRequestAnimationFrame ||
		window.mozRequestAnimationFrame ||
		window.oRequestAnimationFrame ||
		window.msRequestAnimationFrame ||
		function (callback) {
			window.setTimeout(callback, 1000 / 60);
		};
})();
