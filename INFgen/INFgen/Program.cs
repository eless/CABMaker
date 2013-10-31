using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace INFgen
{
    class Program
    {
        static int xxxx;
        static System.IO.StreamWriter fileswriter = new System.IO.StreamWriter("DisksFiles.tmp");
        static System.IO.StreamWriter cfilewriter = new System.IO.StreamWriter("CopyFile.tmp");
        static System.IO.StreamWriter destwriter = new System.IO.StreamWriter("DestDirs.tmp");
            
        static bool FileWriting(int count, string str_temp)
        {
            string toDest = ""; ;
            bool ind = false;
            int a = 1; 
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(str_temp);
            {
                foreach (System.IO.FileInfo file in dir.GetFiles("*.*", System.IO.SearchOption.TopDirectoryOnly))
                {
                    if (file.Name != "DataWedgeAPI.dll")
                    File.Move(file.FullName,(file.FullName)+xxxx);
                    
                    
                    if(a==1)
                    {
                        cfilewriter.WriteLine("");
                        cfilewriter.WriteLine("[CopyFiles{0}]", count);
                        if (str_temp.IndexOf("Windows")!=-1)
                            toDest = String.Concat("CopyFiles",count,"=0,\"%CE2%\"");
                        else
                        toDest = String.Concat("CopyFiles", count, "=0,\"%InstallDir%",str_temp.Remove(0,23),"\"");
                        destwriter.WriteLine(toDest);

                    }
                    a++;
                    if (file.Name != "DataWedgeAPI.dll")
                    {
                        fileswriter.WriteLine("\"{0}\"={1}", (file.Name) + xxxx, count);
                        cfilewriter.WriteLine("\"{0}\",{1},,0x00000001", file.Name, (file.Name) + xxxx);
                        xxxx++;
                    }
                    else
                    {
                        fileswriter.WriteLine("\"{0}\"={1}", file.Name, count);
                        cfilewriter.WriteLine("\"{0}\",{1},,0x00000001", file.Name, file);
                    }
                    ind = true;
                }
            }
            return ind;
        }
        static void Main(string[] args)
        {
            int count = 1;
            xxxx = 1000;
            string str_temp="";
            System.IO.StreamReader streader = new System.IO.StreamReader("dirlist.txt");
           // System.IO.StreamReader addstr = new System.IO.StreamReader("head.dat");
            System.IO.StreamWriter stwriter = new System.IO.StreamWriter("EMIapp.inf");
            //while ((str_temp = addstr.ReadLine()) != null)
            //{
            //    stwriter.WriteLine(str_temp);
            //}
          string[] headr = {"[SOURCE FILE]", "Name=#APPNAME#.cab", "Path=\\git\\#APPNAME#.cab", "AllowUninstall=TRUE", "[Version]","Signature=\"$Chicago$\"","AdvancedINF=2.0, \"Error message\"", "CESignature=\"$Windows CE$\"","Provider=\"EMI\"","[CEStrings]","AppName=\"#APPNAME#\"","InstallDir=\"%CE1%\\EMI\\#APPNAME#\"","[CEDevice]","ProcessorType=0","VersionMin=0.0","VersionMax=0.0","BuildMin=0","BuildMax=0","[SourceDisksNames]"};

          foreach (string n in headr)
          {
              stwriter.WriteLine(n.Replace("#APPNAME#",args[0].Replace("_"," ")));
          }

          //  addstr.Close();
            while ((str_temp = streader.ReadLine()) != null)
            {
                if (FileWriting(count, str_temp))
                {
                    stwriter.WriteLine("{0}=,Source{0},,\"{1}\"", count, str_temp);
                   count++;
                }
            }
            streader.Close();
            stwriter.WriteLine("{0}=,Source{0},,",count);
            stwriter.WriteLine("");
            stwriter.WriteLine("[SourceDisksFiles]");
            fileswriter.Close();
            cfilewriter.Close();
            destwriter.Close();

            System.IO.StreamReader filesreader = new System.IO.StreamReader("DisksFiles.tmp");
            while ((str_temp = filesreader.ReadLine()) != null)
            {
                stwriter.WriteLine(str_temp);
            }
            if(File.Exists("Setup.DLL"))
            stwriter.WriteLine("\"Setup.DLL\"={0}",count);
            filesreader.Close();
            
            System.IO.StreamReader cfilereader = new System.IO.StreamReader("CopyFile.tmp");
            while ((str_temp = cfilereader.ReadLine()) != null)
            {
                stwriter.WriteLine(str_temp);
            }
            stwriter.WriteLine("");
            cfilereader.Close();

           // System.IO.StreamReader endreader = new System.IO.StreamReader("End.dat");

            //while (((str_temp = endreader.ReadLine()) != null) && str_temp.Contains("[CEShortcuts"))
            //{
            //    stwriter.WriteLine(str_temp);
            //    stwriter.WriteLine(str_temp = endreader.ReadLine());
            //}
            string[] poster1 = {"[CEShortcuts1]", "\"#APPNAME#.lnk\",0,\"EMI_WM.exe\""};
              foreach (string n in poster1)
              {
                stwriter.WriteLine(n.Replace("#APPNAME#",args[0].Replace("_"," ")));
              }
            
            System.IO.StreamReader destreader = new System.IO.StreamReader("DestDirs.tmp");
            stwriter.WriteLine("[DestinationDirs]");
            while ((str_temp = destreader.ReadLine()) != null)
            {
                stwriter.WriteLine(str_temp);
            }
            destreader.Close();
            string[] poster2 = { "CEShortcuts1=0,\"%CE11%\"", "[AddRegistry]", "HKLM,\"Security\\Shell\\StartInfo\\Start\\#APPNAME#.lnk\",\"Icon\",0x00000000,\"\\Program Files\\EMI\\#APPNAME#\\html\\icons\\36\\emi_appicon.png\"", "[DefaultInstall]", "CopyFiles", "AddReg=AddRegistry", "CEShortcuts=CEShortcuts1", "Reboot=1" };
            foreach (string n in poster2)
            {
                if (n == "CopyFiles")
                {
                    stwriter.Write("{0}=", n);
                    for (int k = 1; k < count - 1; k++)
                        stwriter.Write("{0}{1}, ", n, k);
                    stwriter.WriteLine("{0}{1}", n, count - 1);
                }
                else
                    stwriter.WriteLine(n.Replace("#APPNAME#",args[0].Replace("_"," ")));
            }
            // stwriter.WriteLine(str_temp);
            //while ((str_temp = endreader.ReadLine()) != null)
            //{
            //    if (str_temp == "CopyFiles")
            //    {
            //        stwriter.Write("{0}=", str_temp);
            //        for (int k=1;k<count-1;k++)
            //            stwriter.Write("{0}{1},", str_temp,k);
            //        stwriter.WriteLine("{0}{1}",str_temp,count-1);
            //    }
            //    else
            //        stwriter.WriteLine(str_temp);
            //}
             stwriter.Close();
             File.Delete("CopyFile.tmp");
             File.Delete("DestDirs.tmp");
             File.Delete("DisksFiles.tmp");
        }
    }
}
