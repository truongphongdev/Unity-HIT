namespace BTNVbuoi2
{
    internal class Bai1
    {

        // Bai 1 
        public static void Input(out int a,out  int b,out int c)
        {
            Console.Write("Nhap vao canh thu 1: ");
            a = int.Parse(Console.ReadLine());
            Console.Write("Nhap vao canh thu 2: ");
            b = int.Parse(Console.ReadLine());
            Console.Write("Nhap vao canh thu 3: ");
            c = int.Parse(Console.ReadLine());
        }

        public static void CheckRec(int a, int b, int c)
        {
            if (a+b>c &&  c+b>a && a+c>b)
            {
                if(a==b && a == c)
                {
                    Console.WriteLine("Day la tam giac deu");
                }
                else if (a == b || a==c || b==c)
                {
                    Console.WriteLine("Day la tam giac can");               
                }
                else
                {
                    Console.WriteLine("Day la tam giac thuong");
                }
            }
            else
            {
                Console.WriteLine("Khong the tao thanh tam giac");
            }
        }
        static void Main(string[] args)
        {
            int a;
            int b;
            int c;
            Input(out a,out  b,out  c);
            CheckRec(a, b, c);
            Console.ReadLine();
        }
    }
}
