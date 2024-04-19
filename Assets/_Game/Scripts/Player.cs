using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Brick brickplayerPrefab;
    [SerializeField] float speed;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Animator anim;

    public static Player instance;
    
    private string currentAnimName;

    public Transform player;

    public GameObject people;

    public List<Brick> bricks = new List<Brick>();

    public Vector2 startPosition;

    public Vector3 endPosition;

    public int stack = 0;

    public int score = 0;

    public bool isSwipe = false;

    private bool isMoving = false;

    public Vector3 moveDirection;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ChangeAnim("idle");
        isMoving = false;
        speed = 15.0f;
    }

    void Update()
    {
        if (!isSwipe && Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            isSwipe = true;
            isMoving = true;
        }

        if (isSwipe)
        {
            moveDirection = Direction();
            if (moveDirection != Vector3.zero)
            {
                Move();
            }
        }

        if (isSwipe && Input.GetMouseButtonUp(0))
        {
            isSwipe = false;
            isMoving = false;
        }

        
    }

    private Vector3 Direction()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.mousePosition.y >= startPosition.y + 20)
        {
            //isSwipe = false;
            moveDirection = Vector3.forward;
        }
        else if (Input.mousePosition.y <= startPosition.y - 20)
        {
            //isSwipe = false;
            moveDirection = -Vector3.forward;
        }
        else if (Input.mousePosition.x >= startPosition.x + 20)
        {
            //isSwipe = false;
            moveDirection = Vector3.right;
        }
        else if (Input.mousePosition.x <= startPosition.x - 20)
        {
            //isSwipe = false;
            moveDirection = -Vector3.right;
        }

        return moveDirection;
    }

    private void Move()
    {
        isMoving = true;
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position + moveDirection, Color.red);
        if (Physics.Raycast(transform.position, moveDirection, out hit, 100f, wallLayer))
        {
            if (hit.collider != null)
            {
                //Tìm vị trí của wall và di chuyển đến bên cạnh chúng
                endPosition = transform.position + moveDirection * (hit.distance - 0.5f);
                transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            }
            if (hit.distance < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void AddBrick()
    {
        Brick brick = Instantiate(brickplayerPrefab, player);
        brick.transform.localPosition = new Vector3(0, (stack-1) * 0.25f, 0);
        people.transform.localPosition = new Vector3(0,stack* 0.25f , 0);
        bricks.Add(brick);
        stack++;
    }

    public void RemoveBrick()
    {
        if (bricks.Count > 0)
        {
            Brick brick = bricks[bricks.Count - 1];
            bricks.RemoveAt(bricks.Count - 1);
            Destroy(brick.gameObject);
            stack--;
            people.transform.localPosition = new Vector3(0, (stack-1) * 0.25f, 0);

        }
        else
        {
            score = 0;
            UI.instance.Lose();
            StopPlayer();
        }
    }

    public void ClearBrick()
    {
        foreach (Brick brick in bricks)
        {
            Destroy(brick.gameObject);
        }

        bricks.Clear();
        //score = 0;
        stack = 0;
        people.transform.localPosition = new Vector3(0, 0.25f, 0);
    }
    
    
    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "BrickBuild")
        {
            score++;
            Destroy(collider.gameObject);
            AddBrick();
        }
        
        if (collider.tag == "BrickUnbuild")
        {
            RemoveBrick();
        }
        
        if (collider.tag == "Finish")
        {
            ChangeAnim("win");
            ClearBrick();
            StopPlayer();
            Chest.instance.ChestOpen();
            StartCoroutine(UI.instance.Victory(2.0f));
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("wall");
            rb.velocity = new Vector3(0,0,0);
        }    
    }

    public void StopPlayer()
        {
            isMoving = false;
            speed = 0;
        }


}