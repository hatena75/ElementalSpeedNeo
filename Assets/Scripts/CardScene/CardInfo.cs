using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    // Mapを用いてオブジェクトにIDを付与する。これによってidとオブジェクト間で互いに指定できるようにする。
    // これは相手と同様の並びである必要があるが、その調整はまあ、頑張る(非同期でなければインスタンス化の順だと思うのでなんとかなるはず)。
    public Dictionary<int, GameObject> myHands = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> enemyHands = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> fields = new Dictionary<int, GameObject>();

    //結局randは今使ってない
    public int seed, seedEnemy;

    //本当はDictionaryを拡張するべき。リファクタリング予定。
    public int GetKey(Dictionary<int, GameObject> dic, GameObject obj){
        var pair = dic.FirstOrDefault( c => c.Value == obj );
        return pair.Key;
    }

    void Awake(){
        //オフライン用の処理
        if( (seed = Connection.seed) == 0){
            seed = (int)UnityEngine.Random.Range(0.0f, 1000.0f);
        }
        if( (seedEnemy = Connection.seedEnemy) == 0){
            seedEnemy = (int)UnityEngine.Random.Range(0.0f, 1000.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
