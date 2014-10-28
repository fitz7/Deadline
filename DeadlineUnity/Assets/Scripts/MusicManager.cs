using UnityEngine;
using System.Collections;

public class MusicManager : UnityObserver
{
    public AudioSource death;
    public AudioSource crunch;
    public AudioSource weapon;
    private static MusicManager _instance;
    private AudioSource mainAudio;

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
        mainAudio = GetComponent<AudioSource>();
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
        if (!mainAudio.isPlaying)
        {
            mainAudio.Play();
        }
    }
}