using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using Holoville.HOTween.Core;
using Holoville.HOTween.Path;
using Holoville.HOTween.Plugins;
using Holoville.HOTween.Plugins.Core;

public class CameraMovementOnPath : LegacyObjectOnPath
{

    public enum BlockOneDirection {
        AfterCurrentPath,
        BeforeCurrentPath,
        AfterMultiPath,
        BeforeMultiPath,
        NONE
    }


    [Range(0.1f, 2.5f)]
    [Header("Speed of the object on the path", order = 0)]
    public float speed;
    
    [Range(1, 3)]
    [Header("Speed multiplicator, 1 = speed x 2, 2 = speed x 3", order = 0)]
    public int accelerationCoef = 1;

	public BlockOneDirection IgnoreDirection;

	private bool forcingMovementCamera = true;

    // To calculate where the camera is. 
    private float _percentageTemp = 0f;

    private float inputOffset = 0.5f;

    public float percentageTemp
    {
        get { return _percentageTemp; }
        set { _percentageTemp = value; }
    }

    // To prevent the camera from moving. 
    private bool _canMove = true;

    // To know wich type of movement the camera will do (Controller or forced travelling)
	private bool _forceMovement = false;

	// Value to calculate angle on the path
    private Vector3 vectAfter;
    private Vector3 vectBefore;
    private float angleAfter;

    private bool _goingUp;

    private float _isEqualOnPath = 0;

	// The name of the path to go when on a multiPath
	private string _pathToGo; 
	private bool _isMultiPath = false;
	private float angleAftermultipath;

	private float _rangePercentage = 0f;
	private float _percentageNextPath;

	private float _angleAxisHorizontal;
	private float _angleAxisVertical;
	private Vector2 _axisDirection;

    private bool _isJoystickOnWhileStoping = false;
    private bool _isJoystickMoving = false;

	private float[] _directionVignetting = new float[4];

    public bool forceMovement
    {
        get { return _forceMovement; }
        set { _forceMovement = value; }
    }

    public bool canMove
    {
        get { return _canMove; }
        set { _canMove = value; }
    }

	public float percentageNextPath
	{
		get { return _percentageNextPath; }
		set { _percentageNextPath = value; }
	}

	public string pathToGo
	{
		get { return _pathToGo; }
		set { _pathToGo = value; }
	}

	public bool isMultiPath
	{
		get { return _isMultiPath; }
		set { _isMultiPath = value; }
	}

    public bool isJoystickOnWhileStoping
    {
        get { return _isJoystickOnWhileStoping; }
        set { _isJoystickOnWhileStoping = value; }
    }
    public float[] directionVignetting
    {
        get { return _directionVignetting; }
    }
    public bool goingUp
    {
        get { return _goingUp; }
        set { _goingUp = value; }
    }

	private float _accelerationSpeed;
	public float accelerationSpeed 
	{
		get {return _accelerationSpeed;}
	}

	[HideInInspector]
	public bool canMoveDuringCutscene = true;

	private void Start()
	{
		directionVignetting[2] = 666;
		directionVignetting[3] = 666;
		PlaceObjectOnPath(percentage / 100);
	}

	public void PlaceObjectOnPath(float percentageFloat)
	{
		gameObject.transform.position = PathManager.instance.GetPathByName(pathName).GetComponent<GivePathPositions>().GetPathPosition(percentageFloat);
		_percentageTemp = percentageFloat;
		_realPercentage = _percentageTemp;
	}

	protected void FixedUpdate()
    {
		//if(GameManager.instance.currentState != GameManager.STATE.PSYCHO) {
			if (canMove && canMoveDuringCutscene)
            	Movement();
		
	}

	/// <summary>
    /// Check if the camera is able to move. And check witch type of movement it can do (Controller or travelling)
    /// </summary>
    void Movement()
    {   
        GetAxisAngle();
        if (forcingMovementCamera)
        	ForceMovementOnPath();

        if (!_isJoystickOnWhileStoping)
            MovementOnPath();

        GoingUpOnpath();
    }

