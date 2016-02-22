function fxDTSBrick::setAlive(%this, %bool) {
	if(%this.alive == %bool) {
		return;
	}

	if(%bool) {
		%this.alive = 1;
		%this.setColor($Life::Color);
	} else {
		%this.alive = 0;
		%this.setColor(7);
	}
}

function fxDTSBrick::setNeighbors(%this) {
	%this.neighbors = 0;
	%x = %this.x;
	%y = %this.y;

	%brick[0] = $Life::Brick[%x-1, %y-1];
	%brick[1] = $Life::Brick[%x-1, %y];
	%brick[2] = $Life::Brick[%x-1, %y+1];
	%brick[3] = $Life::Brick[%x, %y-1];
	%brick[4] = $Life::Brick[%x, %y+1];
	%brick[5] = $Life::Brick[%x+1, %y-1];
	%brick[6] = $Life::Brick[%x+1, %y];
	%brick[7] = $Life::Brick[%x+1, %y+1];

	for(%i=0;%i<8;%i++) {
		if(%brick[%i].alive) {
			%this.neighbors++;
		}
	}
}

function fxDTSBrick::testForLife(%this) {
	%x = %this.x;
	%y = %this.y;

	%alive = %this.neighbors;

	if(%this.alive) {
		if(%alive <= 1) {
			%this.setAlive(0);
			return;
		}
		if(%alive >= 4) {
			%this.setAlive(0);
			return;
		}
		if(%alive == 2 || %alive == 3) {
			%this.setAlive(1);
			return;
		}
	} else {
		if(%alive == 3) {
			%this.setAlive(1);
		}
	}
}

function createGrid(%width, %length) {
	for(%x=0;%x<%width;%x++) {
		for(%y=0;%y<%length;%y++) {
			%brick = new fxDTSBrick() {
				angleID = 0;
				client = -1;
				colorFxID = 0;
				colorID = 7;
				dataBlock = "brick1x1Data";
				isBasePlate = 1;
				isPlanted = 1;
				position = %x*0.5 SPC %y*0.5 SPC 0.3;
				printID = 0;
				scale = "1 1 1";
				shapeFxID = 0;
				stackBL_ID = 888888;
				alive = 0;
				x = %x;
				y = %y;
				neighbors = 0;
				age = 0;
			};
			%brick.plant();
			%brick.setTrusted(1);

			BrickGroup_888888.add(%brick);

			$Life::Brick[%x, %y] = %brick;
		}
	}

	$Life::Width = %width;
	$Life::Length = %length;
}

function setRandomLife() {
	%width = $Life::Width;
	%length = $Life::Length;

	for(%x=0;%x<%width;%x++) {
		for(%y=0;%y<%length;%y++) {
			if(!getRandom(0, 5)) {
				$Life::Brick[%x, %y].setAlive(1);
			} else {
				$Life::Brick[%x, %y].setAlive(0);
			}
		}
	}
}

function doLifeStep() {
	%width = $Life::Width;
	%length = $Life::Length;
	$Life::Color = getRandom(18, 26);

	for(%x=0;%x<%width;%x++) {
		for(%y=0;%y<%length;%y++) {
			$Life::Brick[%x, %y].setNeighbors();
		}
	}

	for(%x=0;%x<%width;%x++) {
		for(%y=0;%y<%length;%y++) {
			$Life::Brick[%x, %y].testForLife();
		}
	}
}

function clearGrid() {
	%width = $Life::Width;
	%length = $Life::Length;

	for(%x=0;%x<%width;%x++) {
		for(%y=0;%y<%length;%y++) {
			$Life::Brick[%x, %y].setAlive(0);
		}
	}
}

package LifePackage {
	function fxDTSBrick::onActivate(%this, %a, %b, %c) {
		if(%this.alive) {
			%this.setAlive(0);
		} else {
			%this.setAlive(1);
		}
		return parent::onActivate(%this, %a, %b, %c);
	}
};
activatePackage(LifePackage);