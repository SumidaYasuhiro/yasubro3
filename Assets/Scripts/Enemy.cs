using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] float speed = 2f;

    [Header("Charger")]
    [SerializeField] bool isCharger;
    [SerializeField] float distanceToCharge = 5f;
    [SerializeField] float chargeSpeed = 12f;
    [SerializeField] float prepareTime = 2f;
    [SerializeField] float chargeDuration = 3f;//

    float normalSpeed;//

    bool isCharging = false;
    bool isPreparingCharge = false;

    private int currentHealth;

    Transform target; //Follow target

    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.Find("Player").transform;
        normalSpeed = speed;//
    }

    // Update is called once per frame
    private void Update()
    {
        if (!WaveManager.Instance.WaveRunning()) 
        {
            CancelInvoke();
            return;
        }
        if (isPreparingCharge) return;

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            var playerToTheRight = target.position.x > transform.position.x;
            transform.localScale = new Vector2(playerToTheRight ? -1 : 1, 1);

            if (isCharger && 
                !isCharging && 
                Vector2.Distance(transform.position, target.position) < distanceToCharge)
                {
                    isPreparingCharge = true;
                    Invoke (nameof(StartCharging), prepareTime);//
                }
        }
    }

    void StartCharging()
    {
        if (!WaveManager.Instance.WaveRunning()) return;//

        isPreparingCharge = false;
        isCharging = true;
        speed = chargeSpeed;

        Invoke(nameof(StopCharging), chargeDuration);//
    }

    void StopCharging()
    {
        isCharging = false;
        speed = normalSpeed;
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
