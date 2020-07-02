using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Person
{
    [Header("Enemy Vars")]
    [Space(20)]

    [Range(0.25f, 5f)]
    public float timeBetweensteps;
    public float stopAtRange = 4, moveForceMultiplier, timeMultiplier = 1, reach = 4;
    public int coinSpawnNumber;
    public GameObject coinPref, preSpawnPref;
    public bool visible, zombieOnBurn = true, hasHealthSlider;
    public Slider2D healthSlider;

    public AudioSource ass;

    public IndicatorArrow indicator;

    [SerializeField] // for assigning by hand in tests
    private Transform _target; // this will be given to enemy by crowd system
    [SerializeField]
    private Vector2 targetDistance;

    private float _time;

    private CrowdSystem crowdSystem;
    private bool gotCS, alive = true;

    public void AssignTarget(Transform target)
    {
        _target = target;
    }

    public void AssignCS(CrowdSystem cs)
    {
        crowdSystem = cs;
        gotCS = true;
    }

    protected override void Start()
    {
        base.Start();

        if (hasHealthSlider)
        {
            health.AssignSlider(healthSlider);
        }

        Instantiate(preSpawnPref, transform.position, Quaternion.identity).GetComponent<PreSpawn>().AddGOActivation(gameObject);
        gameObject.SetActive(false);
    }

    protected override void Update()
    {
        if (PlayerController.isAlive)
        {
            _time += Time.deltaTime;
            if (_time > timeBetweensteps * timeMultiplier)
            {
                _time = 0;
                Tick();
            }
        }
    }

    protected override void OnDamage()
    {
        base.OnDamage();
    }

    protected override void OnDie()
    {
        if (alive)
        {
            alive = false;
            Statics.instance.GlitchForS(0.1f);

            if (gotCS)
            {
                crowdSystem.GotKill(indicator);
            }

            Instantiate(Statics.instance.scoreText, transform.position, Quaternion.identity).GetComponent<TextMesh>().text = coinSpawnNumber * CrowdSystem.combo + "";

            for (int i = 0; i < coinSpawnNumber * CrowdSystem.combo; i++)
            {
                GameObject c = Instantiate(coinPref, transform.position, Quaternion.identity);
                c.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            }

            base.OnDie();
        }
    }

    protected override void OnBurn()
    {
        base.OnBurn();
        timeMultiplier = 0.5f;
    }

    private void Tick()
    {
        if (!_burnable.burning || !zombieOnBurn)
        {
            Move();
            Shoot();
        }
        else
        {
            RandomRun();
        }
    }

    private void Shoot()
    {
        CheckFacing(_target.position);

        Vector3 diff = _target.position - transform.position;
        diff.z = 0;

        if (diff.magnitude < reach)
            rightHand.Trigger(diff);
    }

    private void Move()
    {
        targetDistance = _target.position - transform.position;
        if (targetDistance.magnitude > stopAtRange)
        {
            Vector2 moveDir = (targetDistance.normalized + new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)));

            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(transform.position, moveDir, 1);            
            foreach(RaycastHit2D hit in hits)
            {
                if(hit.collider.gameObject.tag == "Wall")
                {
                    moveDir = Quaternion.Euler(0, 0, 90) * moveDir;
                }
            }

            ReceiveForce(moveDir * moveForceMultiplier);
        }
    }

    private void RandomRun()
    {
        targetDistance = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
        ReceiveForce(targetDistance * moveForceMultiplier);
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }
}