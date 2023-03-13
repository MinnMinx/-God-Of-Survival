using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QTE
{
    public interface IQte
    {
        float Progress { get; }
        bool IsOver { get; }
        void Activate();
        void OnStart();
        void OnUpdate(float deltaTime);
        void CleanUp();
    }
}