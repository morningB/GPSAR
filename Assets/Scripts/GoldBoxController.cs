using System.Collections;
using UnityEngine;

public class GoldBoxController : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinAmount;
    public AudioSource audio;
    public AudioClip clip;
    IEnumerator Start()
    {

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < coinAmount; i++)
        {
            CreateCoin();
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    private void CreateCoin()
    {
        GameObject newCoin = Instantiate(coinPrefab, this.transform.position, Quaternion.identity);
        Rigidbody coinRb = newCoin.GetComponent<Rigidbody>();
        float randomX = Random.Range(-2f, 2f);
        float randomZ = Random.Range(-2f, 2f);
        audio.PlayOneShot(clip);
        coinRb.AddForce(new Vector3(randomX, 10f, randomZ), ForceMode.Impulse);
        coinRb.AddTorque(new Vector3(randomX, randomX, randomX), ForceMode.Impulse);
    }

}
