﻿using UnityEngine;
using System.Collections;

public class TouchJoystick : TouchLogic 
{
	public enum JoystickType {Movement, Aiming};
	public JoystickType joystickType;
	public Transform player = null;
	public float playerSpeed = 2f, maxJoyDelta = 0.05f, rotateSpeed = 100.0f;
	private Vector3 oJoyPos, joyDelta;
	private Transform joyTrans = null;
	public CharacterController joytroller;
	private float pitch = 0.0f,
	yaw = 0.0f;
	private Vector3 oRotation;
	void Start () 
	{
		joyTrans = this.transform;
		oJoyPos = joyTrans.position;
		oRotation = player.eulerAngles;
		pitch = oRotation.x;
		yaw = oRotation.y;
	}
	
	void OnTouchBegan()
	{
		//Used so the joystick only pays attention to the touch that began on the joystick
		touch2Watch = TouchLogic.currTouch;
	}
	
	void OnTouchMovedAnywhere()
	{
		if(TouchLogic.currTouch == touch2Watch)
		{
			//move the joystick
			joyTrans.position = MoveJoyStick();
			ApplyDeltaJoy();
		}
	}
	
	void OnTouchStayedAnywhere()
	{
		if(TouchLogic.currTouch == touch2Watch)
		{
			ApplyDeltaJoy();
		}
	}
	
	void OnTouchEndedAnywhere()
	{
		//the || condition is a failsafe so joystick never gets stuck with no fingers on screen
		if(TouchLogic.currTouch == touch2Watch || Input.touches.Length <= 0)
		{
			//move the joystick back to its orig position
			joyTrans.position = oJoyPos;
			touch2Watch = 64;
		}
	}
	
	void ApplyDeltaJoy()
	{
		switch(joystickType)
		{
		case JoystickType.Movement:
			joytroller.Move ((player.forward * joyDelta.z + player.right * joyDelta.x) * playerSpeed * Time.deltaTime);
			break;
		case JoystickType.Aiming:
			pitch -= Input.GetTouch(touch2Watch).deltaPosition.y * rotateSpeed * Time.deltaTime;
			yaw += Input.GetTouch(touch2Watch).deltaPosition.x * rotateSpeed * Time.deltaTime;
		}
	}
	
	Vector3 MoveJoyStick()
	{
		//convert the touch position to a % of the screen to move our joystick
		float x = Input.GetTouch (touch2Watch).position.x / Screen.width,
		y = Input.GetTouch (touch2Watch).position.y / Screen.height;
		//combine the floats into a single Vector3 and limit the delta distance
		
		//If you want a circularly limited joystick, use this (uncomment it)
		Vector3 position = Vector3.ClampMagnitude(new Vector3 (x-oJoyPos.x, y-oJoyPos.y, 0), maxJoyDelta) + oJoyPos;
		
		//joyDelta used for moving the player
		joyDelta = new Vector3(position.x - oJoyPos.x, 0, position.y - oJoyPos.y).normalized;
		//position used for moving the joystick
		return position;
	}
	
	void LateUpdate()
	{
		if(!joytroller.isGrounded)
			joytroller.Move(Vector3.down * 2);
	}
}