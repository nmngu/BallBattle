using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ball;
    public GameObject prefab_player;
    public GameObject prefab_enemy;
    private List<GameObject> list_players;
    private List<GameObject> list_enemies;
    private Vector3 ballPosition;
    void Start()
    {
        ballPosition = new Vector3(Random.Range(0.0f, -6.0f), 0.3f, Random.Range(-4.0f, 4.0f));// y = 0.3 to place ball on plane
        Instantiate(ball, ballPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayhit;
            if (Physics.Raycast(ray, out rayhit) && rayhit.collider.tag == "Ground")
            {
                var position = rayhit.point;
                GameObject player;
                position.y = 0.5f;
                if (position.x <= 0)
                {
                    player = Instantiate(prefab_player, position, Quaternion.identity);

                }
                else
                    Instantiate(prefab_enemy, position, Quaternion.identity);

            }
        }

    }


}
