using UnityEngine;

public class Collider : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You're ok!");
                break;
            case "Finish":
                Debug.Log("You beat the level!");
                break;
            default:
                Debug.Log("You took damage!");
                break;
        }
    }
}
