using System.Runtime.InteropServices;

    /* This script simply defines the haptics used for button taps, 
       I used Claude AI to help with this script */
public static class Haptics
{
    public enum Style { Light, Medium, Heavy, Soft, Rigid }

#if UNITY_IOS && !UNITY_EDITOR      //using #if makes it so it is ignored in editor and will only work on device
    [DllImport("__Internal)]
    private static extern void _HapticImpact(int style);
#endif

    public static void Impact(Style style = Style.Light)
    {
#if UNITY_IOS && !UNITY_EDITOR
        _HapticImpact((int)style);
#endif
    }
}

