/// Ollie Padfield
/// 09.05.19 - 4
/// 
/// 10.03.19 - 3
/// 
/// 19.02.19 - 2
///
/// 0.02.19

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;
using System.Text;

public class EnemyAI : MonoBehaviour {
    [SerializeField] private float _speed = 1f;
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private int _difficulty;

    private Vector3 _targetPos;
    private Quaternion _targetRot;
    private Rigidbody _rb;

    private enum DIFFICULTY { EASY, MEDIUM, HARD };

    private float timer = 0.0f;
    private float cooldownTime = 2.0f;
    private float attackRange = 4f;
    private float damage = 1.0f;
    private playerController player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("playerHealth").GetComponent<playerController>();

        

        _targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);
        Vector3 delta = new Vector3(_targetPos.x - transform.position.x, 0.0f, _targetPos.z - transform.position.z);
        transform.rotation = Quaternion.LookRotation(delta);
        //_targetPos.y = transform.position.y;
        //transform.forward = _targetPos - transform.position;

        GameObject _gameController = GameObject.FindGameObjectWithTag("GameController");

        switch (_difficulty) {
            case (int)DIFFICULTY.EASY:
                _nameText.text = _gameController.GetComponent<VoiceRecognition>()._listOfNames[Random.Range(0, 26)];
                break;

            case (int)DIFFICULTY.MEDIUM:
                _nameText.text = _gameController.GetComponent<VoiceRecognition>()._listOfNames[Random.Range(26, 52)];
                break;

            case (int)DIFFICULTY.HARD:
                _nameText.text = _gameController.GetComponent<VoiceRecognition>()._listOfNames[Random.Range(52, 78)];
                break;

            default:
                _nameText.text = _gameController.GetComponent<VoiceRecognition>()._listOfNames[Random.Range(0, 26)];
                break;
        }
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        _targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);
        Vector3 delta = new Vector3(_targetPos.x - transform.position.x, 0.0f, _targetPos.z - transform.position.z);
        transform.rotation = Quaternion.LookRotation(delta);
        //_targetPos.y = transform.position.y;
        //transform.forward = _targetPos - transform.position;

        if (_rb)
        {
            _rb.velocity = new Vector3((transform.forward * _speed).x, _rb.velocity.y, (transform.forward * _speed).z);
        }
    }

    void Update() {
        _targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        timer += Time.deltaTime;

        if (timer > cooldownTime) {
            if (Vector3.Distance(transform.position, _targetPos) < attackRange) {

                attack();
                timer = 0.0f;
            }
        }
    }

    void attack() {
        player.takeDamage();
    }
}