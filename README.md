Blockland adaptation of Conway's Game of Life.

How to use
==========
Create a grid for the simulation.  
`createGrid(32, 32);`  
Activate any of the grid bricks to toggle their state, or use `setRandomLife();`  
To advance a step, use `doLifeStep();`  

Automated advancing
===================
    function lifeAutomate() {
    	// the larger your grid, the higher the delay should be
    	// 32x32 can get away with a low delay of ~300
    	// 256x256 needs to be ~15000

    	%delay = 2000;
    	cancel($lifeSched);
    	$lifeSched = schedule(%delay, 0, lifeAutomate);

    	doLifeStep();
    }