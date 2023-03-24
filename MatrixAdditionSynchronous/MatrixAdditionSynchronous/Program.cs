using System.Data.Common;
using System.Diagnostics;

namespace MatrixAdditionSynchronous
{
    /// <summary>
    /// 使用同步計算方式，計算 矩陣A + 矩陣B 的結果到 矩陣C
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // 若這裡數值過大，例如 30000，可能會造成安裝 16GB 的記憶體電腦產生實體記憶體不夠的問題，
            // 使得需要使用虛擬記憶體，結果是每次執行結果都會有明顯不同的速度現象
            int MAXRow = 25000;
            int MAXColumn = 25000;
            int[,] matrixA = new int[MAXRow, MAXColumn] ;
            int[,] matrixB = new int[MAXRow, MAXColumn] ;
            int[,] matrixC = new int[MAXRow, MAXColumn];

            #region 進行 A B C 矩陣值初始化
            Console.WriteLine("進行 A B C 矩陣值初始化");
            int counter = 1;
            for (int r = 0; r < MAXRow; r++)
            {
                for (int c = 0; c < MAXColumn; c++)
                {
                    matrixA[r, c] = counter;
                    matrixB[r, c] = counter++;
                    matrixC[r, c] = 0;
                }
            }
            #endregion

            #region 列出 矩陣A 的 10x10 內容
            //for (int r = 0; r < 10; r++)
            //{
            //    for (int c = 0; c < 10; c++)
            //    {
            //        if (c == 0) Console.Write($"{MatrixValueFormat(matrixA[r, c])}");
            //        else Console.Write($", {MatrixValueFormat(matrixA[r, c])}");
            //    }
            //    Console.WriteLine();
            //}
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("進行 矩陣A + 矩陣B 相加，將結果儲存到 矩陣C");
            stopwatch.Start();
            #region 進行 矩陣A + 矩陣B 相加，將結果儲存到 矩陣C
            for (int r = 0; r < MAXRow; r++)
            {
                for (int c = 0; c < MAXColumn; c++)
                {
                    matrixC[r, c] = matrixA[r, c] + matrixB[r, c];
                }
            }
            #endregion
            stopwatch.Stop();

            #region 列出 矩陣C 的 10x10 內容
            //Console.WriteLine(); Console.WriteLine();
            //for (int r = 0;r < 10; r++)
            //{
            //    for(int c = 0;c < 10; c++)
            //    {
            //        if(c == 0) Console.Write(MatrixValueFormat(matrixC[r, c]));
            //        else Console.Write($", {MatrixValueFormat(matrixC[r, c])}");
            //    }
            //    Console.WriteLine();
            //}
            #endregion

            Console.WriteLine($"執行時間花費 {stopwatch.ElapsedMilliseconds} ms");
        }

        static string MatrixValueFormat(int value)
        {
            return $"{value:D5}";
        }
    }
}