using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    private bool playerInRange = false;
    [SerializeField] private Animator portalAnim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (portalAnim != null)
            {
                portalAnim.SetTrigger("Activate");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (portalAnim != null)
            {
                portalAnim.SetTrigger("NotActivate");
            }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        if (destination != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                player.transform.position = destination.position;
            }
        }
        else
        {
            Debug.LogError("Hedef nokta belirlenmemiş!");
        }
    }
}
