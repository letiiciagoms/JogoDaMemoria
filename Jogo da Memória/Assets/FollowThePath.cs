using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    public Transform[] waypoints;
    
    // é usado para tornar campos privados visíveis e editáveis no Inspector da Unity, sem precisar torná-los públicos
    [SerializeField] private float movespeed = 1f;
    // serve para ocultar uma variável pública do Inspector, mesmo que ela esteja acessível por outros scripts
    [HideInInspector] public int waypointIndex = 0;
    public bool moveAllowed = false;

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        if (moveAllowed)
        
            Move();
        
            
           
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            float moveSpeed = 0;
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position,
                moveSpeed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
            
        }
    }
}
