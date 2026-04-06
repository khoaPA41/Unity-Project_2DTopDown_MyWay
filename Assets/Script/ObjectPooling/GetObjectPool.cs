using System.Collections.Generic;
using UnityEngine;
public class GetObjectPool : MonoBehaviour
{
    [SerializeField] ObjectPooling objectPooling;
    [SerializeField] int objectQuantity;
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 endPos;
    List<Vector2> posExits;

    void Start()
    {
        posExits = new List<Vector2>();
        for (int i = 0; i < objectQuantity; i++)
        {
            foreach (var objectPool in objectPooling.poolObjectList)
            {
                Vector2 calculatePos = RandomPos();
                objectPooling.GetObjectPooled(objectPool.name, calculatePos);
            }
        }
    }

    Vector2 RandomPos()
    {
        Vector2 newPos;
        int maxTry = 50;
        do
        {
            int randomX = Random.Range((int)startPos.x, (int)endPos.x + 1);
            int randomY = Random.Range((int)startPos.y, (int)endPos.y + 1);
            newPos = new Vector2(randomX, randomY);

            maxTry--;
            if (maxTry <= 0)
            {
                break;
            }

        } while (IsTooClose(newPos));

        posExits.Add(newPos);
        return newPos;
    }

    bool IsTooClose(Vector2 newPos)
    {
        foreach (var pos in posExits)
        {
            if (Vector2.Distance(pos, newPos) < 1f)
                return true;
        }
        return false;
    }
}
