using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies2 : MonoBehaviour
{
    private Animator anima;
    private Quaternion angulo;
    private float grado;
    [SerializeField] private int rutina;
    [SerializeField] private float contador;
    private GameObject target;
    [SerializeField] private bool atacando;
    [SerializeField] private float HP = 120;
    [SerializeField] private float Defence = 5;
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ZombieBehaviour();
    }

    private void OnTriggerEnter(Collider collision)   
    {
        if /*(collision.gameObject.tag == "Bullet")*/ (collision.GetComponent<BulletScript>())
        {
            Debug.Log("PUM");
            HP -= collision.GetComponent<BulletScript>().Damage;
        }
        else
        {
            Debug.Log("not pum...");
        }
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void ZombieBehaviour()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 20)
        {

            anima.SetBool("Run", false);
            contador += 1 * Time.deltaTime;
            if (contador >= 4)
            {
                rutina = Random.Range(0, 2);
                contador = 0;
            }
            switch (rutina)
            {
                case 0:
                    anima.SetBool("Walk", false);
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);

                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    anima.SetBool("Walk", true);
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 4 && !atacando)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 10);

                anima.SetBool("Walk", false);
                anima.SetBool("Run", true);

                transform.Translate(Vector3.forward * 2 * Time.deltaTime);

                anima.SetBool("Attack", false);
            }
            else
            {
                anima.SetBool("Run", false);
                anima.SetBool("Walk", false);
                anima.SetBool("Attack", true);
                atacando = true;
            }
        }
    }

    private void FinalAnima()
    {
        anima.SetBool("Attack", false);
        atacando = false;
    }
}
