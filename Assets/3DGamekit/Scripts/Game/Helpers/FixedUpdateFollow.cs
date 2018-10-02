﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D
{
    [DefaultExecutionOrder(9999)]
    public class FixedUpdateFollow : MonoBehaviour
    {
        public Transform toFollow;

        private void FixedUpdate()
        {
            if (toFollow == null)
                return;

            transform.position = toFollow.position;
            transform.rotation = toFollow.rotation;
        }
    }
}
