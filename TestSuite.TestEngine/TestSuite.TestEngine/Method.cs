﻿using System;

namespace TestSuite.TestEngine
{
    [Serializable]
    public class Method
    {
        public string Name { get; private set; }
        public ParameterCollection Parameters { get; private set; }

        public Method(string name)
        {
            this.Name = name;
            this.Parameters = new ParameterCollection();
        }
    }
}