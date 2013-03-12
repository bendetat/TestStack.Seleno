using System.Web.Mvc;
using TestStack.Seleno.Extensions;

namespace TestStack.Seleno.PageObjects.Controls
{
    public interface IInputHtmlControl : IHtmlControl
    {
        string Value { get; }
        TReturn ValueAs<TReturn>();
        InputType Type { get; }

        void ReplaceInputValueWith<TProperty>(TProperty inputValue);
    }

    public abstract class InputHtmlControl : HTMLControl, IInputHtmlControl
    {
        // todo: do we want an MVC enum in a public type in the core of the system?
        public abstract InputType Type { get; }

        public string Value
        {
            get { return ValueAs<string>(); }
        }
        
        public TReturn ValueAs<TReturn>()
        {
            var scriptToExecute = string.Format("$('#{0}').val()", Id);
            return Execute().ScriptAndReturn<TReturn>(scriptToExecute);
        }

        public void ReplaceInputValueWith<TProperty>(TProperty inputValue)
        {
            var scriptToExecute = string.Format(@"$('#{0}').val(""{1}"")", Id, inputValue.ToString().ToJavaScriptString());
            Execute().ExecuteScript(scriptToExecute);
        }
    }
}