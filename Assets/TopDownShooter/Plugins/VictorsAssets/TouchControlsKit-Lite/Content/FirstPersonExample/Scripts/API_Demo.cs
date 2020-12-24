using UnityEngine;
using TouchControlsKit;

public class API_Demo : MonoBehaviour
{
    public bool windowsEnabled = false;

    private int screenWidth, screenHeight;

    [HideInInspector]
    public Rect hideBtnSize, disBtnSize;
    [HideInInspector]
    public Rect leftWindow, rightWindow;

    private bool showingTouchzones = true;


    // Update is called once per frame
    void Update()
    {
        if( screenWidth != Screen.width || screenHeight != Screen.height )
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;

            disBtnSize.x = screenWidth - ( screenWidth / 100f * 57.5f );
            disBtnSize.y = 5f;
            disBtnSize.width = screenWidth / 100f * 15.25f;
            disBtnSize.height = screenHeight / 14f;

            hideBtnSize.x = screenWidth - ( screenWidth / 100f * 57.5f );
            hideBtnSize.y = disBtnSize.height + 12f;
            hideBtnSize.width = screenWidth / 100f * 15.25f;
            hideBtnSize.height = screenHeight / 14f;

            //
            leftWindow.x = 5f;
            rightWindow.x = screenWidth - ( screenWidth / 2.45f );
            rightWindow.y = leftWindow.y = 5f;
            rightWindow.width = leftWindow.width = screenWidth / 2.5f;
            rightWindow.height = leftWindow.height = screenHeight / 2f;
        }
    }

    // OnGUI
    void OnGUI()
    {
        if( GUI.Button( disBtnSize, "Enable / Disable \nControllers" ) )
        {
            TCKInput.SetActive( !TCKInput.isActive );
        }

        if( !TCKInput.isActive )
            return;

        if( GUI.Button( hideBtnSize, "Show / Hide \nTouch Zones" ) )
        {
            showingTouchzones = !showingTouchzones;
            TCKInput.ShowingTouchZone( showingTouchzones );
        }     

        // Left Window
        if( windowsEnabled )
        {
            GUILayout.BeginArea( leftWindow );
            GUILayout.BeginVertical( "Box" );

            SetGuiStyle( "<b>Joystick</b>" );

            Axes( "Joystick" );
            //Sens( "Joystick" );

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        // Right Window
        if( windowsEnabled )
        {
            GUILayout.BeginArea( rightWindow );
            GUILayout.BeginVertical( "Box" );

            SetGuiStyle( "<b>Touchpad</b>" );

            Axes( "Touchpad" );
            Sens( "Touchpad" );
            
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }  

    
    // Sens
    private void Sens( string ctrlName )
    {
        float sensitivity = TCKInput.GetSensitivity( ctrlName );
        sensitivity = customSlider( "Sensitivity", sensitivity, 1f, 10f );
        TCKInput.SetSensitivity( ctrlName, sensitivity );
    }
    
    // Axes
    private void Axes( string ctrlName )
    {
        GUILayout.BeginHorizontal();

        bool enableX = TCKInput.GetAxisEnable( ctrlName, EAxisType.Horizontal );
        GUILayout.BeginHorizontal();
        GUILayout.Label( "Enable X Axis", GUILayout.Width( 115 ) );
        enableX = GUILayout.Toggle( enableX, string.Empty );
        GUILayout.EndHorizontal();
        TCKInput.SetAxisEnable( ctrlName, EAxisType.Horizontal, enableX );

        if( enableX )
        {
            bool inverseX = TCKInput.GetAxisInverse( ctrlName, EAxisType.Horizontal );
            GUILayout.BeginHorizontal();
            GUILayout.Label( "Inverse X", GUILayout.Width( 60 ) );
            inverseX = GUILayout.Toggle( inverseX, string.Empty );
            GUILayout.EndHorizontal();
            TCKInput.SetAxisInverse( ctrlName, EAxisType.Horizontal, inverseX );
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        bool enableY = TCKInput.GetAxisEnable( ctrlName, EAxisType.Vertical );
        GUILayout.BeginHorizontal();
        GUILayout.Label( "Enable Y Axis", GUILayout.Width( 115 ) );
        enableY = GUILayout.Toggle( enableY, string.Empty );
        GUILayout.EndHorizontal();
        TCKInput.SetAxisEnable( ctrlName, EAxisType.Vertical, enableY );

        if( enableY )
        {
            bool inverseY = TCKInput.GetAxisInverse( ctrlName, EAxisType.Vertical );
            GUILayout.BeginHorizontal();
            GUILayout.Label( "Inverse Y", GUILayout.Width( 60 ) );
            inverseY = GUILayout.Toggle( inverseY, string.Empty );
            GUILayout.EndHorizontal();
            TCKInput.SetAxisInverse( ctrlName, EAxisType.Vertical, inverseY );
        }
        GUILayout.EndHorizontal();
    }


    // SetGuiStyle
    private void SetGuiStyle( string labelName )
    {
        GUIStyle style = GUI.skin.GetStyle( "Label" );
        style.richText = true;
        style.alignment = TextAnchor.UpperCenter;
        style.normal.textColor = Color.red;
        GUILayout.Label( labelName, style );
        style.richText = false;
        style.alignment = TextAnchor.UpperLeft;
        style.normal.textColor = Color.white;
    }

    // customSlider
    private float customSlider( string label, float currentValue, float minValue, float maxValue )
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label( label, GUILayout.Width( 115f ) );
        currentValue = GUILayout.HorizontalSlider( currentValue, minValue, maxValue );
        GUILayout.Space( 10f );
        GUILayout.Label( string.Format( "{0:F2}", currentValue ), GUILayout.MaxWidth( 50f ) );
        GUILayout.EndHorizontal();
        return currentValue;
    }
}