using UnityEngine;
using System.Collections;

public class Player : UnityObserver {

    private int health = 20;
    private int MAXHP = 20;
    public const string ATTACK_PLAYER = "ATTACK_PLAYER";

    public GameObject playerDeathAnimation;

    private bool endGame;

    private int baseDamage = 3;

	private MazeCell currentCell;

	private MazeDirection currentDirection;

    private int currentPlayerAmmo = 0;

    private bool runOnce;

    public override void OnNotify(Object sender, EventArguments e)
    {
        if( e.eventMessage == ATTACK_PLAYER )
        {
            AttackPlayer( e.extendedMessageNumber );
        }
    }

	public void SetLocation (MazeCell cell) {
		if (currentCell != null) {
            currentCell.cellIsOccupied = false;
			currentCell.OnPlayerExited();
		}
		currentCell = cell;
        currentCell.cellIsOccupied = true;
		transform.localPosition = cell.transform.localPosition;
        if (!runOnce)
        {
            playerDeathAnimation = Instantiate(playerDeathAnimation) as GameObject;
            playerDeathAnimation.transform.position = transform.localPosition;
            playerDeathAnimation.transform.parent = transform;
            playerDeathAnimation.SetActive(false);
            runOnce = true;
        }
		currentCell.OnPlayerEntered();
        if(currentCell.isExit)
            Subject.Notify(GameManager.NEXT_LEVEL);
        Subject.NotifyExtendedMessage( InGameStats.UPDATE_DAMAGE, baseDamage.ToString( ) );
        Subject.NotifyExtendedMessage( InGameStats.UPDATE_AMMO, currentPlayerAmmo.ToString( ) );
        Subject.NotifyExtendedMessage( InGameStats.UPDATE_HEALTH, health.ToString( ) );
        
	}

	private void Move (MazeDirection direction) {
		MazeCellEdge edge = currentCell.GetEdge(direction);
        if ( edge is MazePassage )
        {
            if ( edge.otherCell.cellIsOccupied )
            {
                AttackMonster( edge.otherCell );
                return;
            }
			SetLocation(edge.otherCell);
		}
	}

    private void AttackMonster( MazeCell occupiedCell )
    {
        occupiedCell.currentMonsterOnCell.AttackEnemy( baseDamage );
        if ( currentPlayerAmmo > 0 )
        {
            currentPlayerAmmo -= 1;
        }
        if( currentPlayerAmmo <= 0 )
        {
            Subject.Notify( InGameStats.CLEAR_WEAPON );
            currentPlayerAmmo = 0;
            baseDamage = 3;
        }
    }

    private void CheckForItems( )
    {
        if ( currentCell.currentItem != null )
        {
            DestroyImmediate( currentCell.currentItem.gameObject );
            string type = currentCell.currentItem.GetItemType();
            if (type == ItemType.Health.ToString())
            {
                health += 2;
                if (health > MAXHP)
                {
                    health = MAXHP;
                }
                Debug.Log("HEALTH PACK 2");
            }
            if (type == ItemType.Armor.ToString())
            {
                health += 4;
                if (health > MAXHP)
                {
                    health = MAXHP;
                }
                Debug.Log("HEALTH PACK 4");
            }
            if (type == ItemType.HealthUp.ToString())
            {
                MAXHP += 5;
                Debug.Log("HEALTH INCREASE");
            }
            Subject.NotifyExtendedMessage( InGameStats.UPDATE_HEALTH, health.ToString( ) );
        }
    }

    private void CheckForWeapon( )
    {
        if ( currentCell.currentWeapon != null )
        {
            baseDamage = 3;
            baseDamage = currentCell.currentWeapon.damage + baseDamage;
            currentPlayerAmmo = currentCell.currentWeapon.ammo;
            Subject.Notify( currentCell.currentWeapon.weaponType.ToString( ) );
            DestroyImmediate( currentCell.currentWeapon.gameObject );
            Subject.NotifyExtendedMessage( InGameStats.UPDATE_DAMAGE, baseDamage.ToString( ) );
            Subject.NotifyExtendedMessage( InGameStats.UPDATE_AMMO, currentPlayerAmmo.ToString( ) );
        }
    }

	private void Look (MazeDirection direction) {
		transform.localRotation = direction.ToRotation();
		currentDirection = direction;
	}

    private void Update()
    {
        if (endGame)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(currentDirection);
            Subject.NotifySendAll( currentCell, OfficeWorker.MOVE_ENEMY, " " );
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(currentDirection.GetNextClockwise());
            Subject.NotifySendAll( currentCell, OfficeWorker.MOVE_ENEMY, " " );
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(currentDirection.GetOpposite());
            Subject.NotifySendAll( currentCell, OfficeWorker.MOVE_ENEMY, " " );
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(currentDirection.GetNextCounterclockwise());
            Subject.NotifySendAll( currentCell, OfficeWorker.MOVE_ENEMY, " " );
        }
        else if ( Input.GetKeyDown( KeyCode.Space ) )
        {
            CheckForItems( );
            CheckForWeapon( );
        }

    }

    public void AttackPlayer( int damage )
    {
        Subject.NotifyExtendedMessage( InGameStats.UPDATE_HEALTH, health.ToString( ) );
        health = health - damage;
        if ( health < 0 )
        {
            endGame = true;
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame( )
    {
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        playerDeathAnimation.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(0);
    }
}