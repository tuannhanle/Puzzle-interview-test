using System;
using Mics;
using Unity.BossRoom.Infrastructure;
using VContainer;
using VContainer.Unity;

namespace UnityEngine.Scripting
{
    public class ExtensionOfNativeClassAttribute : Attribute
    {
    }
}

[UnityEngine.Scripting.ExtensionOfNativeClass]
public class GameManager : IStartable
{
    

    void IStartable.Start()
    {

    }

    
    

    
}
