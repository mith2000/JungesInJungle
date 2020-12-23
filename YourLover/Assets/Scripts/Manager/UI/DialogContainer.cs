using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogContainer : MonoBehaviour
{
    [Header ("Interactable Object")]
    public List<string> healthPotionSentences = new List<string> 
    { "Wow !! It looks so juicyyyyy <3" };
    public List<string> healthCrystalSentences = new List<string>
    { "I met it !! I met it !!", "I've been told that this can increase our muscle (>3<)" };
    public List<string> armorCrystalSentences = new List<string>
    { "Yellow mangow !! It's not poisonous, right (._.?)"};
    public List<string> attackSpeedCrystalSentences = new List<string>
    { "Hey hey !! My Mawa very like this one.", "Should we give it a try on our own (*-*)"};
    public List<string> rainbowCrystalSentences = new List<string>
    { "So pewtiful (*o*)", "But Dappy said 'There’s no rose without a thorn' (-3-)"};
    public List<string> enoughCrystalSentences = new List<string>
    { "Nooooo !!", "Overweight is a nightmare (;^;)", "Believe me !!"};

    [Header("Bosses")]
    public List<string> forestBossSentences = new List<string>
    { "Woof woof !!", "The memories come back every time you scream", "Try to hide !!"};
    public List<string> sandBossSentences = new List<string>
    { "I sense worthy prey..."};
    public List<string> urbanBossSentences = new List<string>
    { "*&^!!#%!^#*!^@^@!(!*&@#!&*^!#!)(#&!@!&%*!($^!*(&@!)", "(&$)!($&()!$&)$!)$*^!^$%^&%!@*!(@^($!"};
}