	private float angleRelativeDirection;
    private float dPadRelativeDirection;
    /// <summary>
    /// Force the camera to move on the path, until it find an other trigger to stop that.
    /// </summary>
    private void ForceMovementOnPath()
    {
		Vector3 forwardOnCamera = transform.forward;
		forwardOnCamera.y = 0;

        // Here I set my camera angle on the path in function of the DPAD movement
        /*
		if (!InputManager.instance.JoystickLeftOffset(inputOffset)) 
			angleRelativeDirection = 666f;
		else
			angleRelativeDirection = Vector3.Angle(_axisDirection, new Vector2(0f,1f));

        if (InputManager.instance.IsDPADHorizontalPressed() == 0 && InputManager.instance.IsDPADVerticalPressed() == 0)
            dPadRelativeDirection = 666f;
        else
            dPadRelativeDirection = Vector3.Angle(_dPadDirection, new Vector2(0f, -1f));
         */

		vectAfter = ReturnAngleFromObjectInPath(_realPercentage, 0.01f, pathName) - transform.position;
        vectAfter.y = 0;

		vectBefore = ReturnAngleFromObjectInPath(_realPercentage, -0.01f, pathName) - transform.position;
        vectBefore.y = 0;

		if(IgnoreDirection != BlockOneDirection.AfterCurrentPath)
			angleAfter = Vector3.Angle(vectAfter, forwardOnCamera);
		else 
			angleAfter = 666;

		float angleBefore = 666;
		if (IgnoreDirection != BlockOneDirection.BeforeCurrentPath)
       	 	angleBefore = Vector3.Angle(forwardOnCamera, vectBefore);

		Vector3 crossAfter = Vector3.Cross(forwardOnCamera, vectAfter);
		Vector3 crossBefore = Vector3.Cross(forwardOnCamera, vectBefore);

		// To get the angle into a negative value
		if(_angleAxisHorizontal < 0)
            angleRelativeDirection = -angleRelativeDirection;

        if (_dPadHorizontal > 0)
            dPadRelativeDirection = -dPadRelativeDirection;

        if (crossAfter.y < 0)
			angleAfter = -angleAfter;

        if (crossBefore.y < 0)
            angleBefore = -angleBefore;

		directionVignetting[0] = angleAfter;
		directionVignetting[1] = angleBefore;

		if(isMultiPath)
            MultiPathCall(forwardOnCamera, angleRelativeDirection, dPadRelativeDirection);

		MovingCamera(angleAfter, angleBefore, angleRelativeDirection, dPadRelativeDirection, false);

		CalculMovement();
    }

	/// <summary>
	/// When the camera is on a multi path trigger, it's check if they're are other way to move.
	/// </summary>
	private void MultiPathCall(Vector3 forwardOnCamera, float angleRelativeDirection, float dPadRelativeDirection)
	{
		Vector3 vectAfterMultiPath = ReturnAngleFromObjectInPath(_percentageNextPath, 0.01f, _pathToGo) - transform.position;
		vectAfterMultiPath.y = 0;
		
		Vector3 vectBeforeMultiPath = ReturnAngleFromObjectInPath(_percentageNextPath, -0.01f, _pathToGo) - transform.position;
		vectBeforeMultiPath.y = 0;

		if (IgnoreDirection != BlockOneDirection.AfterMultiPath)
			angleAftermultipath = Vector3.Angle(forwardOnCamera, vectAfterMultiPath);
		else 
			angleAftermultipath = 666;

        float angleBefore = 666;
		if (IgnoreDirection != BlockOneDirection.BeforeMultiPath)
            angleBefore = Vector3.Angle(forwardOnCamera, vectBeforeMultiPath);

		Vector3 crossAfter = Vector3.Cross(forwardOnCamera, vectAfterMultiPath);
		Vector3 crossBefore = Vector3.Cross(forwardOnCamera, vectBeforeMultiPath);

		if (crossAfter.y < 0)
			angleAftermultipath = -angleAftermultipath;
		
		if (crossBefore.y < 0)
            angleBefore = -angleBefore;
		
		directionVignetting[2] = angleAftermultipath;
        directionVignetting[3] = angleBefore;

        MovingCamera(angleAftermultipath, angleBefore, angleRelativeDirection, dPadRelativeDirection, true);
	}

