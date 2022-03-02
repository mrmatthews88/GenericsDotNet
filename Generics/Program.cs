using System;
using System.Reflection;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {

            string myNameSpace = "Generics";
            string myClassName = "Dog";

            
            Type[] typeArguments = { Type.GetType(myNameSpace + "." + myClassName) };
            Type myGenericClassType = typeof(MyGenericClass<>);
            

            Type constructed = myGenericClassType.MakeGenericType(typeArguments);
            MethodInfo methodExample1 = constructed.GetMethod("Example1");
            MethodInfo methodExample2 = constructed.GetMethod("Example2");
            var myGenericClassInstance = Activator.CreateInstance(constructed);

            // run method with params
            var result = methodExample1.Invoke(myGenericClassInstance, new object[] { "Hello" });

            // run method without params
            string result2 = (string)methodExample2.Invoke(myGenericClassInstance, null);
            Console.WriteLine(result2);
        }
    }

    class MyGenericClass<T> where T : Animal, new()
    {
        public void Example1(string testString)
        {
            new T().Run();
            Console.WriteLine(testString);
        }
        public string Example2()
        {
            new T().Run();
            return "Success";
        }
    }

    abstract class Animal
    {
        public abstract void Run();
    }

    class Dog : Animal
    {
        public override void Run()
        {
            Console.WriteLine("I am a dog");
        }
    }

    class Cat : Animal
    {
        public override void Run()
        {
            Console.WriteLine("I am a cat");
        }
    }
}
