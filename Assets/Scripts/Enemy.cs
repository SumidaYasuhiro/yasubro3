using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int maxHealth = 100;
    [SerializeField] float speed = 2f;

    private int currentHealth;

    Transform target; //Follow target

    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.Find("Player").transform;        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            var playerToTheRight = target.position.x > transform.position.x;
            transform.localScale = new Vector2(playerToTheRight ? -1 : 1, 1);
        }
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
