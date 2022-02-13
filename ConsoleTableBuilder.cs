using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace terrariaPlayerEditor
{
    public class ConsoleTableBuilder
    {
        public List<string[]> tableContent { get; private set; }
        private string space;
        private bool showTableLine = true;
    public ConsoleTableBuilder(string space = "", bool showTableLine = true)
        {
            clear();
            this.space = space;
            this.showTableLine = showTableLine;
        }
        public void print()
        {
            int line = tableContent.Count;
            int[] lineColumnCount = new int[tableContent.Count];
            for (int i = 0; i < tableContent.Count; i++)
            {
                lineColumnCount[i] = tableContent[i].Length;
            }
            int column = lineColumnCount.Max();
            int[] maxColumnLength = new int[column];
            for (int j = 0; j < column; j++)//horizontal
            {
                int[] columnLength = new int[line];
                for (int i = 0; i < line; i++)//vertical
                {
                    columnLength[i] = tableContent[i].Length > j ? getStringLength(tableContent[i][j]) : 0;
                }
                maxColumnLength[j] = columnLength.Max();
            }

            int tableLineLength = 0;
            if (showTableLine)
            {
                tableLineLength = maxColumnLength.Sum() + (column - 1) * getStringLength(space) + 1;
                for (int i = 0; i < tableLineLength; i++)
                {
                    Console.Write("—");
                }
                Console.Write("\n");
            }

            for (int i = 0; i < line; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    string str = "";
                    if (tableContent[i].Length > j)
                    {
                        str = tableContent[i][j];
                    }
                    int length = getStringLength(str);
                    Console.Write(str);
                    for (int k = 0; k < maxColumnLength[j] - length; k++)
                    {
                        Console.Write(" ");
                    }
                    if (j < column - 1)
                    {
                        Console.Write(space);
                    }
                }
                Console.Write("\r\n");
            }
            if (showTableLine)
            {
                for (int i = 0; i < tableLineLength; i++)
                {
                    Console.Write("—");
                }
                Console.Write("\n");
            }
        }
        private int getStringLength(string str)
        {
            if (str.Equals(string.Empty))
                return 0;
            int strlen = 0;
            ASCIIEncoding strData = new ASCIIEncoding();
            //将字符串转换为ASCII编码的字节数字
            byte[] strBytes = strData.GetBytes(str);
            for (int i = 0; i <= strBytes.Length - 1; i++)
            {
                if (strBytes[i] == 63)  //中文都将编码为ASCII编码63,即"?"号
                    strlen++;
                strlen++;
            }
            return strlen;
        }
        public void feedLine(params string[] line)
        {
            tableContent.Add(line);
        }
        public void edit(string[] line , int index)
        {
            tableContent[index] = line;
        }
        public void clear()
        {
            tableContent = new List<string[]>();
        }
    }
}
