using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    #region Public Variables
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    public PlayerCombat playerCombat;
    #endregion

    #region Private Variables
    private Animator animator;
    private float distance;
    private bool attackMode;
    private bool cooling;
    private float intTimer;

    [SerializeField] private AudioSource attackSoundEffect;
    #endregion

    void Awake()
    {
        SelectTarget();
        intTimer = timer;
        animator = GetComponent<Animator>();
        playerCombat = FindObjectOfType<PlayerCombat>();
    }
    // Update is called once per frame
    void Update()
    {

        Debug.Log(intTimer);
        if (!attackMode)
        {
            Move();
        }
        
        if(!InsideofLimits() && !inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            EnemyLogic();
        }

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            cooling = false;
            timer = intTimer;
        }
    }


    void EnemyLogic ()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            animator.SetBool("Attack", false);
        }
    }

    void Move ()
    {
        animator.SetBool("IsWalking", true);
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Skel_attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        attackMode = true;

        animator.SetBool("IsWalking", false);
        animator.SetBool("Attack", true);
        attackSoundEffect.Play();

        playerCombat.TakeDamage(10);
        cooling = true;
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("Attack", false);
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }

        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }
}
