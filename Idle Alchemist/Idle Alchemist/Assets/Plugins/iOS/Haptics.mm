#import <UIKit/UIKit.h>

extern "C"
{
    void _HapticImpact(int style)
    {
        UIImpactFeedbackStyle s = UIImpactFeedbackStyleLight;

        switch (style)
        {
            case 0: s = UIImpactFeedbackStyleLight;  break;
            case 1: s = UIImpactFeedbackStyleMedium; break;
            case 2: s = UIImpactFeedbackStyleHeavy;  break;
            case 3: s = UIImpactFeedbackStyleSoft;   break;   // iOS 13+
            case 4: s = UIImpactFeedbackStyleRigid;  break;   // iOS 13+
        }

        UIImpactFeedbackGenerator *generator = [[UIImpactFeedbackGenerator alloc] initWithStyle:s];
        [generator prepare];
        [generator impactOccurred];
    }
}