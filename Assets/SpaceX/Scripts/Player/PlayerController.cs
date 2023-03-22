using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public float thurster = 10f;
    public float movementSmoothingSpeed = 1f;
    public ParticleSystem speepUpParticle;

    private Rigidbody mRb;
    Material skyMat;
    private bool mIsDesktop = false;
    private Vector2 mMovement;
    private Vector2 mSmoothMovement;
    private float mMoveSpeed;
    private float mSmoothMoveSpeed;

    private void Start()
    {
        mRb = GetComponent<Rigidbody>();
        skyMat = RenderSettings.skybox;

#if UNITY_WEBGL
        if (!Application.isMobilePlatform && !Application.isConsolePlatform)
        {
            mIsDesktop = true;
        }
#elif UNITY_STANDALONE
        isDesktop = true;
#endif
        mMoveSpeed = speed;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        mMovement.Set(inputMovement.x, inputMovement.y);
    }

    void CalculateMovementInputSmoothing()
    {
        mSmoothMovement = Vector3.Lerp(mSmoothMovement, mMovement, Time.deltaTime * movementSmoothingSpeed);
        mSmoothMoveSpeed = Mathf.Lerp(mSmoothMoveSpeed, mMoveSpeed, Time.deltaTime * movementSmoothingSpeed);
    }

    void Update()
    {
        CalculateMovementInputSmoothing();

        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //var zRotate = moveHorizontal * -tilt;
        //var yRotate = moveHorizontal * tilt;
        //var xRotate = moveVertical * -tilt;
        //rigid.rotation = Quaternion.Euler(moveVertical * -tilt, yRotate, zRotate);

        //angle += Time.deltaTime * 2f;
        //if (angle > 360) angle -= 360;
        //skyMat.SetFloat("_Rotation", angle);
    }

    public void OnThurst(InputAction.CallbackContext button)
    {
        if (button.performed)
        {
            speepUpParticle.Play();
            mMoveSpeed = speed * thurster;
        }
        else
        {
            speepUpParticle.Stop();
            mMoveSpeed = speed;
        }
    }

    private void FixedUpdate()
    {
        var angleX = 30;
        var angleY = 35;
        var rotationSpeed = 50;

        mRb.velocity = transform.forward * mSmoothMoveSpeed;

        transform.Rotate(Vector3.up, mSmoothMovement.x * rotationSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(mSmoothMovement.y * -angleY, transform.localEulerAngles.y, mSmoothMovement.x * -angleX);
    }
}
