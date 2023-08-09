using System;
using System.Collections;

public interface IStarter
{
    public IEnumerator Initialize();
    public Type Type();
}
