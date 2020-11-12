using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace parivedaCompetition
{
    public class Function
    {

        private string name;
        private string[] args;
        private List<String> body;

        public Function(string name, string[] args){
            this.name = name;
            this.args = new string[args.Length];
            for(int i = 0; i < args.Length; i++){
                this.args[i] = args[i];
            }
        }

        public int getIfNumber(){
            int ret = 0;
            for(int i = 0; i < body.Count; i++){
                int add = Regex.Matches(body[i], "if").Count;
                ret += add;
            }
            return ret;
        }

        public int getElseNumber(){
            int ret = 0;
            for(int i = 0; i < body.Count; i++){
                int add = Regex.Matches(body[i], "else").Count;
                ret += add;
            }
            return ret;
        }

        public int getWhileNumber(){
            int ret = 0;
            for(int i = 0; i < body.Count; i++){
                int add = Regex.Matches(body[i], "while").Count;
                ret += add;
            }
            return ret;
        }

        public int getForNumber(){
            int ret = 0;
            for(int i = 0; i < body.Count; i++){
                int add = Regex.Matches(body[i], "for").Count;
                ret += add;
            }
            return ret;
        }

        //Get number of int , double, and boolean declaration
        public int getIntNumber(){
            int ret = 0;
            for(int i = 0; i < body.Count; i++){
                String[] parts = body[i].Split(" ");
                for(int j = 0; j < parts.Length; j++){
                    if(parts[j].ToUpper() == "INT"){
                        if(parts[j + 2] == "="){
                            ret++;
                        }
                    }
                }
            }
            return ret;
        }

        public int getBoolNumber(){
            int ret = 0;
            for(int i = 0; i < body.Count; i++){
                String[] parts = body[i].Split(" ");
                for(int j = 0; j < parts.Length; j++){
                    if(parts[j].ToUpper() == "BOOLEAN"){
                        if(parts[j + 2] == "="){
                            ret++;
                        }
                    }
                }
            }
            return ret;
        }

        public List<String> getConsoleWrites(){
            List<String> ret = new List<String>();
            for(int i = 0; i < body.Count; i++){
                String line = body[i];
                if(line.ToUpper().Contains("CONSOLE.WRITELINE")){
                    //FIGURE THIS OUT
                    String[] parts = line.Split('(',')');
                    ret.Add(parts[1]);
                }
            }
            return ret;
        }

        //Returns a list of strings of the names called by this function
        public List<String> getFunctionsCalled(List<Function> functions){
            List<String> ret = new List<String>();
            //Scan through every line of the body of each function and see which functions are called
            for(int i = 0; i < body.Count; i++){
                for(int j = 0; j < functions.Count; j++){
                    if(body[i].Contains(functions[j].getName())){
                        ret.Add(functions[j].getName());
                    }
                }
            }
            return ret;
        }
        
        public string getName(){
            return name;
        }
        public String[] getArgs(){
            return args;
        }

        public List<String> getBody(){
            return body;
        }

        public void setBody(List<String> body){
            this.body = body;
        }



    }
}