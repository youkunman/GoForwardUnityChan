using UnityEngine;
using System.Collections;

public class UnityChanController : MonoBehaviour {
        //アニメーションするためのコンポーネントを入れる
        Animator animator;

        //Unityちゃんを移動させるコンポーネントを入れる
        Rigidbody2D rigid2D;

        // 地面の位置
        private float groundLevel = -3.0f;

        // ジャンプの速度の減衰
        private float dump = 0.8f;

        // ジャンプの速度
        float jumpVelocity = 20;

        // ゲームオーバになる位置（追加）
        private float deadLine = -9;


        // Use this for initialization
        void Start () {
                // アニメータのコンポーネントを取得する
                this.animator = GetComponent<Animator> ();
                // Rigidbody2Dのコンポーネントを取得する
                this.rigid2D = GetComponent<Rigidbody2D> ();

        }
        // Update is called once per frame
        void Update () {
                // 走るアニメーションを再生するために、Animatorのパラメータを調節する
                this.animator.SetFloat("Horizontal", 1);

                // 着地しているかどうかを調べる
                bool isGround = (transform.position.y > this.groundLevel) ? false : true;
                this.animator.SetBool ("isGround", isGround);

                GetComponent<AudioSource> ().volume = (isGround) ? 1:0;

                if (Input.GetMouseButton (0) && isGround) {

                	this.rigid2D.velocity = new Vector2 (0, this.jumpVelocity);
                }

                if (Input.GetMouseButton (0) == false) {
                	if (this.rigid2D.velocity.y > 0) {
                		this.rigid2D.velocity *= this.dump;
                	}
                }

                if (transform.position.x < this.deadLine){
                        GameObject.Find("Canvas").GetComponent<UIController> ().GameOver ();

                        Destroy (gameObject);
                }
        }
}