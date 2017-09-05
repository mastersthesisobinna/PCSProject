using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCSProject
{
    class PDFTool
    {
        private string _projectPath;
        private string[] _pathA, _pathB;
        private string[] _nonCoverPDFFileName, _coverPDFFileName;
        private string _nonCoverPDFSearchPattern, _coverPDFSearchPattern;

        public PDFTool(string folderName, string nonCoverPDFSearchPattern , string coverPDFSearchPattern)
        {
            this._projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            this._projectPath += "\\"+ folderName;
            this._nonCoverPDFSearchPattern = nonCoverPDFSearchPattern;  //example *_SPD_*.pdf
            this._coverPDFSearchPattern = coverPDFSearchPattern;        //example *_SPD COVER PAGE.pdf
        }


        //example \\ObinnaPageMerge path
        public string GetProjectPath()
        {
            return this._projectPath;
        }



        //example \\XYZ_SPD_01012017.pdf path
        public string[] GetNonCoverPDFPath()
        {
            this._pathA = Directory.GetFiles(this._projectPath ,
                this._nonCoverPDFSearchPattern);

            return this._pathA;
        }



        //example \\XYZ_SPD COVER PAGE.pdf path
        public string[] GetCoverPDFPath()
        {
            this._pathB = Directory.GetFiles(this._projectPath ,
                this._coverPDFSearchPattern);

            return this._pathB;
        }



        //example XYZ_SPD_01012017.pdf
        public string[] GetNonCoverPDFFileName()
        {
            this._nonCoverPDFFileName = Directory.GetFiles(this._projectPath ,
                this._nonCoverPDFSearchPattern).Select(Path.GetFileName).ToArray();

            return this._nonCoverPDFFileName;
        }


        //example XYZ_SPD COVER PAGE.pdf
        public string[] GetCoverPDFFileName()
        {
            this._coverPDFFileName = Directory.GetFiles(this._projectPath ,
                this._coverPDFSearchPattern).Select(Path.GetFileName).ToArray();

            return this._coverPDFFileName;
        }


        //example XYZ_SPD COVER PAGE
        public string GetFileNameWithoutExtension(string fileName)
        {
            return fileName.Substring(0 , fileName.IndexOf("_"));
        }
    }
}
