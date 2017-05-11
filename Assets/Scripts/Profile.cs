using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour {

    public enum type { Deceased, Sus1, Sus2, Sus3, Sus4 };

    [SerializeField]
    private type _profile;
    [SerializeField]
    Text _description;
    [SerializeField]
    string _flavorText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public type getProfile() {
        return _profile;
    }

    public void setProfileData() {
        _description.text = _flavorText;
        switch (_profile)
        {
            case type.Deceased:
                _description.text = "This is the victim";
                break;
            case type.Sus1:
                _description.text = "This is the first suspect";
                break;
            case type.Sus2:
                _description.text = "This is the second suspect";
                break;
            case type.Sus3:
                _description.text = "This is the third suspect";
                break;
            case type.Sus4:
                _description.text = "This is the fourth suspect";
                break;
            default:
                break;
        }
    }

}
