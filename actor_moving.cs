
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jump = 2f;
    public float actionTime = 0.5f;

    // 新增：用陣列儲存移動方向
    private Vector3[] directions =
    {
        Vector3.left,   // [0] 向左
        Vector3.right,  // [1] 向右
        Vector3.up      // [2] 向上
    };

    private bool isActing = false;
    private bool isJumping = false;

    private void Update()
    {
        if (Keyboard.current == null) return;

        // 左右移動：跳躍時也可以移動
        float x = 0f;

        if (Keyboard.current.leftArrowKey.isPressed)
            x = -1f;
        else if (Keyboard.current.rightArrowKey.isPressed)
            x = 1f;

        // 修改：透過陣列取得移動方向
        if (x < 0)
            transform.position += directions[0] * speed * Time.deltaTime;
        else if (x > 0)
            transform.position += directions[1] * speed * Time.deltaTime;

        // 跳躍只能在前一次結束後再次觸發
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isJumping)
        {
            StartCoroutine(Jump());
        }
    }

    IEnumerator Move(Vector3 direction)
    {
        isActing = true;

        transform.position += direction * speed * actionTime;

        yield return new WaitForSeconds(actionTime);

        isActing = false;
    }

    IEnumerator Jump()
    {
        isJumping = true;

        Vector3 start = transform.position;
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime / actionTime;

            float y = Mathf.Sin(time * Mathf.PI) * jump;

            // 修改：使用陣列中的向上方向計算跳躍高度
            Vector3 jumpOffset = directions[2] * y;

            transform.position = new Vector3(
                transform.position.x,
                start.y + jumpOffset.y,
                transform.position.z
            );

            yield return null;
        }

        // 回到跳躍前的高度
        transform.position = new Vector3(
            transform.position.x,
            start.y,
            transform.position.z
        );

        isJumping = false;
    }
}
