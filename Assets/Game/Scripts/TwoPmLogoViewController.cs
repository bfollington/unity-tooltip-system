using System.Collections;
using System.Collections.Generic;
using TwoPm.TooltipDemo;
using UnityEngine;
using UnityEngine.Assertions;

public class TwoPmLogoViewController : MonoBehaviour
{
    public ReactToPointer ReactToPointer;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(ReactToPointer);

        ReactToPointer.OnPointerSelected += () => {
            Application.OpenURL("https://twopm.studio");
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
