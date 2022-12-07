using UnityEngine;
using UnityEngine.AI;
using Player.Control;

public class EnemyNavMeshScript : MonoBehaviour
{
    private NavMeshAgent EnemyNMAgent;

    [Header("Attacking Variables")]
    public float defaultAttackSpeed = 5f, timeToAttack;
    public float enemyDamage = 10;

    public bool attacking = false;

    [Header("Script For Referencing")]
    public AttackRange objIdentifier;
    public ForSpawningScript forSpawnScript;
    public SphereCollider objIdentifierSphere;
    
    private GameObject player;

    void Start()
    {
        EnemyNMAgent = transform.GetComponent<NavMeshAgent>();

        if(!transform.name.Contains("Lilnerd")) transform.SetParent(GameObject.Find("Enemies").transform);

        objIdentifier = transform.GetChild(0).GetComponent<AttackRange>();
        objIdentifierSphere = transform.GetChild(0).GetComponent<SphereCollider>();

        timeToAttack = defaultAttackSpeed;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player ??= GameObject.FindGameObjectWithTag("Player");
            return;
        }
        
        if(EnemyNMAgent.isOnNavMesh)
        {
            if (!attacking)
            {
                EnemyNMAgent.destination = player.transform.position;
            }
            else
            {
                EnemyNMAgent.isStopped = true;
            }
        }
        

        if(GetComponentInChildren<BossAbilityScript>() == null)
        {
            
            if (objIdentifier.identifiedObj != null && EnemyNMAgent.isOnNavMesh)
            {
                attacking = EnemyNMAgent.remainingDistance <= EnemyNMAgent.stoppingDistance &&
                            objIdentifier.identifiedObj == player;
            }

            if (attacking)
            {
                timeToAttack -= Time.deltaTime;
                if (timeToAttack <= 0)
                {
                    timeToAttack = defaultAttackSpeed;
                    EnemyNMAgent.isStopped = false;
                    attacking = false;
                    timeToAttack = defaultAttackSpeed;
                    attackTarget();
                }
            }
        }
        
    }


    public void attackTarget()
    {
        if (objIdentifier.identifiedObj == player)
        {
            PlayerStatus.changeHealth?.Invoke(-(int)enemyDamage);
            attacking = false;
        }
        
        if (GetComponent<ZombieBossScript>() != null)
        {
            EnemyNMAgent.speed = GetComponent<ZombieBossScript>().StartSpeed;
        }
    }
}
