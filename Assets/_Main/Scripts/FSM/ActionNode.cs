using System;
using System.Collections.Generic;

class ActionNode : INode
{
    private Action _action;
    public void Execute()
    {
        if(_action != null)
        {
            _action();
        }
    }
    public ActionNode(Action action)
    {
        _action = action;
    }
}
