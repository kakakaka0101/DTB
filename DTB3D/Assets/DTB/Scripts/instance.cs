using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class instance : MonoBehaviour
{
    //落とすアニマルを生成する場所の変数を定義
    [SerializeField]
    private float x =0.0f;
    [SerializeField]
    private float y =0.0f;
    [SerializeField]
    private float z =0.0f;

    [SerializeField]
    private float v = 0.5f;

    //ランダムに生成された0～６の数字に合わせて落とすアニマルを生成できるように、配列を定義
    string[] animal = new string[6] { "animal1", "animal2", "animal3", "animal4", "animal5", "animal6" };

    //生成前のアニマルを操作できるように入れ物を用意
    public GameObject doubutu;

    //生成されたアニマルを操作できるように別の入れ物を定義
    public GameObject clone;

    private int phase = 0;

    private GameObject mainCamobj;
    private Transform _camTransform;
    private Vector3 campos;
    private Quaternion camrot;



    // Start is called before the first frame update
    void Start()
    {
        mainCamobj = Camera.main.gameObject;
        _camTransform = mainCamobj.gameObject.transform;
        campos = _camTransform.position;
        camrot = _camTransform.rotation;

        campos = new Vector3(20f, 45f, -20f);
        camrot = Quaternion.Euler(20f, -45f, 0f);
        _camTransform.position = campos;
        _camTransform.rotation = camrot;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("phase" + phase);
        Debug.Log("campos" + campos);

        //スペースで次のフェーズへ
        //０フェーズ：何もない
        if(phase  == 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {




                    int value = Random.Range(0, 6);

                    doubutu = (GameObject)Resources.Load(animal[value]);
                doubutu.GetComponent<Rigidbody>().useGravity = false;
                clone = Instantiate(doubutu, new Vector3(x, y, z), Quaternion.identity);
                clone.GetComponent<Rigidbody>().useGravity = false;

                campos = new Vector3(0f, 36f, -20f);
                camrot = Quaternion.Euler(0f, 0f, 0f);
                _camTransform.position = campos;
                _camTransform.rotation = camrot;

                phase = 1;

                }
            }

        //１フェーズ：これから落とす動物がいる。左右の移動が可能
        else if(phase == 1)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    clone.transform.position += new Vector3(v, 0.0f, 0.0f);
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    clone.transform.position -= new Vector3(v, 0.0f, 0.0f);
                }

                else if (Input.GetKeyDown(KeyCode.Space))//スペースキーが押されたら落とす
                {

                   campos = new Vector3(20f,36f,0f);
                camrot = Quaternion.Euler(0f,-90f,0f);
                _camTransform.position = campos;
                _camTransform.rotation = camrot;


                    phase = 2;

                }

            }
            //２フェーズ：奥行きの移動が可能
        else if (phase == 2)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                clone.transform.position += new Vector3(0.0f, 0.0f, v);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                clone.transform.position -= new Vector3(0.0f, 0.0f, v);
            }

            else if (Input.GetKeyDown(KeyCode.Space))//スペースキーが押されたら落とす
            {
                clone.GetComponent<Rigidbody>().useGravity = true;

                campos = new Vector3(20f, 45f, -20f);           
                camrot = Quaternion.Euler(20f, -45f, 0f);
                _camTransform.position = campos;
                _camTransform.rotation = camrot;

                phase = 0;
            }
        }
            //３フェーズ：落とした。第０フェーズと同じ

        }
    }

public class CoroutineScript : MonoBehaviour
{

    void Start()
    {
        StartCoroutine("Coroutine");
    }

    private IEnumerator Coroutine()
    {
        //処理１
        Debug.Log("Start");

        //１秒待機
        yield return new WaitForSeconds(1.0f);

        //処理２
        Debug.Log("1秒経過");

        //４秒待機
        yield return new WaitForSeconds(4.0f);

        //処理３
        Debug.Log("５秒経過");

        //コルーチンを終了
        yield break;
    }
}