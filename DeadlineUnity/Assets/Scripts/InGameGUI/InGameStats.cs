using UnityEngine;
using System.Collections;

public class InGameStats : UnityObserver {
    public const string UPDATE_HEALTH = "UPDATE_HEALTH";
    public const string UPDATE_AMMO = "UPDATE_AMMO";
    public const string UPDATE_DAMAGE = "UPDATE_DAMAGE";
    public const string CLEAR_WEAPON = "CLEAR_WEAPON";
    public UILabel healthLabel;
    public UILabel ammoLabel;
    public UILabel damageLabel;
    public UISprite weaponSprite;

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
                weaponSprite.spriteName = "X Mark";
                break;
        }
        if ( e.eventMessage == WeaponType.KEYBOARD.ToString( ) )
        {
            weaponSprite.spriteName = "Keyboard (1)";
        }
        if ( e.eventMessage == WeaponType.PENCIL.ToString( ) )
        {
            weaponSprite.spriteName = "Pencil (1)";
        }
        if ( e.eventMessage == WeaponType.STAPLER.ToString( ) )
        {
            weaponSprite.spriteName = "Stapler (1)";
        }
    }
}
