// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


//Prime Numbers between 100 & 1000 
A obj1 = new A();
Console.WriteLine();
B obj2 = new B();
Console.WriteLine();
A obj3 = new B();
//B obj4 = new A();


public class A
{
    public A()
    {
        Console.Write("A");
    }
}
public class B : A
{
    public B()
    {
        Console.Write("B");
    }
}



