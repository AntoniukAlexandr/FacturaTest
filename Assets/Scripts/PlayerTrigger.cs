using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{    
    private IDamageable _player;
    public event UnityAction UpdateChunks;
    public event UnityAction Victory;    
    
    public void Start()
    {
        _player = GetComponent<IDamageable>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            Vector3 direction = other.gameObject.transform.position - transform.position;            
            damageable.ApplyDamage(110f, transform.position, direction);

            _player.ApplyDamage(10f, transform.position, direction);
        }
        
        if (other.gameObject.CompareTag("TriggerCreateNewChunk"))
        {
            UpdateChunks?.Invoke();
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Victory?.Invoke();
        }
    }    
}