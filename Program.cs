// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using System.Threading;
using ThreadCase;

class MainClass 
{
    private const int iterator = 1000;
    public static void DoWork() 
    {
        for (int i = 0; i < iterator; i++)
        {
            Console.Write($"B{i} ", i);
        }
    }
    public static void Main()
    {
        //Set up 3 consumers
        Colas cola = new ();
        cola.consumers.Add(new Thread(() => { cola.DoWork(ConsoleColor.Red); }   ));
        cola.consumers.Add(new Thread(() => { cola.DoWork(ConsoleColor.Green); }  ));
        cola.consumers.Add(new Thread(() => { cola.DoWork(ConsoleColor.Blue); }  ));

        cola.consumers.ForEach((t) => { t.Start(); });
        while (true) 
        {
            //Add a new task
            Random r = new ();
            cola.EnqueueTask( ()=> {                 Console.Write (r.Next(10));             });
            //Simulate workload
            Thread.Sleep( r.Next(1000) );
        }
        


        //Thread hilo = new Thread(new ThreadStart(DoWork));
        //hilo.Start();   

        //Thread hilo = new Thread( () =>   DoWork() );
        //hilo.Start();
        //for (int i = 0; i < 9; i++) 
        //{
        //    Thread hilo = new Thread(new ThreadStart(DoWork));
        //    hilo.Name = "Hilo" + i.ToString();
        //    Console.Write (hilo.Name);    
        //    hilo.Start();
        //}




        //for (int i = 0; i < iterator; i++)
        //{
        //    Console.Write($"A  ");
        //}




    }



}






