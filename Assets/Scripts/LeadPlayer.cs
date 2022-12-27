using UnityEngine;

public class LeadPlayer : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject player;
  private Vector3 posOffset = new Vector3(-5, -2, 4);
  private Vector3 rotOffset = new Vector3(0, 20, 0);
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
  void LateUpdate()
  {
    transform.position = player.transform.position + posOffset;
    // transform.rotation = player.transform.rotation + rotOffset;
    transform.eulerAngles = player.transform.eulerAngles + rotOffset;


  }
}
