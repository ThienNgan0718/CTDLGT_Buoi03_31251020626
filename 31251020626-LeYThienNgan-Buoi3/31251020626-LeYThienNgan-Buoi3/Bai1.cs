using System;
public class Bai1
{
    // Kiểm tra chuỗi có dấu ngoặc hợp lệ hay không.
    // Nếu hợp lệ, trả về -1; nếu sai, trả về vị trí lỗi.
    static int KiemTraNgoac(string s)
    {
        var st = new Stack<(char ch, int pos)>();

        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];

            // Nếu là ngoặc mở thì lưu vào Stack để chờ ngoặc đóng tương ứng.
            if (c == '(' || c == '[' || c == '{')
            {
                st.Push((c, i));
            }

            else if (c == ')' || c == ']' || c == '}')
            {
                // Kiểm tra ngoặc đóng đóng thừa.
                if (st.Count == 0)
                {
                    return i;
                }

                char open = st.Peek().ch;
                bool isMatch =
                    (open == '(' && c == ')') ||
                    (open == '[' && c == ']') ||
                    (open == '{' && c == '}');

                // Nếu không khớp, ngoặc đóng hiện tại gây ra lỗi -> trả về vị trí lỗi.
                if (!isMatch)
                {
                    return i;
                }

                // Nếu khớp, lấy ngoặc mở ra vì cặp ngoặc đã cân bằng.
                st.Pop();
            }
        }

        // Nếu Stack còn phần tử, vẫn còn ngoặc mở chưa được đóng.
        if (st.Count > 0)
        {
            var remaining = st.ToArray();

            // Báo vị trí ngoặc mở chưa được đóng đứng sớm nhất.
            return remaining[remaining.Length - 1].pos;
        }

        return -1;
    }

    static void Main()
    {
        // Đọc toàn bộ chuỗi đầu vào, kể cả chuỗi rỗng.
        Console.Write("Nhap chuoi: ");
        string s = Console.ReadLine() ?? "";

        int loiPos = KiemTraNgoac(s);

        if (loiPos == -1)
        {
            Console.WriteLine("HOP LE");
        }
        // Nếu có lỗi, in vị trí ký tự gây ra lỗi.
        else
        {
            Console.WriteLine($"SAI tai vi tri {loiPos}");
        }
        
        // Ca 9
        // Tạo chuỗi gồm 500.000 ngoặc mở và 500.000 ngoặc đóng.
        string s1 = new string('(', 500000)
                  + new string(')', 500000);

        // Bắt đầu đo thời gian kiểm tra chuỗi lớn.
        var sw1 = System.Diagnostics.Stopwatch.StartNew();

        // Kiểm tra chuỗi của ca 9.
        int kq1 = KiemTraNgoac(s1);

        // Dừng đồng hồ sau khi kiểm tra xong.
        sw1.Stop();

        // In kết quả và thời gian chạy của ca 9.
        Console.WriteLine("\nCA 9:");
        Console.WriteLine(kq1 == -1 ? "HOP LE" : $"SAI tai vi tri {kq1}");

        Console.WriteLine($"Thoi gian: {sw1.ElapsedMilliseconds} ms");


        // CA 10 

        // Tạo chuỗi gồm 1.000.000 ngoặc mở chưa được đóng.
        string s2 = new string('(', 1000000);

        // Bắt đầu đo thời gian kiểm tra chuỗi của ca 10.
        var sw2 = System.Diagnostics.Stopwatch.StartNew();

        // Kiểm tra chuỗi của ca 10.
        int kq2 = KiemTraNgoac(s2);

        // Dừng đồng hồ sau khi kiểm tra xong.
        sw2.Stop();

        // In kết quả và thời gian chạy của ca 10.
        Console.WriteLine("\nCA 10:");
        Console.WriteLine(kq2 == -1 ? "HOP LE" : $"SAI tai vi tri {kq2}");

        Console.WriteLine($"Thoi gian: {sw2.ElapsedMilliseconds} ms");
    }
}

