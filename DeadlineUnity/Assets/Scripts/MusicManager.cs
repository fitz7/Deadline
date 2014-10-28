using UnityEngine;
using System.Collections;

public class MusicManager : UnityObserver
{
    public AudioSource death;
    public AudioSource crunch;
    public AudioSource weapon;
    public AudioSource musicTwo;
    private static MusicManager _instance;
    private bool toggle;

    public static MusicManager instance
    {
        get
        {
            if ( _instance == null )
            {
                _instance = GameObject.FindObjectOfType<MusicManager>( );
                DontDestroyOnLoad( _instance.gameObject );
            }
            return _instance;
        }
    }

    void Awake( )
    {
        if ( _instance == null )
        {
            _instance = this;
            DontDestroyOnLoad( this );
        }
        else
        {
            if ( this != _instance )
            {
                Destroy( this.gameObject );
            }
        }
    }

    public override void OnNotify(Object sender, EventArguments e)
    {
        if( e.eventMessage == "DEATH_SOUND" )
        {
            death.Play();
        }
        if (e.eventMessage == "CRUNCH_SOUND")
        {
            crunch.Play();
        }
        if (e.eventMessage == "WEAPON_SOUND")
        {
            weapon.Play();
        }
    }

    public void Update()
    {
        if ( !musicTwo.isPlaying && !toggle )
        {
            this.GetComponent<AudioSource>().Play( );
            toggle = true;
        }
        else if ( !this.GetComponent<AudioSource>().isPlaying && toggle )
        {
            musicTwo.Play( );
            toggle = false;
        }
    }
}