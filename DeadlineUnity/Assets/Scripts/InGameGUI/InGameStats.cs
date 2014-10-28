using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameStats : UnityObserver {
    public const string UPDATE_HEALTH = "UPDATE_HEALTH";
    public const string UPDATE_AMMO = "UPDATE_AMMO";
    public const string UPDATE_DAMAGE = "UPDATE_DAMAGE";
    public const string CLEAR_WEAPON = "CLEAR_WEAPON";
    public const string LEVEL_CHANGE = "LEVEL_CHANGE";
    public Text healthLabel;
    public Text ammoLabel;
    public Text damageLabel;
    public Image weaponSprite;
    public Text level;
    public Sprite noWeapon;
    public Sprite keyboard;
    public Sprite stapler;
    public Sprite penicl;

    public override void OnNotify( Object sender, EventArguments e )
    {
        switch ( e.eventMessage )
        { 
            case UPDATE_HEALTH:
                healthLabel.text = e.extendedMessageNumber.ToString( );
                break;
            case UPDATE_DAMAGE:
                damageLabel.text = e.extendedMessageNumber.ToString( );
                break;
            case UPDATE_AMMO:
                ammoLabel.text = e.extendedMessageNumber.ToString( );
                break;
            case CLEAR_WEAPON:
                weaponSprite.sprite = noWeapon;
                break;
            case LEVEL_CHANGE:
                level.text = GameManager.Level.ToString( );
                break;
        }
        if ( e.eventMessage == WeaponType.KEYBOARD.ToString( ) )
        {
            weaponSprite.sprite = keyboard;
        }
        if ( e.eventMessage == WeaponType.PENCIL.ToString( ) )
        {
            weaponSprite.sprite = penicl;
        }
        if ( e.eventMessage == WeaponType.STAPLER.ToString( ) )
        {
            weaponSprite.sprite = stapler;
        }
    }
}
