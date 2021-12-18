using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{    
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Text _collectedText;
    [SerializeField]
    private GameObject _tearPrefab;
    [SerializeField]
    private float _shotSpeed;   
    [SerializeField]
    private float _tearDelay;
    [SerializeField]
    private float _range;
    [SerializeField]
    private Transform _head;

    private Rigidbody2D _rigidbody;
    private float _lastTear;
    private int _collectedAmount = 0;

    public int CollectedAmount { get => _collectedAmount; set => _collectedAmount = value; }
    public float Range { get => _range; set => _range = value; }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        if((shootHorizontal != 0f || shootVertical != 0f) && Time.time > _lastTear + _tearDelay)
        {
            Shoot(shootHorizontal, shootVertical);
            _lastTear = Time.time;
        }

        _rigidbody.velocity = new Vector2(horizontal * _speed, vertical * _speed);
        _collectedText.text = "Items collected: " + CollectedAmount;
    }

    private void Shoot(float x, float y)
    {
        GameObject tear = Instantiate(_tearPrefab, _head.transform.position, _head.transform.rotation) as GameObject;
        tear.AddComponent<Rigidbody2D>().gravityScale = 0f;
        tear.GetComponent<Rigidbody2D>().velocity = new Vector2(
            (x < 0) ? Mathf.Floor(x) * _shotSpeed : Mathf.Ceil(x) * _shotSpeed,
            (y < 0) ? Mathf.Floor(y) * _shotSpeed : Mathf.Ceil(y) * _shotSpeed    
        );
    }
}
