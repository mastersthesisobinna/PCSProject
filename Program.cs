using ceTe.DynamicPDF.Merger;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PCSProject
{
    sealed
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();                                 //Track application performace
            sw.Start();

            MergeDocument document;
            int value = 1, decimalLength = 0;                               //decimalLength and num are both used for
                                                                            //creating an incremental string value 
                                                                            //pattern. An example is 00001, 00002 etc. 


            //Create instance of PDFExtract class to access reusable methods
            PDFTool obj = new PDFTool("PCS_technical_exercise", "*_SPD_*.pdf" , "*_SPD COVER PAGE.pdf");

            //Use Task to improve application performance when locating individual files.
            Task<string[]> t1 = Task.Factory.StartNew(() => obj.GetNonCoverPDFPath());
            Task<string[]> t2 = Task.Factory.StartNew(() => obj.GetCoverPDFPath());
            Task<string[]> t3 = Task.Factory.StartNew(() => obj.GetNonCoverPDFFileName());
            Task<string[]> t4 = Task.Factory.StartNew(() => obj.GetCoverPDFFileName());
            Task.WhenAll(t1 , t2 , t3 , t4);

            //Get project directory and PDF file path
            string path = obj.GetProjectPath();                             //application path
            string[] nonCoverPDFPath = t1.Result;                           //non-cover pdf filepath
            string[] coverPDFPath = t2.Result;                              //cover pdf filepath

            //Get PDF file names
            string[] nonCoverPDFFileName = t3.Result;                       //Containing 'SPD COVER PAGE' in file name
            string[] coverPDFFileName = t4.Result;                          //Containing only 'SPD' in file name


            try
            {
                for (int i = 0; i < nonCoverPDFPath.Length; i++)
                {

                    // Merge the two documents
                    document = MergeDocument.Merge(nonCoverPDFPath[i] , coverPDFPath[i]);

                    //Get only the filename (without) its file's extension
                    string fileNameExtract = obj.GetFileNameWithoutExtension(nonCoverPDFFileName[i]);

                    //pad integer value with four leading zeros 
                    decimalLength = value.ToString("D").Length + 4;
                    string valuePattern = value.ToString("D" + decimalLength.ToString());

                    //increment value
                    value++;

                    // Save new PDF file using only fileNameExtract and valuePattern
                    string newFile = path + @"\" + fileNameExtract + "_" + valuePattern + ".pdf";
                    document.Draw(newFile);

                    Console.WriteLine();

                    Console.WriteLine("File: " + fileNameExtract + "_" + valuePattern + ".pdf" +
                        " succsessfully created in ");
                    Console.WriteLine("Directory: " + path);
                    Console.WriteLine("\n=========");
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sw.Stop();
                long time = sw.ElapsedMilliseconds;
                Console.WriteLine("Execution time (milleseconds): " +time);     //Performance metric report
                Console.ReadLine();
            }

            

        }
    }
}
