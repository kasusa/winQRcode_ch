using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winQRcode_ch
{
    class Filepath
    {
        public Filepath()
        {
            setpath();
        }

        public string path { get; set; }
        public void setpath()
        {
            path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
        }
        public void Open(string path1)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "explorer";
            proc.StartInfo.Arguments = @"/select," + path1;
            proc.Start();
        }
    }
}