	/// <summary>
	/// Check if the percentage is good or not, and then applies the movement.
	/// </summary>
	private void CalculMovement()
	{
        //if (GameManager.instance.currentState != GameManager.STATE.PAUSE) {
            if (_forceMovement && (_realPercentage > 0f && _realPercentage < 1)) {
                if (!IsMovingOnPath()) {
                    _percentageTemp = _percentageTemp + (Time.deltaTime * Acceleration()) / 5;
                }
            }
        
	}

    private float Acceleration()
    {
		_accelerationSpeed = speed + tempSpeed;
		return _accelerationSpeed;
    }

    private bool _isAccelerating;
    public bool IsAccelerating()
    {
        if (_isAccelerating)
            return true;

        return false;
    }
	
	/// <summary>
	/// Take a float in parameter, and return the objects in the path from this float.
	/// </summary>
	public Vector3 ReturnAngleFromObjectInPath(float percentage, float value, string path)
	{
		float tempInt = Mathf.Round(percentage * 100f) / 100f + value;
		Vector3 positionAngle = PathManager.instance.GetPathByName(path).GetComponent<GivePathPositions>().GetPathPosition(tempInt);
		return positionAngle;
	}

    private float _dPadHorizontal;
    private float _dpadVertical;
    private Vector2 _dPadDirection;
	/// <summary>
	/// Get the controller angle, from 180 - 180
	/// </summary>
	private void GetAxisAngle()
	{
        //_dPadHorizontal = 180 * InputManager.instance.IsDPADHorizontalPressed();
        //_dpadVertical = 180 * InputManager.instance.IsDPADVerticalPressed();
        /*
		if (InputManager.instance.JoystickLeftOffset(inputOffset)) {
            _isJoystickMoving = true;
            _angleAxisHorizontal = 180 * InputManager.instance.JoystickLeftHorizontal();
            _angleAxisVertical = 180 * InputManager.instance.JoystickLeftVertical();
        }
         else 
         */
            _angleAxisHorizontal = 0;
            _angleAxisVertical = 0;

            _isJoystickMoving = false;
            _isJoystickOnWhileStoping = false;

        _axisDirection = new Vector2(_angleAxisHorizontal, _angleAxisVertical);
        _dPadDirection = new Vector2(_dPadHorizontal, _dpadVertical);
        _axisDirection.Normalize();
        _dPadDirection.Normalize();
	}

