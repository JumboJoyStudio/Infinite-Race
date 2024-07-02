using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {
	
	// Public variables visible in the inspector
	public float movespeed;
	public float rotateSpeed;
	public bool lamp;

	// Not visible in the inspector
	WorldGenerator generator;
    
	Car car;
	Transform carTransform;

	void Start(){
		// Find the car and the world generator
		car = GameObject.FindObjectOfType<Car>();
		generator = GameObject.FindObjectOfType<WorldGenerator>();
        
		if(car != null)
			carTransform = car.gameObject.transform;
	}

	void Update(){
		// Move towards the car
		transform.Translate(Vector3.forward * movespeed * Time.deltaTime);
        
		// If there's a car, rotate when the player car rotates
		if(car != null)
			CheckRotate();
	}
    
	void CheckRotate(){
		// The directional light rotates over a different axis than the world objects
		Vector3 direction = (lamp) ? Vector3.right : Vector3.forward;
		// Get the car rotation
		float carRotation = carTransform.localEulerAngles.y;
        
		// Get the left rotation (eulerAngles always returns positive rotations)
		if(carRotation > car.rotationAngle * 2f)
			carRotation = (360 - carRotation) * -1f;
        
		// Rotate this object based on the direction value, speed value, car rotation, and world dimensions
		transform.Rotate(direction * -rotateSpeed * (carRotation / (float)car.rotationAngle) * (36f / generator.dimensions.x) * Time.deltaTime);
	}

	// Method to update the movespeed from WorldGenerator
	public void UpdateSpeed(float newSpeed) {
		movespeed = newSpeed;
	}
}
