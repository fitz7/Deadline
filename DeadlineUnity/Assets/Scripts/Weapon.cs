using UnityEngine;
using System.Collections;

enum WeaponType
{
    STAPLER,
    KEYBOARD,
    PENCIL
}
public class Weapon : UnityObserver {

    public const string PickUpWeapon = "PICK_UP_WEAPON";
    private MazeCell currentCell;
    private MazeRoom currentRoom;
    WeaponType weaponType;
    int ammo;
    int damage;

    public override void OnNotify( Object sender, EventArguments e )
    {
        if ( e.eventMessage == PickUpWeapon )
        {
            
        }
    }

    void Start()
    {
        RandomWeapon();
    }

    public void SetInitialLocation( MazeCell cell )
    {
        currentRoom = cell.room;
        SetLocation( cell );
    }

    private void SetLocation( MazeCell cell )
    {
        if ( currentCell != null )
        {
            currentCell.OnPlayerExited( );
        }
        currentCell = cell;
        transform.localPosition = cell.transform.localPosition;
    }

    public void RandomWeapon(){
        int enumCount = WeaponType.GetNames(typeof(WeaponType)).Length;
        System.Random rnd = new System.Random();
        int selection = rnd.Next(0,enumCount);

        if(selection == 0){
           weaponType = WeaponType.STAPLER;
           damage = 3;
        }
        else if(selection == 1)
        {
            weaponType = WeaponType.KEYBOARD;
            damage = 2;
        }else if(selection == 2){
            weaponType = WeaponType.PENCIL;
            damage = 1;
        }
    }
}