using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadCase
{
    internal class Colas
    {

        //array of consumer threads
        public List<Thread> consumers = [];
        
        //Task queue
        public Queue<Action> tasks = new();
        
        //Sync object for locking the task queue
        public readonly object queuelock = new();
        
        //This wait handle notifies consumers of a new task
        public EventWaitHandle newTaskAvailable = new AutoResetEvent (false);
        
        // The sync object for locking the console color
        private readonly object consoleLock = new();

        //Enqueue a new task
        public void EnqueueTask(Action task) 
        {
            lock (queuelock) 
            {
                this.tasks.Enqueue(task);
            }
            newTaskAvailable.Set(); 
        }

        //Thread work for consumers
        public void DoWork(ConsoleColor color) 
        {
            while (true)
            {
                Action task = null; 
                lock (queuelock)
                {
                    if (tasks.Count > 0)
                    {
                        task = tasks.Dequeue();
                    }
                }
                if (task != null)
                {
                    lock (consoleLock)
                    {
                        Console.ForegroundColor = color;    
                    }
                    //execute task
                    task();
                }
                else
                    newTaskAvailable.WaitOne(); 
            }
        }



    }
}
