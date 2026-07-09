// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

MyClass myClass = new MyClass();
myClass.MyMethod();

public class MyClass
{
    public class Dog
    {
        public string Name { get; set; }

        public string Breed { get; set; }

        public int Age { get; set; }

        public Dog(string name, string breed, int age)
        {
            this.Name = name;
            this.Breed = breed;
            this.Age = age;
        }

        public override string ToString() => $"{this.Name} - {this.Breed} - {this.Age}";
    }

    public void MyMethod()
    {
        int a = 1;
        int b = 2;

        Dog dog = new Dog("Romeo", "Labrador", 2);

        Console.WriteLine(dog); // Romeo - Labrador - 2

        MyMethod2(a, b, dog);

        Console.WriteLine(a); // 2
        Console.WriteLine(b); // 7
        Console.WriteLine(dog); // Doremo - Labrador - 2
    }

    public void MyMethod2(int a, int b, Dog dog)
    {
        a = b;
        b = 7;

        dog.Name = "Dormeo";

        Console.WriteLine(dog); // Dormeo - Labrador - 2
    }
}
