using UnityEngine;

public class WeaponPosition : MonoBehaviour
{

    [Header("Elements")]
    public Boing Weapon { get; private set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignWeapon(Boing weapon , int weaponLevel) { 
    
        Weapon = Instantiate(weapon , transform);

        Weapon.transform.localPosition = Vector3.zero;
        Weapon.transform.localRotation = Quaternion.identity;

        Weapon.UpgradeTo(weaponLevel);


    }

}
