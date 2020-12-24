/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


namespace TouchControlsKit
{
    // Used for identification axis by type X or Y.
    public enum EAxisType : byte
    {
        Horizontal, Vertical
    };

    // Used for select update method of delegates and events invoking.
    public enum EUpdateMode
    {
        Normal,
        Late,
        Fixed,
        OFF = -1
    };

    // Describes phase of a finger touch.
    public enum ETouchPhase
    {
        Began,
        Moved,
        Stationary,
        Ended,
        NoTouch = -1
    };

    // Used for delegates of BindAction API.
    public enum EActionEvent : byte
    {
        Down,
        Press,
        Up,
        Click 
    };

    // Used for DPad arrows.
    public enum EArrowType
    {
        none = -1,
        UP,
        DOWN,
        LEFT,
        RIGHT
    };
}
