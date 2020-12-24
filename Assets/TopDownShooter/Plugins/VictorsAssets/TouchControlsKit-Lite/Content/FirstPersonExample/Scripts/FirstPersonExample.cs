using UnityEngine;
using TouchControlsKit;

namespace Examples
{
    public class FirstPersonExample : MonoBehaviour
    {
        bool binded;
        Transform myTransform, cameraTransform;
        CharacterController controller;
        float rotation;
        bool jump, prevGrounded, isPorjectileCube;
        float weapReadyTime;
        bool weapReady = true;


        // Awake
        void Awake()
        {
            myTransform = transform;
            cameraTransform = Camera.main.transform;
            controller = GetComponent<CharacterController>();
        }

        // Update
        void Update()
        {
            if( weapReady == false )
            {
                weapReadyTime += Time.deltaTime;
                if( weapReadyTime > .15f )
                {
                    weapReady = true;
                    weapReadyTime = 0f;
                }
            }


            if( TCKInput.GetAction( "jumpBtn", EActionEvent.Down ) )
            {
                Jumping();
            }

            if( TCKInput.GetAction( "fireBtn", EActionEvent.Press ) )
            {
                PlayerFiring();
            }

            Vector2 look = TCKInput.GetAxis( "Touchpad" );
            PlayerRotation( look.x, look.y );
        }

        // FixedUpdate
        void FixedUpdate()
        {
            /*float moveX = TCKInput.GetAxis( "Joystick", EAxisType.Horizontal );
            float moveY = TCKInput.GetAxis( "Joystick", EAxisType.Vertical );*/
            Vector2 move = TCKInput.GetAxis( "Joystick" ); // NEW func since ver 1.5.5
            PlayerMovement( move.x, move.y );
        }


        // Jumping
        private void Jumping()
        {
            if( controller.isGrounded )
                jump = true;
        }

                        
        // PlayerMovement
        private void PlayerMovement( float horizontal, float vertical )
        {
            bool grounded = controller.isGrounded;

            Vector3 moveDirection = myTransform.forward * vertical;
            moveDirection += myTransform.right * horizontal;

            moveDirection.y = -10f;

            if( jump )
            {
                jump = false;
                moveDirection.y = 25f;
                isPorjectileCube = !isPorjectileCube;
            }

            if( grounded )            
                moveDirection *= 7f;
            
            controller.Move( moveDirection * Time.fixedDeltaTime );

            if( !prevGrounded && grounded )
                moveDirection.y = 0f;

            prevGrounded = grounded;
        }

        // PlayerRotation
        public void PlayerRotation( float horizontal, float vertical )
        {
            myTransform.Rotate( 0f, horizontal * 12f, 0f );
            rotation += vertical * 12f;
            rotation = Mathf.Clamp( rotation, -60f, 60f );
            cameraTransform.localEulerAngles = new Vector3( -rotation, cameraTransform.localEulerAngles.y, 0f );
        }
        
        // PlayerFiring
        public void PlayerFiring()
        {
            if( !weapReady )
                return;

            weapReady = false;

            GameObject primitive = GameObject.CreatePrimitive( isPorjectileCube ? PrimitiveType.Cube : PrimitiveType.Sphere );
            primitive.transform.position = ( myTransform.position + myTransform.right );
            primitive.transform.localScale = Vector3.one * .2f;
            Rigidbody rBody = primitive.AddComponent<Rigidbody>();
            Transform camTransform = Camera.main.transform;
            rBody.AddForce( camTransform.forward * Random.Range( 25f, 35f ) + camTransform.right * Random.Range( -2f, 2f ) + camTransform.up * Random.Range( -2f, 2f ), ForceMode.Impulse );
            Destroy( primitive, 3.5f );
        }

        // PlayerClicked
        public void PlayerClicked()
        {
            //Debug.Log( "PlayerClicked" );
        }
    };
}