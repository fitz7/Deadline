using UnityEngine;
using System.Collections;



enum WeaponType
{
    STAPLER,
    KEYBOARD,
    PENCIL
}

public class Weapon : MonoBehaviour {

    public Material[] mats;
    public int ammo;
    public int damage;
    private MazeCell currentCell;
    private MazeRoom currentRoom;
    WeaponType weaponType;

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
        int selection = rnd.Next(0,10);

        if(selection <= 5){
            weaponType = WeaponType.PENCIL;
            damage = 1;
            ammo = 9;
            Material[] pencil = new Material[1];
            pencil[0] = mats[0];
            transform.GetChild(0).renderer.materials = pencil;
        }
        else if(selection > 5 && selection <9)
        {
            weaponType = WeaponType.KEYBOARD;
            damage = 2;
            ammo = 6;
            Material[] keyboard = new Material[1];
            keyboard[0] = mats[1];
            transform.GetChild(0).renderer.materials = keyboard;
        }else if(selection == 9){
            weaponType = WeaponType.STAPLER;
            damage = 3;
            ammo = 3;
            Material[] stapler = new Material[1];
            stapler[0] = mats[2];
            transform.GetChild(0).renderer.materials = stapler;
        }
    }
}