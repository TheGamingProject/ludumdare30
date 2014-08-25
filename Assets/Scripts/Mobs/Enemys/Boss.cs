using UnityEngine;
using System.Collections;

public class Boss : Enemy {
	public int healthGivenToPlayerOnDeath = 5;
	protected void die() {
		player.GetComponent<Player>().health += healthGivenToPlayerOnDeath;
		base.die();
	}
}
