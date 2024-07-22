#if NET
extern alias DLaBXrm;
using DLaBXrm::DLaB.Xrm.Ioc;
#else
using Source.DLaB.Xrm.Ioc;
#endif
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Extensions;
using System;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class IocContainerTests
    {
        private IocContainer _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new IocContainer();
            _sut.AddTransient<IExample, Example>();
        }

        [TestMethod]
        public void SingletonRegistration_Should_BeSingleton()
        {
            _sut.AddSingleton<IExample, Example>();
            var provider = _sut.BuildServiceProvider();
            var example = provider.Get<IExample>();
            example.Value = Guid.NewGuid().ToString();

            Assert.AreEqual(example.Value, provider.Get<IExample>().Value, "Second request with the same provider should return the same singleton");
            Assert.AreEqual(example.Value, _sut.BuildServiceProvider().Get<IExample>().Value, "New Service Provider still shares same singleton");
        }

        [TestMethod]
        public void ScopedRegistration_Should_BeScoped()
        {
            _sut.AddScoped<IExample, Example>();
            var provider = _sut.BuildServiceProvider();
            var example = provider.Get<IExample>();
            example.Value = Guid.NewGuid().ToString();
            var provider2 = _sut.BuildServiceProvider();
            var example2 = provider2.Get<IExample>();
            Assert.IsNull(example2.Value, "Second Service Provider Should be new scoped instance");
            example2.Value = Guid.NewGuid().ToString();

            Assert.AreEqual(example.Value, provider.Get<IExample>().Value, "Second request with the same provider should return the same scoped instance.");
            Assert.AreEqual(example2.Value, provider2.Get<IExample>().Value, "Second request with the a different provider should return the same scoped instance.");
            Assert.AreEqual(example.Value, provider.Get<IExample>().Value, "Third request with the same provider should return the original scoped instance.");
            Assert.AreNotEqual(provider.Get<IExample>().Value, provider2.Get<IExample>().Value, "Scopes should be separate.");
        }

        [TestMethod]
        public void TransientRegistration_Should_BeTransient()
        {
            var provider = _sut.BuildServiceProvider();
            var example = provider.Get<IExample>();
            example.Value = Guid.NewGuid().ToString();

            Assert.IsNull(provider.Get<IExample>().Value, "Each request should be a new instance");
        }

        [TestMethod]
        public void MultipleDependencies_Should_CreateAll()
        {
            _sut.AddTransient<IExample, Example2>();
            var provider = _sut.BuildServiceProvider();
            var instance = provider.Get<BigClass>();
            Assert.AreEqual(typeof(Example), instance.Example.GetType());
            Assert.AreEqual(typeof(Example2), instance.Example2.GetType());
        }

        [TestMethod]
        public void CircularDependency_Should_Throw()
        {
            var nl = Environment.NewLine;
            ExpectException<InvalidOperationException>(() =>
            {
                _sut.BuildServiceProvider().Get<Grandpa>();
            }, $"Circular dependency detected for type {typeof(Grandpa).FullName}.{nl}  - {typeof(Father).FullName},{nl}  - {typeof(Me).FullName},{nl}  - {typeof(Grandpa).FullName}");

            ExpectException<InvalidOperationException>(() =>
            {
                _sut.BuildServiceProvider().Get<Me>();
            }, $"Circular dependency detected for type {typeof(Me).FullName}.{nl}  - {typeof(Grandpa).FullName},{nl}  - {typeof(Father).FullName},{nl}  - {typeof(Me).FullName}");

            ExpectException<InvalidOperationException>(() =>
            {
                _sut.BuildServiceProvider().Get<Father>();
            }, $"Circular dependency detected for type {typeof(Father).FullName}.{nl}  - {typeof(Me).FullName},{nl}  - {typeof(Grandpa).FullName},{nl}  - {typeof(Father).FullName}");

            // Break Dependency
            _sut.AddSingleton(new Father(null));
            var grandpa = _sut.BuildServiceProvider().Get<Grandpa>();
            Assert.IsNotNull(grandpa);
            Assert.IsNotNull(grandpa.Parent);
            Assert.IsNotNull(grandpa.Parent.Parent);
            Assert.IsNull(grandpa.Parent.Parent.Parent);
        }

        /// <summary>
        /// Ensures that the exception of the given type was thrown (optionally with the given message) when the given action is performed.
        /// </summary>
        protected void ExpectException<TException>(Action action, string message = null) where TException : Exception
        {
            try
            {
                action();
                Assert.Fail(string.IsNullOrEmpty(message)
                    ? $"Expected exception of type {typeof(TException).Name} to be thrown, but no exception was thrown!"
                    : $"Expected exception of type {typeof(TException).Name} to be thrown with message \"{message}\", but no exception was thrown!");
            }
            catch (TException ex)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    if (message != ex.Message)
                    {
                        throw;
                    }
                }
            }
        }

        [TestMethod]
        public void LazyType_Should_Default()
        {
            var provider = _sut.BuildServiceProvider();
            var lazy = provider.Get<Lazy<IExample>>();

            Assert.IsNotNull(lazy);
            Assert.IsNotNull(lazy.Value);
        }

        [TestMethod]
        public void IgnoreStrategy()
        {
            _sut.AddSingleton<IExample>(s => new Example());
            var example = _sut.BuildServiceProvider().Get<IExample>();
            _sut.DuplicateRegistrationStrategy = DuplicateRegistrationStrategy.Ignore;

            _sut.AddScoped(s =>
            {
                Assert.Fail("Duplicate registration should be ignored");
                return example;
            });
            _sut.BuildServiceProvider().Get<IExample>();


            _sut.AddSingleton(s =>
            {
                Assert.Fail("Duplicate registration should be ignored");
                return example;
            });
            _sut.BuildServiceProvider().Get<IExample>();

            _sut.AddTransient(s =>
            {
                Assert.Fail("Duplicate registration should be ignored");
                return example;
            });
            _sut.BuildServiceProvider().Get<IExample>();
        }

        [TestMethod]
        public void Remove_Should_RemoveIfRegistered()
        {
            Assert.IsTrue(_sut.IsRegistered<IExample>());

            _sut.Remove<IExample>();

            Assert.IsFalse(_sut.IsRegistered<IExample>());
        }
    }

    public class BigClass
    {
        public IExample Example { get; }
        public IExample Example2 { get; }

        public BigClass(IExample example2, Example example)
        {
            Example = example;
            Example2 = example2;
        }
    }

    public class Grandpa
    {
        public Me Parent { get; }
        public Grandpa(Me parent)
        {
            Parent = parent;
        }
    }

    public class Me
    {
        public Father Parent { get; }
        public Me(Father parent)
        {
            Parent = parent;
        }
    }

    public class Father
    {
        public Grandpa Parent { get; }
        public Father(Grandpa parent)
        {
            Parent = parent;
        }
    }

    public class Example2 : IExample
    {
        public string Value { get; set; }
    }

    public class Example : IExample
    {
        public string Value { get; set; }
    }

    public interface IExample
    {
        string Value { get; set; }
    }
}
