using UnityEngine;
using System.Collections;

class MouseOrbitImprovedX : MonoBehaviour{
	
	public Transform target = null;
	public float distance = 25.0f;
	public float damping = 6.0f;
	
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;
	
	public float yMinLimit = -10.0f;
	public float yMaxLimit = 75.0f;
	public float distanceMin = 3.0f;
	public float distanceMax = 6.0f;
	
	private float x = 0.0f;
	private float y = 0.0f;
	
	public float smoothTime = 0.3f;
	
	private float xSmooth = 0.0f;
	private float ySmooth = 0.0f;
	private float xVelocity = 0.0f;
	private float yVelocity = 0.0f;

	private float sSmooth = 0.0f;
	private float sVelocity = 0.0f;
	private float s = 0.0f;
	
	private Vector3 posSmooth;
	//private Vector3 posVelocity = Vector3.zero;
	//private float disVelocity = 0.0f;
	//private Vector3 autoZoomVelocity;
	
	void Start(){
		
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
	}
	
	void LateUpdate(){
		
		if(target){
			s = Input.GetAxis("Mouse ScrollWheel")* 2.0f;
			sSmooth = Mathf.SmoothDamp(sSmooth,s, ref sVelocity, smoothTime);
	
			x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			distance = Mathf.Clamp(distance - sSmooth, distanceMin, distanceMax);
	
			RaycastHit hitz = new RaycastHit();
			if(Physics.Linecast(target.position, transform.position, out hitz)){
				distance = hitz.distance;
			}
			
			if(y > yMaxLimit) y = yMaxLimit;
			if(y < yMinLimit) y = yMinLimit;

			xSmooth = Mathf.SmoothDamp(xSmooth, x, ref xVelocity, smoothTime);
			ySmooth = Mathf.SmoothDamp(ySmooth, y, ref yVelocity, smoothTime);
			
			Quaternion rotation = Quaternion.Euler(ySmooth, xSmooth, 0.0f);
			
			posSmooth = target.position;
			
			transform.rotation = rotation;
			transform.position = rotation * new Vector3(0.0f, 0.0f, -distance)+posSmooth;
			
			RaycastHit hit = new RaycastHit();
			if(Physics.Linecast(target.position, transform.position, out hit)){
				
				float tempDistance = Vector3.Distance(target.position,hit.point);

				Vector3 position = rotation * new Vector3(0.0f, 0.0f, -tempDistance)+target.position;
				
				transform.position = position;
				
			}
			
		}
		
	}
	
	public static float ClampAngle(float angle, float min, float max){
		
		if(angle < -360.0f) angle += 360.0f;
		if(angle > 360.0f) angle -= 360.0f;
		return Mathf.Clamp(angle,min,max);
		
	}
	
}