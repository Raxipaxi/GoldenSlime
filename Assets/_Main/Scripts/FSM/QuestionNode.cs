using System;
using System.Collections.Generic;
class QuestionNode : INode
{
    private Func<bool> _question;
    private INode _trueResult;
    private INode _falseResult;
    public void Execute()
    {
        if(_question != null)
        {
            _trueResult.Execute();
        }
        else
        {
            _falseResult.Execute();
        }
    }

    public QuestionNode(Func<bool> question,INode trueResult,INode falseResult)
    {
        _question = question;
        _trueResult = trueResult;
        _falseResult = falseResult;
    }

}
