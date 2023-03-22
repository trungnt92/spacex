using UnityEngine;
using System.Collections;

public class MobileInput : MonoBehaviour
{
    public float speed = 3f;
    public float thurster = 10f;
    private bool mIsWebGLMobile;
    private Rigidbody mRb;

    // Use this for initialization
    void Start()
	{
        mRb = GetComponent<Rigidbody>();
#if UNITY_WEBGL
        if (Application.isMobilePlatform)
        {
            mIsWebGLMobile = true;
        }
#endif
    }

    // Update is called once per frame
    void Update()
	{
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:

                    break;
                case TouchPhase.Moved:
                    float moveHorizontal = Mathf.Clamp(touch.deltaPosition.x / 10f, -1, 1) * (mIsWebGLMobile ? -1 : 1);
                    float moveVertical = Mathf.Clamp(touch.deltaPosition.y / 10f, -1, 1) * (mIsWebGLMobile ? -1 : 1);

                    
                    var angleX = 30;
                    var angleY = 35;
                    var rotationSpeed = 50;

                    transform.Rotate(Vector3.up, moveHorizontal * rotationSpeed * Time.deltaTime);
                    transform.localEulerAngles = new Vector3(moveVertical * -angleY, transform.localEulerAngles.y, moveHorizontal * -angleX);

                    break;
                default:
                    break;
            }
        }

        mRb.velocity = transform.forward * speed;
    }
}

