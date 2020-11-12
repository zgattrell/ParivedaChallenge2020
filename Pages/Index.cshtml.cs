using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.IO.Compression;

namespace parivedaCompetition.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public String FileName {get; set;}
        public List<Function> functions {get; set;}

        public IndexModel(ILogger<IndexModel> logger)
        {
            FileName = "File name not available";
            _logger = logger;
            this.functions = new List<Function>();
        }

 //       public void OnGet()
 //       {

  //      }

        public void OnPost(IFormFile file1){
            FileName = file1.FileName;
            //Divide into List of strings for each function
            List<Function> functions = getFunctionLists(FileToStringList(file1));
            //Create string of functions
            this.functions = functions;
            //Search through 

        }

        //Takes and IFormFile and reads in into a list of Strings
        private List<String> FileToStringList(IFormFile f){
            //Open input stream
            StreamReader streamIn = new StreamReader(f.OpenReadStream());
            List<String> fileLines = new List<String>();
            String line = "";
            while((line = streamIn.ReadLine()) != null){
                fileLines.Add(line);
            }
            return fileLines;
        }

        //Takes a list of all the lines of a file and divides into seperate lists containing the code of each individual functions
        private List<Function> getFunctionLists(List<String> lines){
            List<Function> functions = new List<Function>();
            //Iterate through each line of the file
            for(int i = 0; i < lines.Count; i++){
                String line = lines.ElementAt<String>(i);
                //Function only works if the function returns/doesnt return(void) one of these datatypes below.
                if(containDatatype(line)){
                    //Ensure the line does not contain any of the following to make sure it isnt just a variable declaration or a comment
                    if(line.Contains("=") || line.Contains(";") || line.Contains("//")){
                        
                    } else{
                        //Create new INstance of function with name and args
                        //functions.Add(FunctionFromDecLine(line));
                        Function f = FunctionFromDecLine(line);
                        //Try to find the { opening curly bracket
                        Boolean functionOpened = false;
                        int operatingLine = i;
                        if(line.Contains("{")){
                            functionOpened = true;
                            operatingLine = i;
                        } else{
                            int j = 1;
                            //Attempt to search through each line underneath the declaration until the opening bracket is found.
                            while(!functionOpened){
                                if(lines.ElementAt<String>(i + j).Contains("{")){
                                    operatingLine = i+j;
                                    functionOpened = true;
                                } else {
                                    j++;
                                }
                            }
                        }
                        //scan through remaining lines to see make a list of all the lines of a function
                        operatingLine++;
                        //Add one to operating line and make open brackets 1 because the line with the first open bracket has been processed
                        int openedBrackets = 1;
                        List<String> functionBody = new List<String>();
                        while(openedBrackets != 0){
                            String currentLine = lines.ElementAt<String>(operatingLine);
                            if(currentLine.Contains("{")){
                                openedBrackets++;
                            }
                            if(currentLine.Contains("}")){
                                openedBrackets--;
                            }
                            if(openedBrackets != 0){
                                functionBody.Add(currentLine);
                            }
                            operatingLine++;
                        }
                        f.setBody(functionBody);
                        functions.Add(f);
                        //Console.WriteLine("Function name=" + functionNameFromDecLine(line));
                    }
                        //find the curly bracket and go down the lines beneath it going "up"
                        //a level for a Opening one and "down" a level for a closing one.
                        //Write every line within a method to a list of strings and export it
                }
            }
            return functions;
        }


        //Will return the function name from a function declaration line.
        private Function FunctionFromDecLine(String s){
            String[] parts = s.Split(" ");
            for(int i = 0; i < parts.Length; i++){
                String upWord = parts[i].ToUpper();
                if(upWord == "VOID" || upWord == "INT" || upWord == "STRING" || upWord == "DOUBLE"){
                    String funcName = parts[i + 1].Trim('{');
                    //Get the arguments and remove them from the function name
                    Boolean beginArgs = false;
                    String args = "";
                    String functionName = "";
                    foreach(char c in funcName){
                        if(beginArgs == true && c != ')'){
                            args += c;
                        }  else{
                            functionName += c;
                        }
                        if(c == '('){
                            beginArgs = true;
                        }
                        if(c == ')'){
                            beginArgs = false;
                        }
                    }
                    functionName = functionName.Trim(')','(');
                    Function ret = new Function(functionName, args.Split(","));
                    return ret;
                }
                }
                //FIX THIS
            return new Function("null", null);
        }
        //Function return true if the String contains a datatype or void declaraction.
        private Boolean containDatatype(String line){
                if(line.Contains("void") || line.Contains("int") || line.Contains("String") || line.Contains("double")){
                    return true;
                } 
                return false;
        }

}
}
