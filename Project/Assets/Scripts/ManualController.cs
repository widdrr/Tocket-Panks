using UnityEngine;

public class ManualController : TankController
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)) {
            _tank.Angle -= 0.1f;
        }
        
        if (Input.GetKey(KeyCode.LeftArrow)) {
            _tank.Angle += 0.1f;
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            _tank.Power -= 0.1f;
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            _tank.Power += 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            _tank.Shoot();
        }
    }


}