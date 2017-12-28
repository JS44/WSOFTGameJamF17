using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right };//direction we are facing

    public bool onlyRight;//if it is in right mode or not
    public bool hasKey;
    bool canMove = true;

    public AudioSource source;

    public Tile tileBelow;
    public BlockManager blockManager;

    //Hat and cape
    public GameObject hat, cape;
    Renderer hatRend, capeRend;

    //camera stuff
    public Camera moveCam, upCam, downCam, rightCam, leftCam;
    Camera newCam;
    bool cameraMoving = false;
    float camTimer = 0f;
    public GameObject levelCenter;

    public Direction directionFacing = Direction.Up;

	public GameObject levelSelectionPanel;
	public GameObject mainCamera;

    bool moving1 = false, moving2 = false;
    float moveTimer = 0f;
    float moveSpeed = .5f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(whereDoIGo());
        source = this.GetComponent<AudioSource>();
        moveCam.transform.LookAt(levelCenter.transform);
        hatRend = hat.GetComponent<Renderer>();
        capeRend = cape.GetComponent<Renderer>();
        if (onlyRight)
        {
            hatRend.material.SetColor( "_Color", Color.red);
            capeRend.material.SetColor("_Color", Color.red);
        }
        else
        {
            hatRend.material.SetColor( "_Color", Color.blue);
            capeRend.material.SetColor("_Color", Color.blue);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //account for block undulation when standing still
        if (!moving1 && !moving2)
        {
            float wavey = this.transform.position.y - tileBelow.transform.position.y - .65f;
            if (wavey != 0f)
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - wavey, this.transform.position.z);
        }


        /////////////
        //MOVEMENT///
        /////////////
        if (moving1 && moveTimer < moveSpeed / 2f)
        {
            this.transform.position = Vector3.Slerp(this.transform.position,
                new Vector3((tileBelow.transform.position.x + this.transform.position.x) / 2f, tileBelow.transform.position.y + .85f, 
                (tileBelow.transform.position.z + this.transform.position.z) / 2f), moveTimer / moveSpeed);
            moveTimer += Time.deltaTime;
        }
        else if (moveTimer > moveSpeed / 2f && moving1)
        {
            moveTimer = 0;
            moving1 = false;
            moving2 = true;
        }

        if (moving2 && moveTimer < moveSpeed / 2f)
        {
            this.transform.position = Vector3.Slerp(this.transform.position, 
                new Vector3(tileBelow.transform.position.x, tileBelow.transform.position.y + .65f, tileBelow.transform.position.z), moveTimer / moveSpeed);
            moveTimer += Time.deltaTime;
        }
        else if (moveTimer > moveSpeed / 2f && moving2)
        {
            moveTimer = 0;
            moving2 = false;
            canMove = true;
        }

        //implementation of tile based movement
        if (canMove)
        {
            if (tileBelow.END && hasKey)
            {
                endScene();
                canMove = false;
            }

            Tile newTile = null;
            //holy movement batman
            bool rightPressed = false, leftPressed = false, upPressed = false, downPressed = false;
            int buttonsPressed = 0;
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                rightPressed = true;
                buttonsPressed++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                leftPressed = true;
                buttonsPressed++;

            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                upPressed = true;
                buttonsPressed++;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                downPressed = true;
                buttonsPressed++;
            }

            if (buttonsPressed == 1)
            {
                canMove = false;
                if (rightPressed)
                {
                    if (onlyRight)
                    {
                        switch (directionFacing)
                        {
                            case Direction.Up:
                                newTile = tileBelow.east;
                                break;
                            case Direction.Down:
                                newTile = tileBelow.west;
                                break;
                            case Direction.Left:
                                newTile = tileBelow.north;
                                break;
                            case Direction.Right:
                                newTile = tileBelow.south;
                                break;
                        }
                    }
                    else
                    {
                        //play the fail sound
                    }
                }
                if (leftPressed)
                {
                    if (!onlyRight)
                    {
                        switch (directionFacing)
                        {
                            case Direction.Up:
                                newTile = tileBelow.west;
                                break;
                            case Direction.Down:
                                newTile = tileBelow.east;
                                break;
                            case Direction.Left:
                                newTile = tileBelow.south;
                                break;
                            case Direction.Right:
                                newTile = tileBelow.north;
                                break;
                        }
                    }
                    else
                    {
                        //play the fail sound
                    }
                }
                if (upPressed)
                {
                    if (!onlyRight)
                    {
                        switch (directionFacing)
                        {
                            case Direction.Up:
                                newTile = tileBelow.north;
                                break;
                            case Direction.Down:
                                newTile = tileBelow.south;
                                break;
                            case Direction.Left:
                                newTile = tileBelow.west;
                                break;
                            case Direction.Right:
                                newTile = tileBelow.east;
                                break;
                        }
                    }
                    else
                    {
                        //play the fail sound
                    }
                }
                if (downPressed)
                {
                    if (!onlyRight)
                    {
                        switch (directionFacing)
                        {
                            case Direction.Up:
                                newTile = tileBelow.south;
                                break;
                            case Direction.Down:
                                newTile = tileBelow.north;
                                break;
                            case Direction.Left:
                                newTile = tileBelow.east;
                                break;
                            case Direction.Right:
                                newTile = tileBelow.west;
                                break;
                        }
                    }
                    else
                    {
                        //play the fail sound
                    }
                }


                if(newTile != null)
                {
                    StartCoroutine(movementCoroutine(newTile));
                }
                else
                {
                    canMove = true;
                    //play the fail sound
                }
            }
            else if (buttonsPressed > 1)
            {
                //play the fail sound
            }

        }

        //camera moving
        if (camTimer < 1.5f && cameraMoving)
        {
            moveCam.transform.position = Vector3.Slerp(moveCam.transform.position, newCam.transform.position, camTimer / 1.5f);
            camTimer += Time.deltaTime;
            moveCam.transform.LookAt(levelCenter.transform);
        }
        else
        {
            camTimer = 0f;
            cameraMoving = false;
        }

        //no longer movement
    }

    IEnumerator movementCoroutine(Tile newTile)
    {
        //end coroutine of where do i go, and you have moved start it again from the new tile
        if (tileBelow.ONETIMETERRAIN)
        {
            tileBelow.TileDestroy();
        }

        //Fan check needs to be at the top so whatever the new tile is, is treated normally
        if (newTile.FANTILE && newTile.FANON)
        {
            tileBelow = newTile;
            moving1 = true;
            yield return new WaitForSeconds(.5f);
            newTile = newTile.Destination;
        }

        if (newTile.DIRSWITCH)
        {
            directionFacing = newTile.newDir;
            //camera switching
            if (directionFacing == Direction.Up)
            {
                newCam = upCam;
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            }
            else if (directionFacing == Direction.Down)
            {
                newCam = downCam;
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            }
            else if (directionFacing == Direction.Right)
            {
                newCam = rightCam;
                
                transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
            }
            else if (directionFacing == Direction.Left)
            {
                newCam = leftCam;
                transform.rotation = Quaternion.Euler(transform.rotation.x, 270, transform.rotation.z);
            }
            blockManager.rotateSwitches(directionFacing);
            cameraMoving = true;
        }
        else if (newTile.CONTROLSWITCH)
        {
            if (newTile.switchRight)
            {
                onlyRight = true;
                hatRend.material.SetColor("_Color", Color.red);
                capeRend.material.SetColor("_Color", Color.red);
            }
            else
            {
                onlyRight = false;
                hatRend.material.SetColor("_Color", Color.blue);
                capeRend.material.SetColor("_Color", Color.blue);
            }
        }
        else if (newTile.KEYTILE)
        {
            hasKey = true;
            newTile.hideKey();
        }
        else if (newTile.FANSWITCH)
        {
            blockManager.flipOnOff();
        }

        tileBelow = newTile;
        moving1 = true;
       // yield return new WaitForSeconds(.5f);
       // canMove = true;
    }

    IEnumerator whereDoIGo()
    {
        //to add some indication of a tile being able to be moved to;
        yield return null;
    }
    

    void endScene()
    {
		levelSelectionPanel.gameObject.SetActive (true);
		mainCamera.GetComponent<cameraRotation> ().enabled = true;
        //SceneManager.LoadScene("LevelSelect");
    }
    


}


