using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float RollSpeed = 6f;// Velocidade de rolamento dos objetos
    public float respawnHeight = 1f;
    public float Z = 8.8f; // Altura onde os objetos serão reposicionados

    void Start()
    {
    }

    Vector3 GetRandomPosition()
    {
        // Gerar uma posição aleatória acima do plano
        float randomX = Random.Range(-8, 8);
        

        // Retornar a posição com a altura de respawnHeight
        return new Vector3(randomX, respawnHeight, Z);
    }

    void Spawn()
    {
        // Reposicionar o objeto
        transform.position = GetRandomPosition();
    }


    void Update()
    {   
        
        transform.Translate(-(Vector3.forward * RollSpeed * Time.deltaTime));
        if (transform.position.z < -9)
        {   
            float random_time = Random.Range(0f,1.2f);
            //wait random_time
            // yield return new WaitForSeconds(random_time);
            Spawn();
        }
    }
}



