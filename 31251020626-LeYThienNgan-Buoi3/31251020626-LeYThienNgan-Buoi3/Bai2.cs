using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

class Customer
{
    public int Id { get; set; }
    public long ArrivalTime { get; set; }
    public long ServiceTime { get; set; }

    public Customer(int id, long arrivalTime, long serviceTime)
    {
        Id = id;
        ArrivalTime = arrivalTime;
        ServiceTime = serviceTime;
    }
}

class Bai2
{
    static void Main()
    {
        Console.Write("Nhap testcase (1-6): ");
        int testCase = int.Parse(Console.ReadLine());
        int n;
        int k;

        var queue = new Queue<Customer>();

        // CA 6
        if (testCase == 6)
        {
            n = 100000;
            k = 10;

            for (int i = 1; i <= n; i++)
            {
                long a = i - 1;
                long s = 10;

                Customer customer = new Customer(i, a, s);
                queue.Enqueue(customer);
            }
        }
        else
        {
            Console.Write("Nhap so khach hang n va so quay k: ");
            string[] firstLine = Console.ReadLine().Split();

            n = int.Parse(firstLine[0]);
            k = int.Parse(firstLine[1]);

            queue = new Queue<Customer>();

            // Nhập thông tin từng khách hàng
            for (int i = 1; i <= n; i++)
            {
                Console.Write($"Nhap thoi diem den va thoi gian phuc vu KH {i}: ");

                string[] input = Console.ReadLine().Split();

                long a = long.Parse(input[0]);
                long s = long.Parse(input[1]);

                Customer customer = new Customer(i, a, s);

                queue.Enqueue(customer);
            }
        }
        // freeTime[j] lưu thời điểm quầy j có thể phục vụ khách tiếp theo
        long[] freeTime = new long[k];

        long totalWait = 0;
        long maxWait = 0;
        long finishTime = 0;

        // Bắt đầu đo thời gian xử lý Queue
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Xử lý lần lượt khách hàng theo thứ tự đến
        while (queue.Count > 0)
        {
            Customer customer = queue.Dequeue();

            int q = 0;

            for (int j = 1; j < k; j++)
            {
                // Chỉ đổi quầy nếu quầy mới rảnh sớm hơn
                // Nếu bằng nhau thì giữ quầy có số thứ tự nhỏ hơn
                if (freeTime[j] < freeTime[q])
                {
                    q = j;
                }
            }

            // Thời điểm bắt đầu phục vụ
            long b = Math.Max(customer.ArrivalTime, freeTime[q]);

            // Thời gian khách phải chờ
            long w = b - customer.ArrivalTime;

            // Thời điểm khách hoàn thành phục vụ
            long e = b + customer.ServiceTime;

            // Cập nhật thời điểm quầy rảnh
            freeTime[q] = e;

            totalWait += w;

            if (w > maxWait)
            {
                maxWait = w;
            }

            if (e > finishTime)
            {
                finishTime = e;
            }

            if (n <= 100)
            {
                Console.WriteLine($"KH {customer.Id}: den {customer.ArrivalTime}, " +$"quay {q + 1}, bat dau {b}, cho {w}, xong {e}"
                );
            }
        }

        // Dừng đo thời gian xử lý
        stopwatch.Stop();

        double avgWait = (double)totalWait / n;

        Console.WriteLine();
        Console.WriteLine("Cho trung binh: " + avgWait.ToString("F2", CultureInfo.InvariantCulture));

        Console.WriteLine($"Cho lau nhat: {maxWait}");

        Console.WriteLine($"Hoan thanh luc: {finishTime}");

        Console.WriteLine($"Thoi gian chay: {stopwatch.ElapsedMilliseconds} ms");
    }
}