	/// <summary>
	/// Moves the camera with an offset on the controller to be more easy to use
	/// </summary>
	private void MovingCamera(float angleAfter, float angleBefore,  float angleRelativeDirection, float dPadRelativeDirection, bool multiPath)
	{
        //AFTER
		// Top
        if (!_forceMovement) 
		{
			if(angleRelativeDirection < 45 && angleRelativeDirection > -45)
			{
                _isAccelerating = false;
				// After
				if (angleAfter < 45 && angleAfter > -45)
					SlideUpOnPath(multiPath, 0);

				// Before
				if (angleBefore < 45 && angleBefore > -45)
					SlideDownOnPath(multiPath, 0);
			}

			if(angleRelativeDirection > 45 && angleRelativeDirection < 135)
			{
				//Right
                _isAccelerating = false;
                if (angleAfter > 45 && angleAfter < 135)
                {
                    SlideUpOnPath(multiPath, 0);
                }

				//Left
                if (angleBefore > 45 && angleBefore < 135)
                {
                    SlideDownOnPath(multiPath, 0);
                }
			}

			if((angleRelativeDirection > 135 && angleRelativeDirection <= 180) || (angleRelativeDirection < -135 && angleRelativeDirection >= -180))
			{
                _isAccelerating = false;
				//Bottom
				if ((angleAfter > 135 && angleAfter <= 180) || (angleAfter < -135 && angleAfter >= -180))
                    SlideUpOnPath(multiPath, 0);

				    //Bottom
		 		if ((angleBefore > 135 && angleBefore <= 180) || (angleBefore < -135 && angleBefore >= -180))
                    SlideDownOnPath(multiPath, 0);

			}

			if(angleRelativeDirection > -135 && angleRelativeDirection < -45)
			{
                _isAccelerating = false;
				// Left
                if (angleAfter > -135 && angleAfter < -45)
                {
                    SlideUpOnPath(multiPath, 0);
                }

				// Right
                if (angleBefore > -135 && angleBefore < -45)
                {
                    SlideDownOnPath(multiPath, 0);
                }
			}

            //------------------------------------------
            // D PAD
            //------------------------------------------    

            if (dPadRelativeDirection < 45 && dPadRelativeDirection > -45)
            {
                _isAccelerating = true;
                // After
                if (angleAfter < 45 && angleAfter > -45)
                    SlideUpOnPath(multiPath, accelerationCoef);

                // Before
                if (angleBefore < 45 && angleBefore > -45)
                    SlideDownOnPath(multiPath, accelerationCoef);
            }

            if (dPadRelativeDirection > 45 && dPadRelativeDirection < 135)
            {
                _isAccelerating = true;
                //Right
                if (angleAfter > 45 && angleAfter < 135)
                {
                    SlideUpOnPath(multiPath, accelerationCoef);
                }

                //Left
                if (angleBefore > 45 && angleBefore < 135)
                {
                    SlideDownOnPath(multiPath, accelerationCoef);
                }
            }

            if ((dPadRelativeDirection > 135 && dPadRelativeDirection <= 180) || (dPadRelativeDirection < -135 && dPadRelativeDirection >= -180))
            {
                _isAccelerating = true;
                //Bottom
                if ((angleAfter > 135 && angleAfter <= 180) || (angleAfter < -135 && angleAfter >= -180))
                    SlideUpOnPath(multiPath, accelerationCoef);

                //Bottom
                if ((angleBefore > 135 && angleBefore <= 180) || (angleBefore < -135 && angleBefore >= -180))
                    SlideDownOnPath(multiPath, accelerationCoef);

            }

            if (dPadRelativeDirection > -135 && dPadRelativeDirection < -45)
            {
                _isAccelerating = true;
                // Left
                if (angleAfter > -135 && angleAfter < -45)
                {
                    SlideUpOnPath(multiPath, accelerationCoef);
                }

                // Right
                if (angleBefore > -135 && angleBefore < -45)
                {
                    SlideDownOnPath(multiPath, accelerationCoef);
                }
            }
		}
		else
		{
			for(int i = 0; i < directionVignetting.Length; i++)
			{
				directionVignetting[i] = 666f;
			}
		}
	}

    /// <summary>
    /// Check if the camera is moving or stopped
    /// </summary>
    public bool IsMovingOnPath()
    {
        if (_isEqualOnPath != _realPercentage)
        {
            _isEqualOnPath = _realPercentage;
            return true;
        }
        _isEqualOnPath = _realPercentage;
        return false;
    }

    private void GoingUpOnpath()
    {
        if (_realPercentage > _rangePercentage)
        {
            _rangePercentage = _realPercentage;
            _goingUp = true;
        }
        else
        {
            _rangePercentage = _realPercentage;
            goingUp = false;
        }
    }

