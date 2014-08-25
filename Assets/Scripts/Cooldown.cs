using UnityEngine;
using System.Collections;

public class Cooldown {
	Vector2 totalCooldownTimeRange;
	float totalCooldownTime;
	float currentCooldownTimeLeft = 0;

	public Cooldown(float totalTime) {
		totalCooldownTime = totalTime;
		resetCooldown();
	}
	public Cooldown(Vector2 totalTimeRange) {
		totalCooldownTimeRange = totalTimeRange;
		resetCooldown();
	}

	public bool isCooldownUp () {
		return currentCooldownTimeLeft <= 0.0f;
	}
	public void updateCooldown () {
		currentCooldownTimeLeft -= Time.deltaTime;
	}
	public void resetCooldown () {
		if (totalCooldownTime != 0) {
			currentCooldownTimeLeft = totalCooldownTime;
		} else {
			currentCooldownTimeLeft = Random.Range(totalCooldownTimeRange.x, totalCooldownTimeRange.y);
		}
	}
	public float getTimeLeft () {
		return currentCooldownTimeLeft;
	}
	public void setUp () {
		currentCooldownTimeLeft = 0;
	}
}

