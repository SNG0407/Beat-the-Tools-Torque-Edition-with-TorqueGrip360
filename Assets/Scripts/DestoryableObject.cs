using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryableObject : MonoBehaviour
{

    Rigidbody rb;
    float power = 10f;
    private int HitCount = 0;
    bool move = true;
    bool isHit = false;
    private Dinosaur_UImanager UImanager;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
        //m_Particles = new ParticleSystem.Particle[WFX_19_StarTail_GazFire.main.maxParticles];
    }
 
    void Update()
    {
           //1. 랜덤 시간마다 랜덤 위치로 별 생성하기

        //2. 생성된 별 떨어지기
        // float xMove = Input.GetAxis("Horizontal");
        // float zMove = Input.GetAxis("Vertical");
 
        // Vector3 getVel = new Vector3(xMove, 0, zMove) * speed;
        // rb.velocity = getVel;
        if(move){
         rb.AddForce(Vector3.left * power);
         rb.velocity = new Vector3(-power,-5f,0);
        }
    }

    public void MoveStars(){
        Debug.Log("MoveStars");

    }

    public void StarDestoryed(Vector3 Positon){
        HitCount++;
        Debug.Log("This star is hit "+ HitCount+" times");
        if (HitCount >= 3 && !isHit)
        {
            Debug.Log("It's hit more than 3 times, so destroy it");
            isHit=true;
            Dinosaur_UImanager.Instance.HitFunction();

            this.gameObject.transform.Find("CFXR Fire Breath").gameObject.SetActive(false);
            this.gameObject.GetComponent<StarEffectManger>().WFX_6_StarDestroy_ExplosiveSmoke.gameObject.SetActive(true);
            this.gameObject.GetComponent<StarEffectManger>().Effect_WFX_6_StarDestroy_ExplosiveSmoke();
            this.gameObject.transform.Find("BeveledCube_0").gameObject.SetActive(false);
            this.gameObject.transform.Find("BeveledCube_1").gameObject.SetActive(false);

             //UI 업데이트

            Destroy(gameObject, 0.3f);
            
        }
        else
        {
            this.gameObject.GetComponent<StarEffectManger>().WFX_4_StarDestroy_Explosion.gameObject.transform.position = Positon;
            this.gameObject.GetComponent<StarEffectManger>().WFX_4_StarDestroy_Explosion.gameObject.SetActive(true);
            this.gameObject.GetComponent<StarEffectManger>().Effect_StarDestroy_Explosion();
        }
        

        
        //Destroy(this, .5f);

    }

    private void StarOnTheGround(){
        Debug.Log("StarOnTheGround");

    }

    private void StarOnTheGround_Effect(){
        //Debug.Log("StarOnTheGround_Effect");
        //StartCoroutine("OnTheGround_Effect");
        this.gameObject.transform.Find("WFX_19_StarTail_GazFire").gameObject.SetActive(false);
        this.gameObject.GetComponent<StarEffectManger>().WFX_3_Groud_ExplosionLandMine.gameObject.SetActive(true);
        this.gameObject.GetComponent<StarEffectManger>().Effect_WFX_3_Groud_ExplosionLandMine();
        this.gameObject.transform.Find("BeveledCube_0").gameObject.SetActive(false);
        this.gameObject.transform.Find("BeveledCube_1").gameObject.SetActive(false);
        Destroy(gameObject, 0.3f);
    }

    private void StarGennerate(){
        Debug.Log("Star's generated at: ");
    }

    private void StarGennerate_Effect(){
        //Debug.Log("StarGennerate_Effect");
    }

    private void OnCollisionEnter(Collision other) {
        //총알과 부딪힌 경우, 땅과 부딪힌 경우

        //Debug.Log("hit item name: "+ other.gameObject.name);
        Debug.Log(other.gameObject.name);
   

        if(other.gameObject.name == "floor" || other.gameObject.name == "Terrain" || other.gameObject.tag =="Wood")
        {
            move=false;

            //UI 업데이트
            Dinosaur_UImanager.Instance.MissFunction();
            rb.velocity = new Vector3(0,0,0);
            StarOnTheGround_Effect();
        }

        //StarDestoryed();
    }

 //   IEnumerator OnTheGround_Effect ()
	//{

 //       while (true)
	//	{
	//		yield return new WaitForSeconds(0.3f);
	//		if(!WFX_19_StarTail_GazFire.IsAlive(true))
	//		{
	//			#if UNITY_3_5
	//					m_System.gameObject.SetActiveRecursively(false);
	//				#else
	//					WFX_19_StarTail_GazFire.gameObject.SetActive(false);
	//				#endif
	//		}
	//	}
	//}
}
