Sprites are from the kenney.nl Platformer Pack Redux
Download from http://kenney.nl/assets/platformer-pack-redux

INSCRUCTIONS
simply drag and drop the prefab onto your scene. Configure the AI from the script attached to the brain gameobject.

1. Drag the prefab onto the scene or instantiate it

2. When the player jumped on the fish's head it sends a message to the player. Add this to the players script...


void Hit(){
		
	playerRB.AddForce (Vector2.up * jumpDist);
	
}

This will apply a force upwards when the player jumps on the slime.