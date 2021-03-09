using System;
using interfaces;

namespace implementations
{
    public class implement:interfaceType
    {
        public readonly string _defaultString = string.Empty;
        public readonly int _runTimeVal = 0;
        public implement(string StringVal)
        {
            _defaultString = StringVal;
        }

        public implement(string StringVal,int runTimeVal)
        {
            _defaultString = StringVal;
            _runTimeVal = runTimeVal;

        }
        public string getStringvalue()
        {
            return "some value";
        }
    }
}
