using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Test.Tests
{
    public abstract class TestBase<T> where T : class
    {
        protected ILogger<T> logger;

        [SetUp]
        public void SetUpBase()
        {
            logger = Mock.Of<ILogger<T>>();
        }
    }
}