	/// <summary>
	/// Move the camera on the path, in the up way
	/// </summary>
    void SlideUpOnPath(bool multiPath, float speedAcceleration)
	{
		if(multiPath)
			MultiPathSwitch();

        if (!IsMovingOnPath())
            forceMovement = true;

		isMultiPath = false;
        if (speedAcceleration == 0f)
        {
            speed = Mathf.Abs(speed);
            tempSpeed = 0f;
        }
        else
        {
            speed = Mathf.Abs(speed);
            tempSpeed = Mathf.Abs(speed * speedAcceleration);
        }

	}

    private float tempSpeed;
	/// <summary>
	/// Move the camera on the path, in the down way
	/// </summary>
	void SlideDownOnPath(bool multiPath, float speedAcceleration)
	{
		if(multiPath)
			MultiPathSwitch();

        if (!IsMovingOnPath())
            forceMovement = true;

		isMultiPath = false;

        if (speed > 0)
        {
            speed = -speed;
            if (speedAcceleration == 0f)
                tempSpeed = 0f;
            else
                tempSpeed = speed * speedAcceleration;
        }
	}

    /// <summary>
    /// Make the switch on multiPath
    /// </summary>
    private void MultiPathSwitch()
    {
        _percentageTemp = percentageNextPath;
        percentage = 0;
        pathName = _pathToGo;
    }

    public bool isJoystickMoving()
    {
        if (_isJoystickMoving)
            return true;
        else
            return false;
    }
    /// <summary>
    /// Make the object move on the path.
    /// </summary>
    private void MovementOnPath()
    {
        // Adding the controller value / forcing value to the public percentage
		CheckLimitPath();
		_realPercentage = _percentageTemp;
		_realPercentage = Mathf.Clamp(_realPercentage, 0f, 1f);
        gameObject.transform.position = PathManager.instance.GetPathByName(pathName).GetComponent<GivePathPositions>().GetPathPosition(_realPercentage);
    }

    /// <summary>
    /// Check if the path is looping or not. If it's looping, when the camera is coming to the end of the path, make it go to the next path.
    /// </summary>
	private void CheckLimitPath()
    {
        if (!PathManager.instance.GetPathByName(pathName).GetComponent<GivePathPositions>().IsClosed())
            BlockOnPath();
        else
            LoopMovement();
    }

    /// <summary>
    /// When arriving at the end of a path, increase the index to put the object on the next path. When arriving at the beginning of a path, decrease the index to put the object on the before path.
    /// </summary>
	private void BlockOnPath()
    {
		if (_realPercentage == 0.0000001f)
			MovementPathEdge("up");
		
		if (_realPercentage == 1)
			MovementPathEdge("down");
    }

	/// <summary>
	/// Check if the player can move when he's on the edge of the path.
	/// </summary>
	private void MovementPathEdge(string indication)
	{
		float tempPercentageTemp = _percentageTemp;
		float forecastPercentageTemp = _percentageTemp + (Time.deltaTime * Acceleration()) / 5;

		if(indication == "up")
			MovementPathEdgeUp(tempPercentageTemp, forecastPercentageTemp);
		else 
			MovementPathEdgeDown(tempPercentageTemp, forecastPercentageTemp);
	}
	
	private void MovementPathEdgeUp(float tempPercentage, float forecastPercentageTemp)
	{	
		if (tempPercentage > 0 && tempPercentage < forecastPercentageTemp)
			_percentageTemp = _percentageTemp + (Time.deltaTime * Acceleration()) / 5;
	}
	
	private void MovementPathEdgeDown(float tempPercentage, float forecastPercentageTemp)
	{	
		if (tempPercentage > 0 && tempPercentage > forecastPercentageTemp)
			_percentageTemp = _percentageTemp + (Time.deltaTime * Acceleration()) / 5;
	}

	/// <summary>
	// When arriving at the end of the path, getting back at the beginning to loop throught it
	/// </summary>
	private void LoopMovement()
	{
       	if (_realPercentage >= 1f)
			_percentageTemp = 0.0000001f;

		if (_realPercentage <= 0f) {
			_percentageTemp = 0.999999f;
		}
	}
}  
