using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
public class CircularQueue<T>
    {
        private T[] items;
        private int front;
        private int rear;
        private int count;

        public int Count
        {
            get { return count; }
        }

        public int Capacity
        {
            get { return items.Length; }
        }

        // Khởi tạo hàng đợi với dung lượng ban đầu là 4
        public CircularQueue()
        {
            items = new T[4];
            front = 0;
            rear = 0;
            count = 0;
        }

        // Thêm phần tử vào cuối hàng đợi
        public void Enqueue(T value)
        {
            // Nếu hàng đợi đầy thì mở rộng mảng
            if (count == Capacity)
            {
                Resize();
            }

            items[rear] = value;

            // Cập nhật vị trí rear theo vòng tròn
            rear = (rear + 1) % Capacity;

            count++;
        }

        // Lấy và xóa phần tử đầu hàng đợi
        public T Dequeue()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Hàng đợi đang rỗng!");
            }

            T value = items[front];

            // Xóa phần tử tại vị trí front
            items[front] = default(T);

            // Di chuyển front sang vị trí kế tiếp
            front = (front + 1) % Capacity;

            count--;

            // Nếu hàng đợi rỗng thì đưa các chỉ số về 0
            if (count == 0)
            {
                front = 0;
                rear = 0;
            }

            return value;
        }

        // Xem phần tử đầu hàng đợi nhưng không xóa
        public T Peek()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Hàng đợi đang rỗng!");
            }

            return items[front];
        }

        // Mở rộng dung lượng mảng
        private void Resize()
        {
            T[] newItems = new T[Capacity * 2];

            // Sao chép theo thứ tự logic từ front
            for (int i = 0; i < count; i++)
            {
                newItems[i] = items[(front + i) % Capacity];
            }

            items = newItems;

            // Đặt lại các chỉ số
            front = 0;
            rear = count;
        }
    }

    class Bai3
    {
        static void Main()
        {
            Console.WriteLine("KIEM THU CIRCULAR QUEUE");

            TestCase1();
            TestCase2();
            TestCase3();
            TestCase4();
            TestCase5();
            TestCase6();
            TestCase7();
        }

        // Ca 1: Hang doi rong
        static void TestCase1()
        {
            Console.WriteLine("\nCA 1: HANG DOI RONG");

            CircularQueue<int> q = new CircularQueue<int>();

            Console.WriteLine("Count: " + q.Count);
            Console.WriteLine("Capacity: " + q.Capacity);

            try
            {
                q.Dequeue();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Dequeue: " + ex.GetType().Name);
            }

            try
            {
                q.Peek();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Peek: " + ex.GetType().Name);
            }
        }

        // Ca 2: Hang doi vua day
        static void TestCase2()
        {
            Console.WriteLine("\nCA 2: VUA DAY");

            CircularQueue<string> q = new CircularQueue<string>();

            q.Enqueue("A");
            q.Enqueue("B");
            q.Enqueue("C");
            q.Enqueue("D");

            Console.WriteLine("Count: " + q.Count);
            Console.WriteLine("Capacity: " + q.Capacity);
            Console.WriteLine("Peek: " + q.Peek());
        }

        // Ca 3: Quay vong chi so
        static void TestCase3()
        {
            Console.WriteLine("\nCA 3: QUAY VONG CHI SO");

            CircularQueue<string> q = new CircularQueue<string>();

            q.Enqueue("A");
            q.Enqueue("B");
            q.Enqueue("C");
            q.Enqueue("D");

            q.Dequeue();
            q.Dequeue();

            q.Enqueue("E");
            q.Enqueue("F");

            Console.Write("Lay ra: ");

            while (q.Count > 0)
            {
                Console.Write(q.Dequeue() + " ");
            }

            Console.WriteLine("\nCapacity: " + q.Capacity);
        }

        // Ca 4: Mo rong khi front = 0
        static void TestCase4()
        {
            Console.WriteLine("\nCA 4: MO RONG KHI FRONT = 0");

            CircularQueue<int> q = new CircularQueue<int>();

            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(4);
            q.Enqueue(5);

            Console.WriteLine("Capacity: " + q.Capacity);

            Console.Write("Lay ra: ");

            while (q.Count > 0)
            {
                Console.Write(q.Dequeue() + " ");
            }

            Console.WriteLine();
        }

        // Ca 5: Mo rong khi front khac 0
        static void TestCase5()
        {
            Console.WriteLine("\nCA 5: MO RONG KHI FRONT KHAC 0");

            CircularQueue<string> q = new CircularQueue<string>();

            q.Enqueue("A");
            q.Enqueue("B");
            q.Enqueue("C");
            q.Enqueue("D");

            q.Dequeue();
            q.Dequeue();

            q.Enqueue("E");
            q.Enqueue("F");
            q.Enqueue("G");

            Console.WriteLine("Capacity: " + q.Capacity);

            Console.Write("Lay ra: ");

            while (q.Count > 0)
            {
                Console.Write(q.Dequeue() + " ");
            }

            Console.WriteLine();
        }

        // Ca 6: Enqueue va Dequeue 1 trieu lan
        static void TestCase6()
        {
            Console.WriteLine("\nCA 6: 1 TRIEU LAN");

            CircularQueue<int> q = new CircularQueue<int>();

            for (int i = 0; i < 1000000; i++)
            {
                q.Enqueue(i);
                q.Dequeue();
            }

            Console.WriteLine("Count: " + q.Count);
            Console.WriteLine("Capacity: " + q.Capacity);
        }

        // Ca 7: Enqueue 100000 phan tu roi Dequeue het
        static void TestCase7()
        {
            Console.WriteLine("\nCA 7: 100000 PHAN TU");

            CircularQueue<int> q = new CircularQueue<int>();

            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 1; i <= 100000; i++)
            {
                q.Enqueue(i);
            }

            for (int i = 1; i <= 100000; i++)
            {
                q.Dequeue();
            }

            sw.Stop();

            Console.WriteLine("Count: " + q.Count);
            Console.WriteLine("Capacity: " + q.Capacity);
            Console.WriteLine("Thoi gian: " + sw.ElapsedMilliseconds + " ms");
        }
    }
    

