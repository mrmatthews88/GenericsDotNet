using System;
using System.Reflection;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {

            string myNameSpace = "Generics";
            string myClassName = "Cat";

            // The type used to instantiate the generic class ie genericClass<T> ... this is the <T>
            Type myClassType = Type.GetType(myNameSpace + "." + myClassName);

            // an array of types needed for the generic class, 
            //if we needed more than one they go in here in the order of the class definition
            Type[] typeArguments = { myClassType };

            // The Class with the <T> definition we want to instantiate
            Type myGenericClassType = typeof(MyGenericClass<>);
            
            // creates the class with the correct types
            Type constructed = myGenericClassType.MakeGenericType(typeArguments);

            // gets a method/function from the created class to run
            MethodInfo methodExample1 = constructed.GetMethod("Example1");
            MethodInfo methodExample2 = constructed.GetMethod("Example2");

            //instantiates the created class - this will run the constructor (with no parametors)
            var myGenericClassInstance = Activator.CreateInstance(constructed);

            // run method with params
            methodExample1.Invoke(myGenericClassInstance, new object[] { "Hello" });

            // run method without params, also casting the result to same return type as the method
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
