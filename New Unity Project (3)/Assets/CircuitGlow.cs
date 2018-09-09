using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircuitGlow : MonoBehaviour
{
    
    [SerializeField] private GameObject _equipmentSlot;
    private Material _glowMaterial;

    private static Dictionary<string, Color> _colorTable = new Dictionary<string, Color>()
    {
        { "FakePunch" , Color.red },
        { "FakeJump" , Color.green },
        { "FakeSuperPunch", Color.yellow }
    };

    void Awake()
    {
        _glowMaterial = GetComponent<Image>().material;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        FollowMouse[] transforms = _equipmentSlot.GetComponentsInChildren<FollowMouse>();

        Debug.Log(transforms.Length);

        if (transforms.Length == 1)
        {
            if (_colorTable.ContainsKey(transforms[0].name))
            {
                _glowMaterial.SetColor("_GlowyColor", _colorTable[transforms[0].gameObject.name]);
            }
        }
        else
        {
            _glowMaterial.SetColor("_GlowyColor", Color.black);
        }
    }
}
