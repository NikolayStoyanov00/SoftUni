using System;
using System.Collections.Generic;
using System.Linq;

namespace HW11._Key_Revolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int bulletPrice = int.Parse(Console.ReadLine());
            int gunBarrelSize = int.Parse(Console.ReadLine());

            int[] bullets = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            int[] locks = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            int intelligenceValue = int.Parse(Console.ReadLine());

            Queue<int> locksQueue = new Queue<int>(locks);
            Stack<int> bulletsStack = new Stack<int>(bullets);
            int gunBarrelCurrent = gunBarrelSize;
            while (bulletsStack.Count > 0 && locksQueue.Count > 0)
            {
                int currentBullet = bulletsStack.Pop();
                gunBarrelCurrent--;
                int currentLock = locksQueue.Peek();

                if (currentBullet <= currentLock)
                {
                    Console.WriteLine("Bang!");
                    locksQueue.Dequeue();
                }
                else
                {
                    Console.WriteLine("Ping!");
                }

                if (gunBarrelCurrent == 0 && bulletsStack.Count > 0)
                {
                    Console.WriteLine($"Reloading!");
                    gunBarrelCurrent = gunBarrelSize;
                }
            }
            int totalMoneyEarned = intelligenceValue - bulletPrice * (bullets.Length - bulletsStack.Count);
            if (bulletsStack.Count == 0 && locksQueue.Count > 0)
            {
                Console.WriteLine($"Couldn't get through. Locks left: {locksQueue.Count}");
            }
            else
            {
                Console.WriteLine($"{bulletsStack.Count} bullets left. Earned ${totalMoneyEarned}");
            }
        }
    }
}
