using UnityEngine;

/// <summary>
/// Physics2Dの拡張クラス
/// </summary>
public static class Physics2DExtentsion
{

    //Rayの表示時間
    private const float RAY_DISPLAY_TIME = 3;

    /// <summary>
    /// Rayを飛ばすと同時に画面に線を描画する
    /// </summary>
    public static RaycastHit2D RaycastAndDraw(Vector2 origin, Vector2 direction, float maxDistance, int layerMask)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, layerMask);

        //衝突時のRayを画面に表示
        if (hit.collider)
        {
            Debug.DrawRay(origin, hit.point - origin, Color.blue);
        }
        //非衝突時のRayを画面に表示
        else
        {
            Debug.DrawRay(origin, direction * maxDistance, Color.red);
        }

        return hit;
    }
    
    /// <summary>
    /// raycast and get closest object
    /// </summary>
    public static RaycastHit2D RaycastClosest(Vector2 origin, Vector2 direction, float maxDistance, int layerMask)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, maxDistance, layerMask);
        if (hits.Length > 0) //if no object was found there is no minimum
        {
            if (!(hits.Length == 1 )) //if we found only 1 and that is the player object there is also no minimum. This can be written in a simplified version but this is more understandable i think.
            {
                float min = hits[0].distance; //lets assume that the minimum is at the 0th place
                int minIndex = 0; //store the index of the minimum because thats hoow we can find our object

                for (int i = 1; i < hits.Length; ++i)// iterate from the 1st element to the last.(Note that we ignore the 0th element)
                {
                    if (hits[i].distance < min) //if we found smaller distance and its not the player we got a new minimum
                    {
                        min = hits[i].distance; //refresh the minimum distance value
                        minIndex = i; //refresh the distance
                    }
                }
                return hits[minIndex];
            }
            else {
                return hits[0];

	        }
        }
        else {
            //return Physics2D.Raycast(origin, direction, maxDistance, layerMask);
            return new RaycastHit2D();
	    }
    }

}
