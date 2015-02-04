using System.Text;

namespace ExampleLibraryProject
{
    public class StringFactory : IFactory
    {
        private readonly StringBuilder _textRequestScope;

        public StringFactory()
        {
            _textRequestScope = new StringBuilder();
        }

        public string Text(int times)
        {
            for (var i = 0; i < times; i++)
            {
                _textRequestScope.Append("A");
            }
            return _textRequestScope.ToString();
        }

        public void DoSomePreInitialization()
        {
            _textRequestScope.Append("Message: ");
        }
    }

    public interface IFactory
    {
        string Text(int times);
        void DoSomePreInitialization();
    }
}
